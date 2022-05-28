using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserDTO
    {
        private string username;
        private string password;

        public UserDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void ChangePassword(string NewPassword)
        {
            
            
            
        }
    }
}
