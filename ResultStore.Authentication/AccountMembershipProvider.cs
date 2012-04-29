using System;
using System.Web.Security;
using ResultStore.Data.Authentication;
using ResultStore.Model.Authentication;
using ResultStore.Repository;

namespace ResultStore.Authentication
{
    public class AccountMembershipProvider : MembershipProvider
    {
        private static IAuthenticationRepository c_repository;

        static AccountMembershipProvider() {
            c_repository = new AuthenticationRepository();
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            if (String.IsNullOrEmpty(username)) {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }

            if (String.IsNullOrEmpty(password)) {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (String.IsNullOrEmpty(email)) {
                status = MembershipCreateStatus.InvalidEmail;
                return null;
            }

            if (c_repository.UserExists(username)) {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            try {
                User user = new User {
                    Name = username,
                    Password = password,
                    Email = email
                };
                c_repository.SaveUser(ref user);

                status = MembershipCreateStatus.Success;
                return new MembershipUser(this.Name, user.Name, user.Id, user.Email, null, null, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            } catch (Exception) {
                status = MembershipCreateStatus.ProviderError;
                return null;
            }
        }

        //---------------------------------------------------------------------------------------------------

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override bool EnablePasswordReset {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override bool EnablePasswordRetrieval {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override int GetNumberOfUsersOnline() {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override string GetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override string GetUserNameByEmail(string email) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override int MaxInvalidPasswordAttempts {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override int MinRequiredNonAlphanumericCharacters {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override int MinRequiredPasswordLength {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override int PasswordAttemptWindow {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override MembershipPasswordFormat PasswordFormat {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override string PasswordStrengthRegularExpression {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override bool RequiresQuestionAndAnswer {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override bool RequiresUniqueEmail {
            get { throw new NotImplementedException(); }
        }

        //---------------------------------------------------------------------------------------------------

        public override string ResetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override bool UnlockUser(string userName) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override void UpdateUser(MembershipUser user) {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------

        public override bool ValidateUser(string username, string password) {
            return c_repository.IsValidLogin(username, password);
        }
    }
}
