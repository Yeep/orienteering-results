using System;
using ResultStore.Model.Authentication;
using ResultStore.Repository;

namespace ResultStore.Data.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public bool IsValidLogin(string username, string password) {
            User user = new User {
                Name = username,
                Password = password
            };

            var users = SessionProvider.Instance.GetSession().CreateQuery("FROM User u WHERE u.Name = :username AND u.Password = :password")
                .SetString("username", "'" + user.Name + "'")
                .SetString("password", "'" + user.Password + "'")
                .List<User>();
            if (users.Count != 1) {
                return false;
            }
            return true;
        }

        //---------------------------------------------------------------------------------------------------

        public void SaveUser(ref User user) {
            using (var transaction = SessionProvider.Instance.GetSession().BeginTransaction()) {
                SessionProvider.Instance.GetSession().SaveOrUpdate(user);
                transaction.Commit();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public bool UserExists(string username) {
            var users = SessionProvider.Instance.GetSession().CreateQuery("FROM User u WHERE u.Name = :username")
                .SetString("username", username)
                .List<User>();
            if (users.Count == 0) {
                return false;
            }
            return true;
        }

        //---------------------------------------------------------------------------------------------------

        public User GetUserById(Int32? id) {
            return SessionProvider.Instance.GetSession().Get<User>(id);
        }

        //---------------------------------------------------------------------------------------------------

        public User GetUserByName(string username) {
            return SessionProvider.Instance.GetSession().CreateQuery("FROM User u WHERE u.Name = :username")
                .SetString("username", username)
                .UniqueResult<User>();
        }
    }
}
