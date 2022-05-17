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

        public bool UserHasAnyBoard(string userEmail) //checks if user has any board
        {
            if (Boards.ContainsKey(userEmail))
            {
                return true;

            }
            else
            {
                return false;
            }

        }


        public bool UserHasThisBoard(string userEmail,string boardName) //checks if board exists
        {
            if (this.Boards[userEmail].ContainsKey(boardName))
            { 
                return true;
            }
            else 
            { 
                return false;
            }

            
        }

        public void CreateBoard(string userEmail, string boardName)
        {
            if (userController.IsLoggedIn(userEmail))
            {
                if (UserHasAnyBoard(userEmail))
                {
                    if (!UserHasThisBoard(userEmail, boardName))
                    {
                        Board newBoard = new Board(boardName);
                        this.Boards[userEmail].Add(boardName, newBoard);
                    }
                    else
                    {
                        throw new ArgumentException("USER CANNOT CREATE A THIS BOARD! USER HAS A BOARD WITH THIS NAME ALREADY");
                    }

                }
                else
                {
                    Board newBoard = new Board(boardName);
                    Dictionary<string, Board> board = new Dictionary<string, Board>();
                    board.Add(boardName, newBoard);
                    Boards.Add(userEmail, board);
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

        public void DeleteBoard(string userEmail, string boardName)
        {
            if (userController.IsLoggedIn(userEmail))
            {
                if (UserHasThisBoard(userEmail, boardName))
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
                if(UserHasThisBoard(userEmail, boardName))
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
