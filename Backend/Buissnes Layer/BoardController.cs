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
        private UserController userController;

        public BoardController(UserController UC)
        {
            this.userController = UC;
        }

        public bool BoardExists(string userEmail, string boardName) //checks if board exists
        { 
            if (userController.IsLoggedIn(userEmail))
            {
                if (Boards.ContainsKey(userEmail))
                {

                    return this.Boards[userEmail].ContainsKey(boardName);

                }
                else
                {
                    Board board = new Board(boardName);
                    Dictionary<string,Board> boardname = new Dictionary<string,Board>();
                    this.Boards.Add(userEmail,boardname);
                    return true;
                }
                
            }
            else
            {
              throw new ArgumentException("user not logged in");
            }
        }

        public List<Task> GetAllInPrograss(string userEmail)
        {
            if (userController.IsLoggedIn(userEmail))
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
            if (userController.IsLoggedIn(userEmail)) 
            {
                if (BoardExists(userEmail, boardName))
                {
                    Board newBoard = new Board(boardName);
                    this.Boards[userEmail].Add(boardName, newBoard);

                }
                else
                {
                    throw new ArgumentException(
                        "BOARD IS ALREADY EXIST AT THIS USER , CAN'T CREATE ANOTHER WITH THE SAME NAME! ");
                }

            }
            else
            {
                throw new ArgumentException("user not logged in");
            }
        }
        public void DeleteBoard(string userEmail, string boardName)
        {
            if (userController.IsLoggedIn(userEmail))
            {
                if (BoardExists(userEmail, boardName))
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
            if (userController.IsLoggedIn(userEmail))
            {
                if(BoardExists(userEmail, boardName))
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
