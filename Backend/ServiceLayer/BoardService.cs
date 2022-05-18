﻿using IntroSE.Kanban.Backend.Buissnes_Layer;
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
    public class BoardService
    {

        public BoardController boardController;
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
            try
            {
                Buissnes_Layer.Board b = boardController.GetBoard(email, boardName);
                try
                {
                    b.AddTask(title, description, dueDate);

                    Response r = new Response(null, email);
                    return r.OKJson();
                }
                catch (Exception e)
                {
                    Response r = new Response(e.Message, false);
                    return r.BadJson(); //return exception when reached max task limit
                }

            }
            catch (Exception e)
            {
                Response r = new Response(e.Message, false);
                log.Warn(e.Message);

                return r.BadJson();
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
                    Response r = new Response(null, true);
                    String msg = String.Format("task changed state Successfully! to state :{0}", b.GetTask(taskId).GetState());
                    log.Info(msg);

                    return r.OKJson();
                }
                catch (Exception e)
                {
                    //RETURN BAD JASON
                    Response r = new Response(e.Message, false);
                    log.Warn(e.Message);

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
                String msg = String.Format("BoardService deleted! userEmail = {0} deleted board :{1}", userEmail, name);
                log.Info(msg);

                return r.OKJson();
            }
            catch (Exception e)
            {

                //RETURN BAD JASON
                Response r = new Response(e.Message, false);
                log.Warn(e.Message);
                return r.BadJson();

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
                //List<Buissnes_Layer.Task> proCol = boardController.GetAllInPrograss(email);
                 List<Buissnes_Layer.Task> proCol = boardController.GetAllInPrograss(email);
                Response r = new Response(null, proCol);
                return r.OKJson();
            }
            catch (Exception e)
            {

                Response r = new Response(e.Message, false);
                return r.BadJson();
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
                Response r = new Response(null, allCol);
                return r.OKJson();
            }
            catch (Exception e)
            {

                Response r = new Response(e.Message, false);
                return r.BadJson();
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
                return r.OKJson();
            }
            catch (Exception e)
            {
                Response r = new Response(e.Message, false);
                return r.BadJson();
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
                // return JsonSerializer.Serialize(true);
               // return r.OKJson();
               return colVal;
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                Response r = new Response(e.Message, false);
                // return JsonSerializer.Serialize(true);
                return r.BadJson();
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
                // return JsonSerializer.Serialize(true);
               // return r.OKJson();
               return colVal.ToString();
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                Response r = new Response(e.Message, false);
                // return JsonSerializer.Serialize(true);
                return r.BadJson();
            }

        }
    }
  
}
