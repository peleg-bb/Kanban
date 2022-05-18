using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool IsValidEmail(string email)
        {
            Regex regex = new Regex(
                @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return regex.IsMatch(email);
        }

        public void CreateUser(string email, string password)
        {
            if (this.users != null)
            {
                if (UserExists(email))
                {
                    throw new ArgumentException("User already exists");
                }
                if (!IsValidEmail(email))
                {
                    throw new ArgumentException("Not a valid email address");
                }

                if (!IsLegalPassword(password))
                {
                    throw new ArgumentException("Illegal password. A legal password must be 6-20 characters" +
                                                " and must contain an Upper case, a lower case and a number");
                }
                else
                {
                    User u = new User(email, password);
                    users.Add(email, u);
                }
            }
            else
            {
               
                if (!IsValidEmail(email))
                {
                    throw new ArgumentException("Not a valid email address");
                }
                if (!IsLegalPassword(password))
                {
                    throw new ArgumentException("Illegal password. A legal password must be 6-20 characters" +
                                                " and must contain an Upper case, a lower case and a number");
                }
                else
                {
                    User u = new User(email, password);
                    users.Add(email, u);
                }
                
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

        public bool IsLegalPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            if (password.Length > 20 )
            {
                return false;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsNumber))
            {
                return false;
            }
            else
            {
                return true;
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
                throw new ArgumentException("User does not exist");
            }

            else if (!ValidatePassword(email, password))
            {
                throw new ArgumentException("Wrong password");
            }

            else if (loggedIn.Contains(email))

            {
                throw new ArgumentException("User is already logged in");
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
                throw new ArgumentException("User does not exist");
            }

            else if (!loggedIn.Contains(email))

            {
                throw new ArgumentException("User is already logged out");
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
                throw new ArgumentException("User does not exist");
            }
            return loggedIn.Contains(email);
        }

    }
}