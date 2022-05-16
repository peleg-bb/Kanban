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
        public bool Exists(string userEmail, string boardName)
        {
            // if (UserController.isLoggedIn(userEmail))
            //{
            //  Boards.ContainsKey(userEmail);
            //}
            //else
            //{
            //  throw new ArgumentException("user not logged in");
            //}
            return Boards.ContainsKey(userEmail);
        }
        public void CreateBoard(string userEmail, string boardName)
        {
            if (Exists(userEmail, boardName))
            {
                throw new ArgumentException("BOARD IS ALREADY EXIST AT THIS USER , CAN'T CREATE ANOTHER! ");

            }
            else
            {
                Board newBoard = new Board(boardName);
                Boards[userEmail] = newBoard;
            }
        }
        public void DeleteBoard(string userEmail, string boardName)
        {
            if (Exists(userEmail, boardName))
            {
               Boards.Remove(userEmail);

            }
            else
            {
                throw new ArgumentException("BOARD IS NOT EXIST AT THIS USER ! ");
            }
        }
        public Board GetBoard(string userEmail, string boardName)
        {
            if(Exists(userEmail, boardName))
            {
                return Boards[userEmail];
            }
            else
            {
                throw new ArgumentException("BOARD IS NOT EXIST AT THIS USER ! ");
            }
        }

    }
}
