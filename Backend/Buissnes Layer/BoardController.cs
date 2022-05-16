using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class BoardController
    {
        private Dictionary<string, Dictionary<string,Board>> Boards = new Dictionary<string, Dictionary<string, Board>>();

        public bool Exists(string userEmail, string boardName)
        { 
            if (Connections.IsLoggedIn(userEmail))
            {
                if (Boards.ContainsKey(userEmail))
                {
                    return this.Boards[userEmail].ContainsKey(boardName);

                }
                else
                {
                    throw new ArgumentException("user exist");
                }
            }
            else
            {
              throw new ArgumentException("user not logged in");
            }
        }

        public List<Task> GetAllInPrograss(string userEmail)
        {
            if (Connections.IsLoggedIn(userEmail))
            {
                if (Boards.ContainsKey(userEmail))
                {
                    Dictionary<string, Board> boards = Boards[userEmail];
                    List<Task>  taskInProgList= new List<Task>();
                    foreach (var item in boards.Keys)
                    {
                        taskInProgList.AddRange(boards[item].GetInProgress());
                    }
                    return taskInProgList;
                }
                else
                {
                    List<Task> taskInProgList = new List<Task>();
                    return taskInProgList;
                }
               

            }
            else
            {
                throw new ArgumentException("user not logged in");
            }
        }
        public void CreateBoard(string userEmail, string boardName)
        { 
            if (Connections.IsLoggedIn(userEmail)) 
            {
                if (Exists(userEmail, boardName))
                {

                    throw new ArgumentException(
                        "BOARD IS ALREADY EXIST AT THIS USER , CAN'T CREATE ANOTHER WITH THE SAME NAME! ");
                }
                else
                {

                    Board newBoard = new Board(boardName);
                    this.Boards[userEmail].Add(boardName, newBoard);
                }

            }
            else
            {
                throw new ArgumentException("user not logged in");
            }
        }
        public void DeleteBoard(string userEmail, string boardName)
        {
            if (Connections.IsLoggedIn(userEmail))
            {
                if (Exists(userEmail, boardName))
                {
                    this.Boards[userEmail].Remove(boardName);

                }
                else
                {
                    throw new ArgumentException("BOARD IS NOT EXIST AT THIS USER ! ");
                }

            }
            else
            {
                throw new ArgumentException("user not logged in");
            }

        }
        public Board GetBoard(string userEmail, string boardName)
        {
            if (Connections.IsLoggedIn(userEmail))
            {
                if(Exists(userEmail, boardName))
                {
                    return this.Boards[userEmail][boardName];
                }
                else
                {
                    throw new ArgumentException("BOARD IS NOT EXIST AT THIS USER ! ");
                }

            }
            else
            {
                throw new ArgumentException("user not logged in");
            }
        }

    }
}
