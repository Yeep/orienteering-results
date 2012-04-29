using System.Web;

namespace ResultStore.Data
{
    public class NHibernateSessionModule : IHttpModule
    {
        public void Dispose() {
        }

        public void Init(HttpApplication context) {
            context.BeginRequest += delegate {
                SessionProvider.Instance.GetSession();
            };
            context.EndRequest += delegate {
                SessionProvider.Instance.CloseSession();
            };
        }
    }
}
