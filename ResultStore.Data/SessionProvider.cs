using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ResultStore.Data
{
    public class SessionProvider
    {
        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        private const string SESSION_KEY = "CONTEXT_SESSION";

        private ISessionFactory m_session_factory;
        private Configuration m_configuration;

        //---------------------------------------------------------------------------------------------------

        public static SessionProvider Instance {
            get { return Nested.SessionProvider; }
        }

        //---------------------------------------------------------------------------------------------------

        private SessionProvider() {
            SetupConfiguration();
            SetupSessionFactory();
        }

        //---------------------------------------------------------------------------------------------------

        private void SetupConfiguration() {
            m_configuration = new Configuration();
            m_configuration.AddAssembly(Assembly.Load("ResultStore.Model"));
        }

        //---------------------------------------------------------------------------------------------------

        private void SetupSessionFactory() {
            m_session_factory = m_configuration.BuildSessionFactory();
        }

        //---------------------------------------------------------------------------------------------------

        public void RegisterInterceptor(IInterceptor interceptor) {
            ISession session = ContextSession;

            if (session != null && session.IsOpen) {
                throw new CacheException("Can not register an interceptor on an open session");
            }

            GetSession(interceptor);
        }

        //---------------------------------------------------------------------------------------------------

        public ISession GetSession() {
            return GetSession(null);
        }

        //---------------------------------------------------------------------------------------------------

        private ISession GetSession(IInterceptor interceptor) {
            ISession session = ContextSession;

            if (session == null) {
                if (interceptor != null) {
                    session = m_session_factory.OpenSession(interceptor);
                } else {
                    session = m_session_factory.OpenSession();
                }

                ContextSession = session;
            }

            return session;
        }

        //---------------------------------------------------------------------------------------------------

        public void CloseSession() {
            ISession session = ContextSession;

            if (session != null && session.IsOpen) {
                session.Flush();
                session.Close();
            }

            ContextSession = null;
        }

        //---------------------------------------------------------------------------------------------------

        public void BeginTransaction() {
            ITransaction transaction = ContextTransaction;

            if (transaction == null) {
                transaction = GetSession().BeginTransaction();
                ContextTransaction = transaction;
            }
        }

        //---------------------------------------------------------------------------------------------------

        public void CommitTransaction() {
            ITransaction transaction = ContextTransaction;

            try {
                if (HasOpenTransaction()) {
                    transaction.Commit();
                    ContextTransaction = null;
                }
            } catch (HibernateException) {
                RollbackTransaction();
                throw;
            }
        }

        //---------------------------------------------------------------------------------------------------

        public bool HasOpenTransaction() {
            ITransaction transaction = ContextTransaction;

            return transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack;
        }

        //---------------------------------------------------------------------------------------------------

        public void RollbackTransaction() {
            ITransaction transaction = ContextTransaction;

            try {
                if (HasOpenTransaction()) {
                    transaction.Rollback();
                }

                ContextTransaction = null;
            } finally {
                CloseSession();
            }
        }

        //---------------------------------------------------------------------------------------------------

        private ITransaction ContextTransaction {
            get {
                if (IsInWebContext()) {
                    return (ITransaction)HttpContext.Current.Items[TRANSACTION_KEY];
                } else {
                    return (ITransaction)CallContext.GetData(TRANSACTION_KEY);
                }
            }
            set {
                if (IsInWebContext()) {
                    HttpContext.Current.Items[TRANSACTION_KEY] = value;
                } else {
                    CallContext.SetData(TRANSACTION_KEY, value);
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        private ISession ContextSession {
            get {
                if (IsInWebContext()) {
                    return (ISession)HttpContext.Current.Items[SESSION_KEY];
                } else {
                    return (ISession)CallContext.GetData(SESSION_KEY);
                }
            }
            set {
                if (IsInWebContext()) {
                    HttpContext.Current.Items[SESSION_KEY] = value;
                } else {
                    CallContext.SetData(SESSION_KEY, value);
                }
            }
        }

        //---------------------------------------------------------------------------------------------------

        private bool IsInWebContext() {
            return HttpContext.Current != null;
        }

        //---------------------------------------------------------------------------------------------------

        public void RebuildSchema() {
            new SchemaExport(m_configuration).Create(true, true);
        }

        //---------------------------------------------------------------------------------------------------

        private class Nested
        {
            static Nested() { }
            internal static readonly SessionProvider SessionProvider = new SessionProvider();
        }
    }
}
