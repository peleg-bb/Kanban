using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    internal class Connections
    {
        private static List<string> ConnectedUsers;

        public List<string> GetConnectedUsers()
        {
            return ConnectedUsers;
        }

        public void LoginUser(string email)
        {
            ConnectedUsers.Add(email);
        }

        public void LogoutUser(string email)
        {
            ConnectedUsers.Remove(email);
        }
    }
}
