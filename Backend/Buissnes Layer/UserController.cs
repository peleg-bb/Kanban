using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.DataAccessLayer.Mappers;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    /// <summary>
    /// A class to instantiate users and control their logic. As of the end of milestone 2 -
    /// BusinessLayer classes should call the IsLoggedIn method.
    /// ServiceLayer classes should call the CreateUser, Login and Logout methods.
    /// </summary>
    public class UserController
    {

        private Dictionary<string, User> users;
        private List<string> loggedIn;
        private UserDTOMapper userDtoMapper;
        /// <summary>
        /// Constructor for the class. Instantiates its private fields.
        /// The class must be instantiated in order to call its methods and functionality.
        /// </summary>
        public UserController()
        {
            this.users = new Dictionary<string, User>();
            this.loggedIn = new List<string>();
            this.userDtoMapper = new UserDTOMapper();
            LoadUsers();
        }

        /// <summary>
        /// Checks whether a string contains Hebrew characters. Should be used in conjunction with IsLegalEmail.
        /// </summary>
        private bool IsHebrew(string str)
        {
            string[] heb =
            {
                "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "כ", "ל", "מ", "נ", "ס", "ע", "פ", "צ", "ק", "ר", "ש",
                "ת", "ף", "ץ", "ך", "ן"
            };
            List<string> hebrew = new List<string>(heb);
            for (int i = 0; i < heb.Length; i++)
            {
                if (str.Contains(heb[i]))
                {
                    return true;
                }

            }

            return false;

        }

        /// <summary>
        /// Checks whether the an email address is valid using a regex.
        /// </summary>
        private bool IsValidEmail(string email)
        {
            Regex regex = new Regex(
                @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return regex.IsMatch(email);
        }
        /// <summary>
        /// Checks whether the an email address is valid using a regex.
        /// </summary>
        bool IsValidEmail2(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Creates a user. A new user's email must not preexist in the system and must have a valid email and password.
        /// </summary>
        public void CreateUser(string email, string password)
        {
            if (this.users != null)
            {
                if (UserExists(email))
                {
                    throw new ArgumentException("User already exists");
                }
                if (!IsValidEmail(email) || IsHebrew(email)||!IsValidEmail2(email))
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
                    UserDTO userDto = userDtoMapper.CreateUser(email, password);
                    User u = new User(userDto);
                    users.Add(email, u);
                }
            }
            else
            {
               
                if (!IsValidEmail(email) || IsHebrew(email)||!IsValidEmail2(email))
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
                    UserDTO userDto = userDtoMapper.CreateUser(email, password);
                    User u = new User(userDto);
                    users.Add(email, u);
                    
                }
                
            }
        }

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        public void DeleteUser(string email)
        {
            try
            {
                users.Remove(email);
            }
            catch (Exception){
                throw new ArgumentException("User does not exist");
            }
        }

        /// <summary>
        /// Checks whether a user exists in the system.
        /// </summary>
        private bool UserExists(string email)
        {
            return users.ContainsKey(email);
        }

        /// <summary>
        /// Returns a user object. Used for internal purposes.
        /// </summary>
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

        /// <summary>
        /// Checks whether a password is legal according to the system requirements.
        /// A legal password is 6-20 characters, contains at least 1 uppercase, 1 lower case and one number
        /// </summary>
        private bool IsLegalPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            if (password.Length > 20 )
            {
                return false;
            }
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsNumber)||IsHebrew(password))
            {
                return false;
            }

            if (!password.Any(char.IsAscii))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private bool ValidatePassword(string email, string password)
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

        public void LoadUsers()
        {
            List<UserDTO> userDtos = userDtoMapper.LoadUsers();
            foreach (var u in userDtos)
            {
                User user = new User(u);
                users[user.username] = user;
                // Console.WriteLine($"User {user.username} loaded successfully!");
            }
            
        }
    }
}