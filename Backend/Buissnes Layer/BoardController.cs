using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.DataAccessLayer.Mappers;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class BoardController
    {

        private Dictionary<string, Dictionary<string,Board>> BoardsOfUsers = new Dictionary<string, Dictionary<string, Board>>();
        private Dictionary<string,List<string>> ownerBoards = new Dictionary<string, List<string>>();
        public UserController userController;
        public int bId { get; }
        private int BID;
        private BoardDTOMapper boardDTOMapper;
        private List<Board> boards;

        public BoardController(UserController UC)
        {
            this.bId = BID;
            this.userController = UC;
            this.boardDTOMapper = new BoardDTOMapper();
            // this.boardDTOMapper.LoadData(); Do NOT activate! Ask Peleg why (constructors must not load data - if they throw an exception the entire program fails)
            this.BID = boardDTOMapper.GetCount();
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

        public bool isOwnerOfAnyBoard(string userEmail)
        {
            if (ownerBoards.ContainsKey(userEmail))
            {
                return true;
            }

            return false;
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
                            if (isOwnerOfAnyBoard(userEmail))
                            {
                                Board newBoard = new Board(boardName, this.bId, userEmail);
                                newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                                this.ownerBoards[userEmail].Add(newBoard.name);
                                BID++;
                                this.BoardsOfUsers[userEmail].Add(boardName, newBoard);
                                // New:
                                this.boardDTOMapper.CreateBoard(userEmail, boardName);
                            }
                            else
                            {
                                Board newBoard = new Board(boardName, this.bId, userEmail);
                                List<string> listBoard= new List<string>();
                                listBoard.Add(newBoard.name);
                                newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                                this.ownerBoards.Add(userEmail, listBoard);
                                BID++;
                                this.BoardsOfUsers[userEmail].Add(boardName, newBoard);
                                // New
                                this.boardDTOMapper.CreateBoard(userEmail, boardName);
                            }

                        }
                        else
                        {
                            throw new ArgumentException("USER CANNOT CREATE A THIS BOARD! USER HAS A BOARD WITH THIS NAME ALREADY");
                        }

                    }
                    else
                    {
                        Board newBoard = new Board(boardName , this.bId, userEmail);
                        List<string> listBoard = new List<string>();
                        listBoard.Add(newBoard.name);
                        newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                        this.ownerBoards.Add(userEmail, listBoard);
                        BID++;
                        Dictionary<string, Board> board = new Dictionary<string, Board>();
                        board.Add(boardName, newBoard);
                        BoardsOfUsers.Add(userEmail, board);
                        //New:
                        this.boardDTOMapper.CreateBoard(userEmail, boardName);
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

        public void joinBoard(string userEmailOwner, string boardName, string userEmailJoiner)
        {
            try
            {
                if ((userController.IsLoggedIn(userEmailOwner)))
                {
                    if (!UserHasThisBoard(userEmailJoiner, boardName))
                    {
                        BoardsOfUsers[userEmailOwner][boardName].AddToJoinList(userEmailJoiner);
                        BoardsOfUsers[userEmailJoiner].Add(boardName, BoardsOfUsers[userEmailOwner][boardName]);
                    }
                    else
                    {
                        throw new ArgumentException("user already joined that board");
                    }
                }
                else {
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            
        }
        public void leaveBoard(string userEmailOwner, string boardName, string userEmailJoiner)
        {
            try
            {
                if ((userController.IsLoggedIn(userEmailOwner)))
                {
                    if (UserHasThisBoard(userEmailJoiner, boardName))
                    {
                        if (!ownerBoards[userEmailJoiner].Contains(boardName))
                        {
                            BoardsOfUsers[userEmailOwner][boardName].leaveTasks(userEmailJoiner); // all joiner taka become unAssigned
                            BoardsOfUsers[userEmailOwner][boardName].DeleteFromJoinList(userEmailJoiner);
                            BoardsOfUsers[userEmailOwner].Remove(boardName);
                        }
                        else
                        {
                            throw new ArgumentException("OWNER CAN'T LEAVE HIS OWN BOARD!");
                        }

                    }
                    else
                    {
                        throw new ArgumentException("user doesn't have that board");
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

        public void  switchOwnership(string userEmailOwner, string boardName, string userEmailFutureOwner)
        {
            try
            {
                if ((userController.IsLoggedIn(userEmailOwner)))
                {
                    if (ownerBoards[userEmailOwner].Contains(boardName) && BoardsOfUsers[userEmailOwner][boardName].IsInListOfJoiners(userEmailFutureOwner))
                    {
                        BoardsOfUsers[userEmailOwner][boardName].SetOwner(userEmailFutureOwner);
                        if (isOwnerOfAnyBoard(userEmailFutureOwner))
                        {
                            ownerBoards[userEmailFutureOwner].Add(boardName);
                            ownerBoards[userEmailOwner].Remove(boardName);
                        }
                        else
                        {
                            List<string> listBoard = new List<string>();
                            listBoard.Add(boardName);
                            ownerBoards.Add(userEmailFutureOwner, listBoard);
                            ownerBoards[userEmailOwner].Remove(boardName);
                        }

                    }
                    else
                    {
                        throw new ArgumentException("not the owner of this board!");
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
        /// This method changes board owner.
        /// </summary>
        /// <returns>void.</returns>
        public void changeOwner(string currntUserEmail,string nextUserEmail , string boardName)
        {
            try
            {
                if((userController.IsLoggedIn(currntUserEmail)))
                {
                    if (BoardsOfUsers[currntUserEmail][boardName].IsInListOfJoiners(nextUserEmail))
                    {
                        BoardsOfUsers[currntUserEmail][boardName].SetOwner(nextUserEmail);
                        List<string> value = ownerBoards[currntUserEmail];
                        ownerBoards.Remove(currntUserEmail);
                        ownerBoards.Add(nextUserEmail,value);
                    }
                }
                else {
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
                    if (UserHasThisBoard(userEmail, boardName))
                    {
                        if (BoardsOfUsers[userEmail][boardName].GetOwner() == userEmail)
                        {
                            this.BoardsOfUsers[userEmail].Remove(boardName);
                            // Logically speaking - boards are recognized by ID.
                            // However, the GradingService recognizes them by
                            // owner email and board name as a double key. 
                            // I believe that ID's 
                            this.boardDTOMapper.DeleteBoard(userEmail, boardName);
                        }
                        else
                        {
                            throw new ArgumentException("THIS USER ISN'T THE OWNER OF THE BOARD ! ");
                        }

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

        public void LoadData()
        {
            // this.boardsList = 
            List<BoardDTO> boardDTOs = this.boardDTOMapper.LoadBoards();
            foreach (var boardDTO in boardDTOs)
            {
                boardDTO.LoadBoard();
                // Create Board object
                // Load info based on boardDTOs (don't forget board_count)
            }
        }

        public void DeleteAllData()
        {
            this.boardDTOMapper.DeleteAllData();
            this.BoardsOfUsers.Clear();
            this.ownerBoards.Clear();
        }

        public Task GetTask(string email, string boardName, int taskId)
        {
           return GetBoard(email, boardName).GetTask(taskId);
        }

    }
}
