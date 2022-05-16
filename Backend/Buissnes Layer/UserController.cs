using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    internal class UserController
    {
        // To complete - change the loggedIn checks across this class.
        
        private Dictionary<string, User> users;


        public void CreateUser(string email, string password)
        {
            if (UserExists(email))
            {
                throw new ArgumentException("User already exists");
            }
            else
            {
                User u = new User(email, password);
                users.Add(email, u);
            }
        }


        public void DeleteUser(string email)
        {
            if (users.ContainsKey(email))
            {
                users.Remove(email); // Remove from existing users list

                if (IsLoggedIn(email)) // Remove from logged-in list
                {
                    Connections.LogoutUser(email);
                }
            }
            else
            {
                throw new ArgumentException("User does not exist");
            }
        }

        public bool UserExists(string email)
        {
            return users.ContainsKey(email);
        }

        public User GetUser(string username)
        {
            if (users.ContainsKey(username))
            {
                return users[username];
            }
            else
            {
                throw new ArgumentException("User does not exist");
            }

        }

        public bool ValidatePassword(string email, string password)
        {
            if (users.ContainsKey(email))
            {
                return users[email].ValidatePassword(password);
            }
            else
            {
                throw new ArgumentException("User does not exist");
            }
        }

        public void Login(string email, string password)
        {
            if (!UserExists(email))
            {
                throw new ArgumentException("User does not exist");
            }

            else if (!ValidatePassword(email, password))
            {
                throw new ArgumentException("Wrong password");
            }

            else if (Connections.IsLoggedIn(email))

            {
                throw new ArgumentException("User is already logged in");
            }

            else
            {
                Connections.LoginUser(email);
            }
        }

        public void Logout(string email) //Contains the logic flow of logging out connected users
        {
            if (!UserExists(email))
            {
                throw new ArgumentException("User does not exist");
            }

            else if (!Connections.IsLoggedIn(email))

            {
                throw new ArgumentException("User is already logged out");
            }

            else
            {
                Connections.LogoutUser(email);
            }

        }

        public bool IsLoggedIn(string email)
        {
            if (UserExists(email))
            {
                return Connections.IsLoggedIn(email);
            }
            else
            {
                throw new ArgumentException("User does not exist");
            }
            
        }

    }
}
