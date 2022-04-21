﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{

    public class Board
    {
        /// <summary>
        /// This method creates a new board.
        /// </summary>
        /// <param name="name">The name of the board</param>
        /// <param name="userEmail">Email of the user. To connect between the new board to the user who made it.</param>
        /// <returns>Response with a command to create board, unless a board exists with the same name.</returns>
        public string CreateBoard( string name, string userEmail)
        {
            throw new NotImplementedException();

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
        public string AddTask(string email, string boardName, string title, string description, string dueDate)
        {

            throw new NotImplementedException();

        }
        /// <summary>
        /// This method delete the a task from .
        /// </summary>
        /// <param name="title">title of the task the user would like to delete. Must be logged in the spoken noard</param>
        /// <returns>Response with a command to delete task, unless doesn't exists a task with the same name.</returns>
        public string DeleteTask(string title)
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// This method updates the state of the  task.
        /// </summary>
        /// <param name="taskName">the currnt state of the task, and will be change to the next possible state.</param>
        /// <returns>Response with a command to move the task state, unless doesn't exists a task with the same name.</returns>
        public string NextState(string taskName)
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// This method delete a board.
        /// </summary>
        /// <param name="name">The name of the board</param>
        /// <param name="userEmail">Email of the user. To identify which board nees to be deleted from which user. </param>
        /// <returns>Response with a command to delete board, unless doesn't exists a board with the same name.</returns>
        public string DeleteBoard(string name, string userEmail)
        {
            throw new NotImplementedException();

        }
    }
}
