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
        private int next_id;
        private Dictionary<int, User> users;
        private Collection<User> loggedIn;


        public void CreateUser(){}


        public void DeleteUser(){}

        public bool UserExists()
        {
            return true;
        }

        public User GetUser()
        {
            return new User();
        }

        public bool ValidatePassword()
        {
            return true;
        }

        public void Login(){}
        public void Logout(){}




    }
}
