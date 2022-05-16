using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    internal static class Connections
    {
        private static List<string> ConnectedUsers;

        public static List<string> GetConnectedUsers()
        {
            return ConnectedUsers;
        }

        public static bool IsLoggedIn(string email)
        {
            return ConnectedUsers.Contains(email);
        }

        public static void LoginUser(string email)
        {
            ConnectedUsers.Add(email);
        }

        public static void LogoutUser(string email)
        {
            ConnectedUsers.Remove(email);
        }
    }
}
