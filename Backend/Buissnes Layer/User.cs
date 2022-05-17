using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class User
    {
        public string username { get; }
        private string password;
     

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void ChangePassword(string oldP, string newP)
        {
            if (oldP == this.password)
            {
                this.password = newP;
            }

            else
            {
                throw new ArgumentException("The password provided is wrong");
            }
        }


        public bool ValidatePassword(string password)
        {
            return this.password == password;
        }



    }
}
