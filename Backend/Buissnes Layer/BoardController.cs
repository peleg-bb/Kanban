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
        private Dictionary<string, Dictionary<string,Board>> BoardsOfUsers = new Dictionary<string, Dictionary<string, Board>>();
        private Dictionary<string,Board> ownerBoards = new Dictionary<string,Board>();
        public UserController userController;
        public int bId { get; }
        private static int BID = 0;

        public BoardController(UserController UC)
        {
            this.bId = BID;
            this.userController = UC;
            //BID += 1;
        }
        /// <summary>
        /// This method checks if a user has any board.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <returns>bool </returns>
        public bool UserHasAnyBoard(string userEmail) //checks if user has any board
        {
            if (BoardsOfUsers.ContainsKey(userEmail))
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
            
            if (this.BoardsOfUsers[userEmail].ContainsKey(boardName))
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
                            Board newBoard = new Board(boardName , this.bId , userEmail);
                            newBoard.SetOwner(userEmail); // set who the owner of the new board
                            newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                            this.ownerBoards.Add(userEmail,newBoard);
                            BID++;
                            this.BoardsOfUsers[userEmail].Add(boardName, newBoard);
                        }
                        else
                        {
                            throw new ArgumentException("USER CANNOT CREATE A THIS BOARD! USER HAS A BOARD WITH THIS NAME ALREADY");
                        }

                    }
                    else
                    {
                        Board newBoard = new Board(boardName , this.bId, userEmail);
                        newBoard.SetOwner(userEmail);// set who the owner of the new board
                        newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                        this.ownerBoards.Add(userEmail, newBoard);
                        BID++;
                        Dictionary<string, Board> board = new Dictionary<string, Board>();
                        board.Add(boardName, newBoard);
                        BoardsOfUsers.Add(userEmail, board);
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
        /// This method changes board owner.
        /// </summary>
        /// <returns>void.</returns>
        public void changeOwner(string currntUserEmail,string nextUserEmail , string boardName)
        {
            try
            {
                if((userController.IsLoggedIn(currntUserEmail)))
                {
                    if (ownerBoards[currntUserEmail].IsInListOfJoiners(nextUserEmail))
                    {
                        ownerBoards[currntUserEmail].SetOwner(nextUserEmail);
                        Board value = ownerBoards[currntUserEmail];
                        ownerBoards.Remove(currntUserEmail);
                        ownerBoards.Add(nextUserEmail,value);
                    }
                }
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
                    if (BoardsOfUsers.ContainsKey(userEmail))
                    {
                        Dictionary<string, Board> boards = BoardsOfUsers[userEmail];
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
                    if (ownerBoards[userEmail].name == boardName)
                    if (UserHasThisBoard(userEmail, boardName))
                    {
                        this.BoardsOfUsers[userEmail].Remove(boardName);

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
                        return this.BoardsOfUsers[userEmail][boardName];
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
