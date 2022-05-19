using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    /// <summary>
    /// A class for representing a User. Should only be instantiated by the UserController
    /// </summary>
    public class User
    {
        public string username { get; }
        private string password;


        internal User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Allows the UserController to change a user's password.
        /// </summary>
        internal void ChangePassword(string oldP, string newP)
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

        /// <summary>
        /// Returns whether the user's password is correct.
        /// </summary>
        internal bool ValidatePassword(string password)
        {
            return this.password == password;
        }

        
    }
}
