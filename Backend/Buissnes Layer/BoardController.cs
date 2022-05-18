using System;
using System.CodeDom;
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
        /// <summary>
        /// This method checks if a user has any board.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <returns>bool </returns>
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

        /// <summary>
        /// This method checks if a user has a certain board already.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>bool </returns>
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
        /// <summary>
        /// This method adds a board to the specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>void, unless an error occurs </returns>
        public void CreateBoard(string userEmail, string boardName)
        {
            try
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
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            
        }
        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <returns>Response with a list of the in progress tasks, unless an error occurs .</returns>
        public List<Task> GetAllInPrograss(string userEmail)
        {
            try
            {
                if (userController.IsLoggedIn(userEmail))
                {
                    if (Boards.ContainsKey(userEmail))
                    {
                        Dictionary<string, Board> boards = Boards[userEmail];
                        List<Task> taskInProgList = new List<Task>();
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
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            
        }
        /// <summary>
        /// This method removes a board to the specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>void, unless an error occurs </returns>
        public void DeleteBoard(string userEmail, string boardName)
        {
            try
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
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }


        }
        /// <summary>
        /// This method get a specific board to the specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>Board, unless an error occurs .</returns>
        public Board GetBoard(string userEmail, string boardName)
        {
            try
            {
                if (userController.IsLoggedIn(userEmail))
                {
                    if (UserHasThisBoard(userEmail, boardName))
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
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
          
        }

    }
}
