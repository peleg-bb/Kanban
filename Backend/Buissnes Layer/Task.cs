using System;
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
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

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
        public int BoardId;

        public string Assignee { private set; get; }
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Exception ex = new ArgumentException();
        private const int BacklogState = 0;
        private const int InProgressState = 1;
        private const int Done = 2;
        public Task (string title, DateTime dueDate, int boardId, string description = "", string assignee = "Unassinged")
        {
            this.Id = ID;
            this.CreationTime = DateTime.Today;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.State = 0;
            this.Assignee = assignee;
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
                String msg = String.Format("Task title edited in buisness layer! new title = {0}", newTitle);
                log.Info(msg);
                this.Title = newTitle;
            }
            

        }

        internal int GetState()
        {
            return this.State;
        }
        internal string GetStatus()
        {
            {
                if (this.State == BacklogState)
                {
                    return "backlog";
                }
                else if (this.State == InProgressState)
                {
                    return "in progress";
                }
                else if (this.State == Done)
                {
                    return "Done";
                }
                else
                {
                    log.Warn("this column state does not exist!");
                    throw new ArgumentException("this column state does not exist!");
                }
                String msg = String.Format("Got the column name Successfully in BuissnesLayer!");
                log.Info(msg);
            }
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
                String msg = String.Format("Task description edited in buisness layer! new description = {0}", newDescription);
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
                String msg = String.Format("Task due date edited in buisness layer! new due date = {0}", newDueDate);
                log.Info(msg);
                this.DueDate = newDueDate;
            }
        }

        internal void EditAssignee(string userEmail)
        {
            this.Assignee = userEmail;
        }
        public string GetTitle()
        {
            return this.Title;
        }
        public string GetDescription()
        {
            return this.Description;
        }
        public string GetDueDate()
        {
            return this.DueDate.ToString();
        }
    }
}
