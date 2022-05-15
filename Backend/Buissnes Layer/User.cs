using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    internal class User
    {
        public string username;
        private string password;
        private Collection<Board> Boards;
        private Collection<Task> tasksInProgress;

        public User(string username, string password)
        {
            this.password = password;
            this.username = username;
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

        public bool Login(string password) // This may be a redundant method as the UserController already contains it
        {
            return true;
        }

        public bool ValidatePassword(string password)
        {
            return this.password == password;
        }



    }
}
