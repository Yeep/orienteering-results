using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ResultStore.Model.Orienteering;

namespace ResultStore.Model.Authentication
{
    public class User
    {
        private static MD5CryptoServiceProvider s_crypto;

        private string m_password;

        //---------------------------------------------------------------------------------------------------

        static User() {
            s_crypto = new MD5CryptoServiceProvider();
        }

        //---------------------------------------------------------------------------------------------------

        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { 
            get { return m_password; }
            set { m_password = HashPassword(value); }
        }
        public virtual string Email { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Right> Rights { get; set; }

        public virtual Person Person { get; set; }

        //---------------------------------------------------------------------------------------------------

        private static string HashPassword(string password) {
            byte[] bytes = UnicodeEncoding.UTF8.GetBytes(password);
            byte[] hashed_bytes = s_crypto.ComputeHash(bytes);
            return Convert.ToBase64String(hashed_bytes);
        }
    }
}
