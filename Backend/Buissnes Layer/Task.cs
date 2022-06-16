﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Reflection;
using System.IO;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    [Serializable, DataContract]
    public class Task
    {
        [DataMember]
        public int Id { get; }
        [DataMember(Order = 1 )]
        public readonly DateTime CreationTime;
        [DataMember(Order = 1)]
        private string Title { set; get; }
        [DataMember(Order = 1)]
        private string Description { set; get; }
        [DataMember(Order = 1)]
        private DateTime DueDate { set; get; }
        [JsonIgnore]
        private int State;
        [JsonIgnore]
        private static int ID = 1;
        private int BoardId;

        public string Assignee { private set; get; }
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Exception ex = new ArgumentException();

        public Task (string title, DateTime dueDate, int boardId, string description = "")
        {
            this.Id = ID;
            this.CreationTime = DateTime.Today;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.State = 0;
            ID += 1;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting log!");
            BoardId = boardId;
            // Do NOT Load Data!

        }
        /// <summary>
        /// This method edit the title of a task
        /// </summary>
        ///  <param name="newTitle">The new Title of the task</param>
        /// <returns> nothing, just change it in the tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>

        internal void EditTitle(string newTitle)
        {

            if (newTitle.Length==0 || newTitle.Length>50 || newTitle == null)
            {
                log.Warn(ex.Message);
                throw ex;
            }
            else
            {
                String msg = String.Format("Task title edited! new title = {0}", newTitle);
                log.Info(msg);
                this.Title = newTitle;
            }
            

        }

        internal int GetState()
        {
            return this.State;
        }
        internal void SetState(int state)
        {
             this.State=state;
        }
        /// <summary>
        /// This method edit the description of a task
        /// </summary>
        ///  <param name="newDescription">The description of the task</param>
        /// <returns> nothing, just change it in the tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>

        internal void EditDescription(string newDescription)
        {
            if (newDescription.Length>300 || newDescription==null)
            {
                log.Warn(ex.Message);
                throw ex;
            }
            else
            {
                String msg = String.Format("Task description edited! new description = {0}", newDescription);
                log.Info(msg);
                this.Description = newDescription;
            }
        }
        /// <summary>
        /// This method edit the due date of a task
        /// </summary>
        ///  <param name="newDueDate">The new due date of the task</param>
        /// <returns> nothing, just change it in the tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>

        internal void EditDueDate(DateTime newDueDate)
        {
            if (newDueDate<=this.CreationTime || newDueDate == null)
            {
                log.Warn(ex.Message);
                throw ex;
            }
            else
            {
                String msg = String.Format("Task due date edited! new due date = {0}", newDueDate);
                log.Info(msg);
                this.DueDate = newDueDate;
            }
        }

        internal void EditAssignee(string userEmail)
        {
            this.Assignee = userEmail;
        }
        
    }
}
