using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        private static int ID = 0;

        public string Assignee { private set; get; }

        public Task (string title, DateTime dueDate, string description="")
        {
            this.Id = ID;
            this.CreationTime = DateTime.Today;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            
            this.State = 0;

            ID += 1;
            // Do NOT Load Data!

        }
        /// <summary>
        /// This method edit the title of a task
        /// </summary>
        ///  <param name="newTitle">The new Title of the task</param>
        /// <returns> nothing, just change it in the tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>

        internal void EditTitle(string newTitle)
        {

            if (newTitle.Length==0 || newTitle.Length>50)
            {
                throw new ArgumentNullException();
            }
            else
            {
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
            if (newDescription.Length>300)
            {
                throw new ArgumentNullException();
            }
            else
            {

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
            if (newDueDate<=this.CreationTime)
            {
                throw new ArgumentException();
            }
            else
            {
                this.DueDate = newDueDate;
            }
        }

        internal void EditAssignee(string userEmail)
        {
            this.Assignee = userEmail;
        }
        
    }
}
