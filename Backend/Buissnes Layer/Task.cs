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
        public int TaskId { get; }
        [DataMember(Order = 1 )]
        public readonly DateTime CreationDate;
        [DataMember(Order = 1)]
        private string Title { set; get; }
        [DataMember(Order = 1)]
        private string Description { set; get; }
        [DataMember(Order = 1)]
        private DateTime DueDate { set; get; }
        [JsonIgnore]
        private int State;
        [JsonIgnore]
        private int ID = 0;

        public Task (string title, DateTime dueDate, string description="")
        {
            this.TaskId = ID;
            this.CreationDate = DateTime.Today;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            
            this.State = 0;

            ID += 1;

        }
        public void EditTitle(string newTitle)
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

        public int GetState()
        {
            return this.State;
        }
        public void SetState(int state)
        {
             this.State=state;
        }

        public void EditDescription(string newDescription)
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

        public void EditDueDate(DateTime newDueDate)
        {
            if (newDueDate<=this.CreationDate)
            {
                throw new ArgumentException();
            }
            else
            {
                this.DueDate = newDueDate;
            }
        }
        
    }
}
