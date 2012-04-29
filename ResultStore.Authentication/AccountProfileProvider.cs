using System;
using System.Web.Profile;
using ResultStore.Data.Authentication;
using ResultStore.Repository;

namespace ResultStore.Authentication
{
    public class AccountProfileProvider : ProfileProvider
    {
        private static IAuthenticationRepository c_repository;

        static AccountProfileProvider() {
            c_repository = new AuthenticationRepository();
        }

        //---------------------------------------------------------------------------------------------------

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override int DeleteProfiles(string[] usernames) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override int DeleteProfiles(ProfileInfoCollection profiles) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate) {
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

        public override System.Configuration.SettingsPropertyValueCollection GetPropertyValues(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyCollection collection) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override void SetPropertyValues(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyValueCollection collection) {
            throw new NotImplementedException();
        }
    }
}
