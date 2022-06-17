﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.DataAccessLayer.Mappers;
using log4net;
using log4net.Config;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class BoardController
    {

        private Dictionary<string, Dictionary<string,Board>> BoardsOfUsers = new Dictionary<string, Dictionary<string, Board>>();
        private Dictionary<string,List<string>> ownerBoards = new Dictionary<string, List<string>>();
        public UserController userController;
        public int BID
        {
            get
            {
                return this.bID;

            }

            set
            {
                this.bID = value;
            }
        }
        
        private int bID;
        private BoardDTOMapper boardDTOMapper;
        private Dictionary<int,Board> boardById = new Dictionary<int,Board>();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BoardController(UserController UC)
        { 
            this.userController = UC;
            this.boardDTOMapper = new BoardDTOMapper();
            // this.boardDTOMapper.LoadData(); Do NOT activate! Ask Peleg why (constructors must not load data - if they throw an exception the entire program fails)
            this.BID = boardDTOMapper.GetCount();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting log!");
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

        public Board GetBoardById( int boardID)
        {
            return this.boardById[boardID];
        }

        public List<int> GetUserBList(string userEmail)
        {
            if (userController.IsLoggedIn(userEmail))
            {
                List<int> listOfUserBoard = new List<int>();
                foreach (var i in this.BoardsOfUsers[userEmail])
                {
                    listOfUserBoard.Add(i.Value.BoardID);
                }
                return listOfUserBoard;
            }
            else
            {
                log.Warn("user not logged in");
                throw new ArgumentException("user not logged in");
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
                            if (isOwnerOfAnyBoard(userEmail))
                            {
                                Board newBoard = new Board(boardDTOMapper.CreateBoard(userEmail, boardName));
                                //new Board(boardName, this.bId, userEmail); - old constructor, do not use
                                newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                                this.boardById.Add(this.bId+1 ,newBoard);
                                this.ownerBoards[userEmail].Add(newBoard.name);
                                BID++;
                                this.BoardsOfUsers[userEmail].Add(boardName, newBoard);
                            }
                            else
                            {
                                Board newBoard = new Board(boardDTOMapper.CreateBoard(userEmail, boardName));
                                List<string> listBoard= new List<string>();
                                listBoard.Add(newBoard.name);
                                newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                                this.ownerBoards.Add(userEmail, listBoard);
                                BID++;
                                this.BoardsOfUsers[userEmail].Add(boardName, newBoard);
                            }
                        }
                        else
                        {
                            log.Warn("USER CANNOT CREATE THIS BOARD! USER HAS A BOARD WITH THIS NAME ALREADY");
                            throw new ArgumentException("USER CANNOT CREATE THIS BOARD! USER HAS A BOARD WITH THIS NAME ALREADY");
                        }

                    }
                    else
                    {
                        Board newBoard = new Board(boardDTOMapper.CreateBoard(userEmail, boardName));
                        List<string> listBoard = new List<string>();
                        listBoard.Add(newBoard.name);
                        newBoard.AddToJoinList(userEmail);// the owner is a joiner as well
                        this.ownerBoards.Add(userEmail, listBoard);
                        BID++;
                        Dictionary<string, Board> board = new Dictionary<string, Board>();
                        board.Add(boardName, newBoard);
                        BoardsOfUsers.Add(userEmail, board);
                    }
                    String msg = String.Format("Board created Successfully in BuissnesLayer! The Board {0} to email :{1}", boardName, userEmail);
                    log.Info(msg);
                }
                else
                {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
            }
            
        }
        /// <summary>
        /// This method assign a user from the board to a task.
        /// </summary>
        /// <param name="userEmailToAssign">Email of the user need to be assign to a task.</param>
        /// <param name="boardName">The name of the new board</param>
        /// <param name="userEmailAssigning">Email of the user assigning other user assign to a task.Must be logged in.</param> 
        /// <param name="taskId">The taskId of the task the userEmailAssigning will assigne </param>
        /// <returns>void, unless an error occurs .</returns>
        public void assignAssignee(string userEmailToAssign , string boardName ,int columnOrdinal, string userEmailAssigning ,int taskId )
        {
            try
            {
                if ((userController.IsLoggedIn(userEmailAssigning)))
                {
                    Board b = GetBoard(userEmailAssigning, boardName);
                    if (columnOrdinal != 2 && columnOrdinal != null)
                    {
                        if (b.IsInListOfJoiners(userEmailAssigning))
                        {
                            b.GetTask(taskId).EditAssignee(userEmailToAssign);
                            String msg = String.Format("task assignee assigned Successfully in BuissnesLayer! The assignee :{0}", userEmailToAssign);
                            log.Info(msg);
                        }
                        else
                        {
                            log.Warn("USER WHO IS NOT A MEMBER OF THE BOARD CAN NOT BE ASSIGNED TO TASK !");
                            throw new Exception("USER WHO IS NOT A MEMBER OF THE BOARD CAN NOT BE ASSIGNED TO TASK !");
                        }
                    }
                    else
                    {
                        log.Warn("NOT IN A COLUMN THAT THE USER CAN BE ASSIGN AT !");
                        throw new Exception("NOT IN A COLUMN THAT THE USER CAN BE ASSIGN AT !");
                    }
                }
                else
                {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
            }


        }
        /// <summary>
        /// This method add a new member to a board.
        /// </summary>
        /// <param name="userEmailJoiner">Email of the user added.</param>
        /// /// <param name="boardId">The id of the board</param>
        /// <returns>An empty response, unless an error occurs.</returns>
        public void joinBoard(int boardId,string userEmailJoiner)
        {
            try
            {
                string userEmailOwner = GetBoardById(boardId).GetOwner();
                string boardName = GetBoardById(boardId).name;
                if ((userController.IsLoggedIn(userEmailJoiner)))
                {
                    if (!UserHasThisBoard(userEmailJoiner, boardName))
                    {
                        BoardsOfUsers[userEmailOwner][boardName].AddToJoinList(userEmailJoiner);
                        BoardsOfUsers[userEmailJoiner].Add(boardName, BoardsOfUsers[userEmailOwner][boardName]);
                        boardDTOMapper.AddUserToBoard(BoardsOfUsers[userEmailOwner][boardName].BoardID, userEmailJoiner);
                        String msg = String.Format("joined Board Successfully in BuissnesLayer! userEmailOwner = {0} the board :{1}", userEmailOwner, boardName);
                        log.Info(msg);
                    }
                    else
                    {
                        log.Warn("user already joined that board");
                        throw new ArgumentException("user already joined that board");
                    }
                }
                else {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
            }
            
        }
        /// <summary>
        /// This method remove a member from a board.
        /// </summary>
        /// <param name="userEmailOwner">Email of the user owner.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="userEmailLeaving">Email of the user removed.</param>
        /// <returns>An empty response, unless an error occurs.</returns>
        public void leaveBoard(int boardId, string userEmailLeaving)
        {
            try
            {
                string userEmailOwner = GetBoardById(boardId).GetOwner();
                string boardName = GetBoardById(boardId).name;
                if ((userController.IsLoggedIn(userEmailLeaving)))
                {
                    if (UserHasThisBoard(userEmailLeaving, boardName))
                    {
                        if (!ownerBoards[userEmailLeaving].Contains(boardName))
                        {
                            BoardsOfUsers[userEmailOwner][boardName].leaveTasks(userEmailLeaving); // all joiner take become unAssigned
                            BoardsOfUsers[userEmailOwner][boardName].DeleteFromJoinList(userEmailLeaving);
                            BoardsOfUsers[userEmailLeaving].Remove(boardName); // delete to userEmailLeaving from boards
                            this.boardDTOMapper.RemoveUserFromBoard(BoardsOfUsers[userEmailOwner][boardName].BoardID, userEmailLeaving);
                            String msg = String.Format("userEmail Left Successfully in BuissnesLayer! userEmailOwner = {0} the board :{1}", userEmailLeaving, boardName);
                            log.Info(msg);
                        }
                        else
                        {
                            log.Warn("OWNER CAN'T LEAVE HIS OWN BOARD!");
                            throw new ArgumentException("OWNER CAN'T LEAVE HIS OWN BOARD!");
                        }

                    }
                    else
                    {
                        log.Warn("user doesn't have that board");
                        throw new ArgumentException("user doesn't have that board");
                    }
                }
                {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
            }
          
        }
        /// <summary>
        /// This method transfers a board ownership.
        /// </summary>
        /// <param name="userEmailOwner">Email of the current owner. Must be logged in</param>
        /// <param name="userEmailFutureOwner">Email of the new owner</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns> void, unless an error occurs </returns>
        public void  switchOwnership(string userEmailOwner, string boardName, string userEmailFutureOwner)
        {
            try
            {
                if ((userController.IsLoggedIn(userEmailOwner)))
                {
                    if (ownerBoards[userEmailOwner].Contains(boardName) && BoardsOfUsers[userEmailOwner][boardName].IsInListOfJoiners(userEmailFutureOwner))
                    {
                        BoardsOfUsers[userEmailOwner][boardName].SetOwner(userEmailFutureOwner);
                        if (isOwnerOfAnyBoard(userEmailFutureOwner)) // checks if userEmailFutureOwner is owner of other board
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
                        String msg = String.Format("Transfer the Ownership Successfully in BuissnesLayer!  new Owner userEmail = {0} of board :{1}", userEmailFutureOwner, boardName);
                        log.Info(msg);
                    }
                    else
                    {
                        log.Warn("not the owner of this board!");
                        throw new ArgumentException("not the owner of this board!");
                    }
                }
                {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }

            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
            }
        }
        // /// <summary>
        // /// This method changes board owner.
        // /// </summary>
        // /// <returns>void.</returns>
        // public void changeOwner(string currntUserEmail,string nextUserEmail , string boardName)
        // {
        //     try
        //     {
        //         if((userController.IsLoggedIn(currntUserEmail)))
        //         {
        //             if (BoardsOfUsers[currntUserEmail][boardName].IsInListOfJoiners(nextUserEmail))
        //             {
        //                 BoardsOfUsers[currntUserEmail][boardName].SetOwner(nextUserEmail);
        //                 List<string> value = ownerBoards[currntUserEmail];
        //                 ownerBoards.Remove(currntUserEmail);
        //                 ownerBoards.Add(nextUserEmail,value);
        //             }
        //         }
        //         else {
        //             throw new ArgumentException("user not logged in");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         throw new ArgumentException(e.Message);
        //     }
        // }

        /// <summary>
        /// This method returns all the In progress tasks the user assigned to.
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
                            taskInProgList.AddRange(boards[item].GetInProgressByAssignee(userEmail));
                        }
                        return taskInProgList;
                    }
                    else
                    {
                        List<Task> taskInProgList = new List<Task>();
                        return taskInProgList;
                    }

                    String msg = String.Format("got InProgress list Successfully in BuissnesLayer! userEmail = {0} ", userEmail);
                    log.Info(msg);
                }
                else
                {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
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
                            this.ownerBoards[userEmail].Remove(boardName);
                            this.boardById.Remove(GetBoard(userEmail, boardName).BoardID);
                            // Logically speaking - boards are recognized by ID.
                            // However, the GradingService recognizes them by
                            // owner email and board name as a double key. 
                            // I believe that ID's 
                            this.boardDTOMapper.DeleteBoard(userEmail, boardName);
                            String msg = String.Format("Deleted Successfully in BuissnesLayer! userEmail = {0} deleted board :{1}", userEmail, boardName);
                            log.Info(msg);
                        }
                        else
                        {
                            log.Warn("THIS USER ISN'T THE OWNER OF THE BOARD ! ");
                            throw new ArgumentException("THIS USER ISN'T THE OWNER OF THE BOARD ! ");
                        }

                    }
                    else
                    {
                        log.Warn("BOARD IS NOT EXIST AT THIS USER ! ");
                        throw new ArgumentException("BOARD IS NOT EXIST AT THIS USER ! ");
                    }

                }
                else
                {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
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
                        String msg = String.Format("Got board Successfully in BuissnesLayer! userEmail = {0}  board ={1}", userEmail, boardName);
                        log.Info(msg);
                    }
                    else
                    {
                        log.Warn("BOARD IS NOT EXIST AT THIS USER ! ");
                        throw new ArgumentException("BOARD IS NOT EXIST AT THIS USER ! ");
                    }
                    
                }
                else
                {
                    log.Warn("user not logged in");
                    throw new ArgumentException("user not logged in");
                }
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
            }
          
        }
        /// <summary>
        /// This method LoadData to boardDTO.
        /// </summary>
        /// <returns>void </returns>
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
            String msg = String.Format("LoadData Successfully in BuissnesLayer!");
            log.Info(msg);
        }
        /// <summary>
        /// This method DeleteAllData to boardDTO.
        /// </summary>
        /// <returns>void </returns>
        public void DeleteAllData()
        {
            this.boardDTOMapper.DeleteAllData();
            this.BoardsOfUsers.Clear();
            this.ownerBoards.Clear();
            String msg = String.Format(" DeleteAllData Successfully in BuissnesLayer!");
            log.Info(msg);
        }
        /// <summary>
        /// This method get a specific Task to the specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <param name="taskId">The id of new task</param>
        /// <returns>Task, unless an error occurs .</returns>
        public Task GetTask(string email, string boardName, int taskId)
        {
            try
            {
                return GetBoard(email, boardName).GetTask(taskId);
                String msg = String.Format("Got Task Successfully in BuissnesLayer!");
                log.Info(msg);
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
            }
        }

    }
}
