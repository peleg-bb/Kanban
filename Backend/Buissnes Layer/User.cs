using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    /// <summary>
    /// A class for representing a User. Should only be instantiated by the UserController
    /// </summary>
    public class User
    {
        public string username { get; }
        private string password;
        private UserDTO userDTO;

        /// <summary>
        /// Constructor. Should only be instantiated by UserController
        /// </summary>
        /// <param name="user">A userDTO object, containing a username and a password</param>
        internal User(UserDTO user)
        {
            this.username = user.getUsername();
            this.password = user.getPassword();
            this.userDTO = user;
        }

        /// <summary>
        /// Allows the UserController to change a user's password.
        /// </summary>
        internal void ChangePassword(string oldP, string newP)
        {
            if (oldP == this.password)
            {
                this.password = newP;
                userDTO.ChangePassword(newP);
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
