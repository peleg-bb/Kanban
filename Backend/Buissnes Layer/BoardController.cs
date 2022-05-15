using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class BoardController
    {
        private Dictionary<string, Board> Boards;
        public void Exists(string userEmail, string boardName)
        {
            if (UserController.isLoggedIn(userEmail))
            {
                Boards.ContainsKey(userEmail);
            }
            else
            {
                throw new ArgumentException("user not logged in");
            }
        }
        public void CreateBoard(string userEmail, string boardName)
        {

        }
        public void DeleteBoard(string userEmail, string boardName)
        {

        }
        public void GetBoard(string userEmail, string boardName)
        {

        }

    }
}
