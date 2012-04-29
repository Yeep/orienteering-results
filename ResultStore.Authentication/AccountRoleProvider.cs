using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using ResultStore.Data.Authentication;
using ResultStore.Model.Authentication;
using ResultStore.Repository;

namespace ResultStore.Authentication
{
    public class AccountRoleProvider : RoleProvider
    {
        private static IAuthenticationRepository c_repository;

        static AccountRoleProvider() {
            c_repository = new AuthenticationRepository();
        }

        //---------------------------------------------------------------------------------------------------

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override string ApplicationName {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        //---------------------------------------------------------------------------------------------------

        public override void CreateRole(string roleName) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override string[] GetAllRoles() {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override string[] GetRolesForUser(string username) {
            User user = c_repository.GetUserByName(username);
            IList<string> roles = new List<string>();

            foreach (var role in user.Roles) {
                roles.Add(role.Name);
            }

            foreach (var right in user.Rights) {
                foreach (var role in right.Roles) {
                    roles.Add(role.Name);
                }
            }

            return roles.Distinct().ToArray();
        }

        //---------------------------------------------------------------------------------------------------

        public override string[] GetUsersInRole(string roleName) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override bool IsUserInRole(string username, string roleName) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override bool RoleExists(string roleName) {
            throw new NotImplementedException();
        }
    }
}
