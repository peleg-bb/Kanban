using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class UserController
    {

        private Dictionary<string, User> users;
        private List<string> loggedIn;

        public UserController()
        {
            this.users = new Dictionary<string, User>();
            this.loggedIn = new List<string>();
        }


        public void CreateUser(string email, string password)
        {
            if (!(this.users == null))
            {
                if (UserExists(email))
                {
                    throw new ArgumentException("UserService already exists");
                }

                else
                {
                    User u = new User(email, password);
                    users.Add(email, u);
                }
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
                users.Remove(email);
            }
            else
            {
                throw new ArgumentException("UserService does not exist");
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
                throw new ArgumentException("UserService does not exist");
            }

        }

        public bool ValidatePassword(string email, string password)
        {
            if (users.ContainsKey(email))
            {
                return users[email].ValidatePassword(password);
            }
            return false;
        }

        public void Login(string email, string password)
        {
            if (!UserExists(email))
            {
                throw new ArgumentException("UserService does not exist");
            }

            else if (!ValidatePassword(email, password))
            {
                throw new ArgumentException("Wrong password");
            }

            else if (loggedIn.Contains(email))

            {
                throw new ArgumentException("UserService is already logged in");
            }

            else
            {
                loggedIn.Add(email);
            }
        }

        public void Logout(string email)
        {
            if (!UserExists(email))
            {
                throw new ArgumentException("UserService does not exist");
            }

            else if (!loggedIn.Contains(email))

            {
                throw new ArgumentException("UserService is already logged out");
            }

            else
            {
                loggedIn.Remove(email);
            }

        }

        public bool IsLoggedIn(string email)
        {
            if (!UserExists(email))
            {
                throw new ArgumentException("UserService does not exist");
            }
            return loggedIn.Contains(email);
        }

    }
}