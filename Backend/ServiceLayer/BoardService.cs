﻿using IntroSE.Kanban.Backend.Buissnes_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        public BoardController boardController = new BoardController();
        /// <summary>
        /// This method creates a new board.
        /// </summary>
        /// <param name="name">The name of the board</param>
        /// <param name="userEmail">Email of the user. To connect between the new board to the user who made it.</param>
        /// <returns>Response with a command to create board, unless a board exists with the same name.</returns>
        public string CreateBoard(string name, string userEmail)
        {
            try
            {
                boardController.CreateBoard(name, userEmail);
                Response r = new Response(null, true);
                // return JsonSerializer.Serialize(true);
                return r.OKJson();
            }
            catch (Exception e)
            {
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
            Buissnes_Layer.Board b = boardController.GetBoard(email,boardName);
            try
            {
                b.AddTask(title, description, dueDate);

                Response r = new Response(null, b);
                return r.OKJson();
            }
            catch (Exception e)
            {
                Response r = new Response(e.Message, b);
                return r.BadJson(); //return exception when reached max task limit
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
                try
                {
                    b.ChangeState(taskId);
                    Response r = new Response(null, b);
                    return r.OKJson();
                }
                catch (Exception e)
                {
                    //RETURN BAD JASON
                    Response r = new Response(e.Message, b);
                    return r.BadJson();

                }
            }
            catch (Exception e)
            {
                Response r = new Response(e.Message, false);
                return r.BadJson();
            }
           

        }
        /// <summary>
        /// This method delete a board.
        /// </summary>
        /// <param name="name">The name of the board</param>
        /// <param name="userEmail">Email of the user. To identify which board nees to be deleted from which user. </param>
        /// <returns>Response with a command to delete board, unless doesn't exists a board with the same name.</returns>
        public string DeleteBoard(string name, string userEmail)
        {
            try
            {
                //NEED TO USE CHANGEsTATE
                boardController.DeleteBoard(userEmail, name);
                Response r = new Response(null, true);
                return r.OKJson();
            }
            catch (Exception e)
            {
                //RETURN BAD JASON
                Response r = new Response(e.Message, false);
                return r.BadJson();

            }
        }
    }
}
