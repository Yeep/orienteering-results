using System;
using ResultStore.Model.Authentication;

namespace ResultStore.Repository
{
    public interface IAuthenticationRepository
    {
        bool IsValidLogin(string username, string password);
        void SaveUser(ref User user);

        bool UserExists(string username);
        User GetUserById(Int32? id);
        User GetUserByName(string username);
    }
}
