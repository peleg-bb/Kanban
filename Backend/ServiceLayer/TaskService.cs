using System;
using System.Text.Json;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using log4net.Util;
using Newtonsoft.Json;
using IntroSE.Kanban.Backend.Buissnes_Layer;
using Task = IntroSE.Kanban.Backend.Buissnes_Layer.Task;

namespace IntroSE.Kanban.Backend.ServiceLayer

{
    public class TaskService
    {
        private BoardController boardController;

        public TaskService(BoardController BC)
        {
            this.boardController = BC;
        }

        ///// <summary>
        ///// This method creates a new task.
        ///// </summary>
        ///// <param name="email">Email of the user. The user must be logged in.</param>
        ///// <param name="boardName">The name of the board</param>
        ///// <param name="title">Title of the new task</param>
        ///// <param name="description">Description of the new task</param>
        ///// <param name="dueDate">The due date if the new task</param>
        ///// <returns>Response with user-email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        //public string CreateTask(string email, string boardName, string title,string description, string dueDate)
        //{
        //    if (Connections.IsLoggedIn(email))
        //    {
        //        if(BoardService.)
        //    }

        //}
        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="newTitle">New title for the task</param>

        /// <returns>The string "{\"Title\" : \"newTitle\", \"Description\" : \"description\", \"DueDate\" : \"21.04.22\"}", unless an error occurs </returns>
        public string EditTitle(string email, string boardName, int taskId, string newTitle)
        {
            try
            {
                Task task = boardController.GetBoard(email, boardName).GetTask(taskId);
                try
                {
                    task.EditTitle(newTitle);
                    Response response = new Response(null, task);
                    return response.OKJson();
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, task);
                    return response.BadJson();
                }
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, false);
                return response.BadJson();
            }
        }

        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="newDescription">New description for the task</param>
        /// <returns>The string "{\"Title\" : \"title\", \"Description\" : \"newDescription\", \"DueDate\" : \"21.04.22\"}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string EditDescription(string email, string boardName, int taskId, string newDescription)
        {
            try
            {
                Task task = boardController.GetBoard(email, boardName).GetTask(taskId);
                try
                {
                    task.EditDescription(newDescription);
                    Response response = new Response(null, task);
                    return response.OKJson();
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, task);
                    return response.BadJson();
                }
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, false);
                return response.BadJson();
            }
        }

        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="newDueDate">The new due date of the column</param>
        /// <returns>The string "{\"Title\" : \"title\", \"Description\" : \"description\", \"DueDate\" : \"newDueDate"}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string EditDueDate(string email, string boardName, int taskId, DateTime newDueDate)
        {
            try
            {
                Task task = boardController.GetBoard(email, boardName).GetTask(taskId);
                try
                {
                    task.EditDueDate(newDueDate);
                    Response response = new Response(null, task);
                    return response.OKJson();
                }
                catch (Exception ex)
                {
                    Response response = new Response(ex.Message, task);
                    return response.BadJson();
                }
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, false);
                return response.BadJson();
            }
        }
    }
}