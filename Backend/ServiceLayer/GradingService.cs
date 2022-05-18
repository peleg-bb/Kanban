using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using IntroSE.Kanban.Backend.Buissnes_Layer;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// A class for grading your work <boardService>ONLY</boardService>. The methods are not using good SE practices and you should <boardService>NOT</boardService> infer any insight on how to write the service layer/business layer. 
    /// <para>
    /// Each of the class' methods should return a JSON string with the following structure (see <see cref="System.Text.Json"/>):
    /// <code>
    /// {
    ///     "ErrorMessage": &lt;string&gt;,
    ///     "ReturnValue": &lt;object&gt;
    /// }
    /// </code>
    /// Where:
    /// <list type="bullet">
    ///     <item>
    ///         <term>ReturnValue</term>
    ///         <description>
    ///             The return value of the function.
    ///             <para>
    ///                 The value may be either a <paramref name="primitive"/>, a <paramref name="Task"/>, or an array of of them. See below for the definition of <paramref name="Task"/>.
    ///             </para>
    ///             <para>If the function does not return a value or an exception has occorred, then the field is undefined.</para>
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>ErrorMessage</term>
    ///         <description>If an exception has occorred, then this field will contain a string of the error message.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>
    /// The structure of the JSON of a TaskService, is:
    /// <code>
    /// {
    ///     "Id": &lt;int&gt;,
    ///     "CreationTime": &lt;DateTime&gt;,
    ///     "Title": &lt;string&gt;,
    ///     "Description": &lt;string&gt;,
    ///     "DueDate": &lt;DateTime&gt;
    /// }
    /// </code>
    /// </para>
    /// </summary>
    public class GradingService
    {
        private UserController userController;
        private BoardService boardService;
        public UserService userService;
        private TaskService taskService;
        public GradingService()
        {
            this.userController = new UserController();
            this.boardService = new BoardService(this.userController);
            this.userService = new UserService(this.userController);
            this.taskService = new TaskService(this.boardService.boardController);
        }


        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Register(string email, string password)
        {
            try
            {
                userController.CreateUser(email, password);
                Response response = new Response(null, true);
                return "{}";
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }
        }


        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>Response with user email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Login(string email, string password)
        {
            try
            {
                userController.Login(email, password);
                return email;
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }
        }


        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string Logout(string email)
        {
            try
            {
                userController.Logout(email);
                return "{}";
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }
        }
        
        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                boardService.LimitColumn(email, boardName, columnOrdinal, limit);
                return "{}";
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }
        }

        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column limit value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            try
            {
                string limVal= boardService.GetColumnLimit(email, boardName, columnOrdinal);
                return limVal;
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }

        }


        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column name value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            try
            {
                string colName = boardService.GetColumnName(email, boardName, columnOrdinal);
                return colName;
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }
            
        }


        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>Response with user-email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            try
            {
                boardService.AddTask(email, boardName, title, description, dueDate);
                return email;
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }

            
                
        }


        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1 || columnOrdinal == 2)
            {
                try
                {
                    if (boardService.boardController.GetBoard(email, boardName).GetTask(taskId).GetState() ==
                        columnOrdinal)
                    {
                        try
                        {
                            taskService.EditDueDate(email, boardName, taskId, dueDate);
                            return "{}";
                        }
                        catch (Exception e)
                        {
                            Response response = new Response(e.Message, null);
                            return ToJson.toJson(response);
                        }
                    }
                    else
                    {
                        Response response = new Response("Not the right colomn number", null);
                        return ToJson.toJson(response);
                    }

                }
                catch (Exception e)
                {
                    Response response = new Response(e.Message, null);
                    return ToJson.toJson(response);
                }

            }
            else
            {
                Response response = new Response("Not available colomn number", null);
                return ToJson.toJson(response);
            }


        }


        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1 || columnOrdinal == 2)
            {
                try
                {
                    if (boardService.boardController.GetBoard(email, boardName).GetTask(taskId).GetState() ==
                        columnOrdinal)
                    {
                        try
                        {
                            taskService.EditTitle(email, boardName, taskId, title);
                            return "{}";
                        }
                        catch (Exception e)
                        {
                            Response response = new Response(e.Message, null);
                            return ToJson.toJson(response);
                        }
                    }
                    else
                    {
                        Response response = new Response("Not the right colomn number", null);
                        return ToJson.toJson(response);
                    }

                }
                catch (Exception e)
                {
                    Response response = new Response(e.Message, null);
                    return ToJson.toJson(response);
                }

            }
            else
            {
                Response response = new Response("Not available colomn number", null);
                return ToJson.toJson(response);
            }


        }


        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1 || columnOrdinal == 2)
            {
                try
                {
                    if (boardService.boardController.GetBoard(email, boardName).GetTask(taskId).GetState() ==
                        columnOrdinal)
                    {
                        try
                        {
                            taskService.EditDescription(email, boardName, taskId, description);
                            return "{}";
                        }
                        catch (Exception e)
                        {
                            Response response = new Response(e.Message, null);
                            return ToJson.toJson(response);
                        }
                    }
                    else
                    {
                        Response response = new Response("Not the right colomn number", null);
                        return ToJson.toJson(response);
                    }

                }
                catch (Exception e)
                {
                    Response response = new Response(e.Message, null);
                    return ToJson.toJson(response);
                }

            }
            else
            {
                Response response = new Response("Not available colomn number", null);
                return ToJson.toJson(response);
            }


        }


        /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                boardService.NextState(email, boardName, taskId);
                return "{}";
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }


        }


        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with  a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            try
            {
                return boardService.GetColum(email, boardName, columnOrdinal);
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }

        }


        /// <summary>
        /// This method adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AddBoard(string email, string name)
        {
            try
            {
                boardService.boardController.CreateBoard(email, name);
                return "{}";
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }
            
           
            
        }


        /// <summary>
        /// This method removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string RemoveBoard(string email, string name)
        {
            try
            {
                boardService.DeleteBoard(name, email);
                return "{}";
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response); 
            }
            
              
        }


        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string InProgressTasks(string email)
        {
            try
            {
                return boardService.InProgress(email);
                 
            }
            catch (Exception e)
            {
                Response response = new Response(e.Message, null);
                return ToJson.toJson(response);
            }
            
        }
    }
}
