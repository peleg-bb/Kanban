using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;

namespace Frontend.ModelView
{
    internal class UserVM
    {
        private UserModel userModel;
        public string email { get; set; }

        public UserVM()
        {
            this.userModel = new UserModel();
        }

        public bool Login(string username, string password)
        {
            email = userModel.Login(username, password);
            if (email == username)
            {
                return true;
            }
            else
            {
                return true;
            }
            
        }

        public bool Register(string username, string password)
        {
            return this.userModel.Register(username, password);
        }

        public bool Logout(string email)
        {
            this.userModel.Logout(email);
            return true;
        }
    }
}
