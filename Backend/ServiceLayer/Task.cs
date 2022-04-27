using System;
using System.Text.Json;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
namespace IntroSE.Kanban.Backend.ServiceLayer

{
    public class Task 
    {
        /// <summary>
        /// This method creates a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>Response with user-email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string createTask(string email, string boardName, string title,string description, string dueDate)
        {
            throw new NotImplementedException();
            
        }
        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="newTitle">New title for the task</param>
        /// <returns>The string "{\"Title\" : \"newTitle\", \"Description\" : \"description\", \"DueDate\" : \"21.04.22\"}", unless an error occurs </returns>
        public string editTitle(string email, int taskId,string newTitle)
        {

            throw new NotImplementedException();

        }
        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="newDescription">New description for the task</param>
        /// <returns>The string "{\"Title\" : \"title\", \"Description\" : \"newDescription\", \"DueDate\" : \"21.04.22\"}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string editDescription(string email, int taskId,string newDescription)
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="newDueDate">The new due date of the column</param>
        /// <returns>The string "{\"Title\" : \"title\", \"Description\" : \"description\", \"DueDate\" : \"newDueDate"}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string editDueDate(string email, int taskId,string newDueDate)
        {
            throw new NotImplementedException();

        }
    }
}