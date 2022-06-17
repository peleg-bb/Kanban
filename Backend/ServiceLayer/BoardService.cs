using IntroSE.Kanban.Backend.Buissnes_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using log4net;
using log4net.Config;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// boardController instance should be changed to private!!!
    /// </summary>
    public class BoardService
    {

        public BoardController boardController; // Should be Private!!!!!
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BoardService(UserController UC)
        {
            this.boardController = new BoardController(UC);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting log!");
        }
        /// <summary>
        /// This method creates a new board.
        /// </summary>
        /// <param name="name">The name of the board</param>
        /// <param name="userEmail">Email of the user. To connect between the new board to the user who made it.</param>
        /// <returns>Response with a command to create board, unless  an error occurs.</returns>
        public string CreateBoard(string name, string userEmail)
        {
            try
            {
                boardController.CreateBoard(userEmail, name);
                String msg = String.Format("Board Added Successfully! to email :{0}", userEmail);
                log.Info(msg);
                // Response r = new Response(null, true);
                // return JsonSerializer.Serialize(true);
                //return r.OKJson();
                return "{}";
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                //RETURN BAD JASON
                Response r = new Response(e.Message, false);
                return r.BadJson();
            }

        }

        /// <summary>
        /// This method add new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>Response with user-email, unless an error occurs .</returns>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            if (boardController.userController.IsLoggedIn(email))
            {
                try
                {
                    Buissnes_Layer.Board b = boardController.GetBoard(email, boardName);
                    try
                    {
                        
                        b.AddTask(title, description, dueDate , email);
                        String msg = String.Format("task added Successfully! to board :{0}", boardName);
                        log.Info(msg); 
                        Response r = new Response((object)email);
                        return ToJson.toJson(r);

                    }
                    catch (Exception e)
                    {
                        log.Warn(e.Message);
                        //Response r = new Response(e.Message, false);
                        //return r.BadJson(); //return exception when reached max task limit
                        throw new Exception(e.Message);
                    }

                }
                catch (Exception e)
                {
                    //Response r = new Response(e.Message, false);
                    log.Warn(e.Message);
                    //return r.BadJson();
                    throw new Exception(e.Message);
                }

            }
            else
            {
                log.Warn("user not logged in");
                throw new ArgumentException("user not logged in");
            }
           
           
        }

        /// <summary>
        /// This method updates the state of the  task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>Response with a command to move the task state, unless doesn't exists a task with the same name.</returns>
        public string NextState(string email, string boardName, int taskId)
        {
            try
            {
                Buissnes_Layer.Board b = boardController.GetBoard(email, boardName);
                if (b.GetTask(taskId).Assignee == email)
                {
                    try
                    {
                        b.ChangeState(taskId);
                        Response r = new Response(null, true);
                        String msg = String.Format("task changed state Successfully! to state :{0}", b.GetTask(taskId).GetState());
                        log.Info(msg);

                        return r.OKJson();
                    }
                    catch (Exception e)
                    {
                        //RETURN BAD JASON
                        //Response r = new Response(e.Message, false);
                        log.Warn(e.Message);
                        throw new Exception(e.Message);

                        //return r.BadJson();

                    }
                }
                else
                {
                    log.Warn("ONLY ASSIGNEE OF THE TASK CAN CHANGE ITS STATE");
                    throw new ArgumentException("ONLY ASSIGNEE OF THE TASK CAN CHANGE ITS STATE");
                }
               
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
                //Response r = new Response(e.Message, false);
                //return r.BadJson();
            }
           

        }
        /// <summary>
        /// This method add a new member to a board.
        /// </summary>
        /// <param name="userEmailOwner">Email of the user owner.</param>
        /// <param name="name">The name of the board</param>
        /// <param name="userEmail">Email of the user added.</param>
        /// <returns>An empty response, unless an error occurs.</returns>
        public string JoinBoard(string userEmailOwner, string name, string userEmail)
        {
            if (boardController.userController.IsLoggedIn(userEmail))
            {
                try
                {
                    boardController.joinBoard(userEmailOwner, name, userEmail);
                    Response r = new Response(null, true);
                    String msg = String.Format("joined Board! userEmailOwner = {0} the board :{1}", userEmail, name);
                    log.Info(msg);

                    return ToJson.toJson(r);
                }
                catch (Exception e)
                {

                    //RETURN BAD JASON
                    //Response r = new Response(e.Message, false);
                    log.Warn(e.Message);
                    //return r.BadJson();
                    throw new ArgumentException(e.Message);
                }
            }
            else
            {
                log.Warn("user not logged in");
                throw new ArgumentException("user not logged in");
            }
            
        }
        /// <summary>
        /// This method remove a member from a board.
        /// </summary>
        /// <param name="userEmailOwner">Email of the user owner.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="userEmailLeaving">Email of the user removed.</param>
        /// <returns>An empty response, unless an error occurs.</returns>
        public string LeaveBoard(string userEmailOwner, string boardName, string userEmailLeaving)
        {
            if (boardController.userController.IsLoggedIn(userEmailLeaving))
            {
                try
                {
                    boardController.leaveBoard(userEmailOwner, boardName, userEmailLeaving);
                    String msg = String.Format("Left Board! userEmailOwner = {0} the board :{1}", userEmailLeaving, boardName);
                    log.Info(msg);
                    Response r = new Response((object)userEmailLeaving);
                    return ToJson.toJson(r);
                }
                catch (Exception e)
                {

                    //RETURN BAD JASON
                    //Response r = new Response(e.Message, false);
                    log.Warn(e.Message);
                    //return r.BadJson();
                    throw new ArgumentException(e.Message);
                }

            }
            else
            {
                log.Warn("user not logged in");
                throw new ArgumentException("user not logged in");
            }
        }

        /// <summary>
        /// This method assign a user from the board to a task.
        /// </summary>
        /// <param name="userEmailToAssign">Email of the user need to be assign to a task.</param>
        /// <param name="boardName">The name of the new board</param>
        /// <param name="columnOrdinal">The column number.????</param>
        /// <param name="userEmailAssigning">Email of the user assigning other user assign to a task.Must be logged in.</param> 
        /// <param name="taskId">The taskId of the task the userEmailAssigning will assigne </param>
        /// <returns>jason, unless an error occurs .</returns>
        public string AssignTask(string userEmailToAssign, string boardName,int columnOrdinal, string userEmailAssigning, int taskId)
        {
            if (boardController.userController.IsLoggedIn(userEmailAssigning))
            {
                try
                {
                    boardController.assignAssignee(userEmailToAssign, boardName, columnOrdinal, userEmailAssigning, taskId);
                    String msg = String.Format("task assignee assigned Successfully ! The assignee :{0}", userEmailToAssign);
                    log.Info(msg);
                    Response r = new Response((object)userEmailToAssign);
                    return ToJson.toJson(r);
                }
                catch (Exception e)
                {
                    log.Warn(e.Message);
                    throw new ArgumentException(e.Message);
                }
            }
            else
            {
                log.Warn("user not logged in");
                throw new ArgumentException("user not logged in");
            }


        }
        /// <summary>
        /// This method transfers a board ownership.
        /// </summary>
        /// <param name="currentOwnerEmail">Email of the current owner. Must be logged in</param>
        /// <param name="newOwnerEmail">Email of the new owner</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>An empty response, unless an error occurs </returns>
        public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            try
            {
                //NEED TO USE CHANGEsTATE
                boardController.switchOwnership(currentOwnerEmail, boardName , newOwnerEmail);
                Response r = new Response(null, true);
                String msg = String.Format("Transfer the Ownership!  new Owner userEmail = {0} of board :{1}", newOwnerEmail, boardName);
                log.Info(msg);

                return r.OKJson();
            }
            catch (Exception e)
            {

                //RETURN BAD JASON
                //Response r = new Response(e.Message, false);
                log.Warn(e.Message);
                //return r.BadJson();
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// This method delete a board.
        /// </summary>
        /// <param name="name">The name of the board</param>
        /// <param name="userEmail">Email of the user. To identify which board needs to be deleted from which user. </param>
        /// <returns>Response with a command to delete board, unless doesn't exists a board with the same name.</returns>
        public string DeleteBoard(string boardName, string userEmail)
        {
            try
            {
                //NEED TO USE CHANGEsTATE
                boardController.DeleteBoard(userEmail, boardName);
                Response r = new Response(null, true);
                String msg = String.Format("BoardService deleted! userEmail = {0} deleted board :{1}", userEmail, boardName);
                log.Info(msg);

                return r.OKJson();
            }
            catch (Exception e)
            {

                //RETURN BAD JASON
                //Response r = new Response(e.Message, false);
                log.Warn(e.Message);
                //return r.BadJson();
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string InProgress(string email)
        {
            try
            {
                List<Buissnes_Layer.Task> proCol = boardController.GetAllInPrograss(email);
                Response r = new Response(null, proCol);
                String msg = String.Format("got InProgress list! userEmail = {0} ", email);
                log.Info(msg);
                return ToJson.toJson(proCol);
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                throw new ArgumentException(e.Message);
                //Response r = new Response(e.Message, false);
                //return r.BadJson();
            }

        }

        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with  a list of the column's tasks, unless an error occurs </returns>
        public string GetColum(string email, string boardName, int columnOrdinal)
        {
            try
            {
                List<Buissnes_Layer.Task> allCol = boardController.GetBoard(email, boardName).GEtColList(columnOrdinal);
                //Response r = new Response(null, allCol);
                String msg = String.Format("Got the Column! columnOrdinal = {0} ", columnOrdinal);
                log.Info(msg);
                //return r.OKJson();
                return ToJson.toJson(allCol);
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                //Response r = new Response(e.Message, false);
                //return r.BadJson();
                throw new ArgumentException(e.Message);
            }

        }
        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>The string "{}", unless an error occurs </returns>
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                boardController.GetBoard(email, boardName).SetMaxTask(limit, columnOrdinal);
                Response r = new Response(null, true); 
                String msg = String.Format("Limit Column has been set! limit = {0}  at columnOrdinal :{1}", limit, columnOrdinal);
                log.Info(msg);
                return r.OKJson();
                
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                //Response r = new Response(e.Message, false);
                //return r.BadJson();
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column name value, unless an error occurs </returns>
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            try
            {
                string colVal = boardController.GetBoard(email, boardName).GetNameOrdinal(columnOrdinal);
                Response r = new Response(null, colVal);
                String msg = String.Format("Got the Column Name! columnOrdinal{0}", columnOrdinal);
                log.Info(msg);
                // return JsonSerializer.Serialize(true);
                // return r.OKJson();
                return colVal;
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                //Response r = new Response(e.Message, false);
                //return r.BadJson();
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column limit value, unless an error occurs </returns>
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            try
            {
                int colVal = boardController.GetBoard(email, boardName).GetMaxTask(columnOrdinal);
                Response r = new Response(null, colVal);
                String msg = String.Format("Got  the Column Limit! columnOrdinal = {0} ", columnOrdinal);
                log.Info(msg);
                // return JsonSerializer.Serialize(true);
                // return r.OKJson();
                return colVal.ToString();
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                //// Console.WriteLine(e);
                //Response r = new Response(e.Message, false);
                //// return JsonSerializer.Serialize(true);
                //return r.BadJson();
                throw new ArgumentException(e.Message);
            }

        }

        public void LoadData()
        {
            this.boardController.LoadData();
        }
        public void DeleteAllData()
        {
            this.boardController.DeleteAllData();
        }
    }
  
}
