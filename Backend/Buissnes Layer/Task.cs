using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class Task
    {
        private string Title { set; get; }

        private string Description { set; get; }
        private string DueDate { set; get; }
        public readonly string CreationDate = DateTime.Now.ToString("'yyyy'-'MM'-'dd'");
        private int State = 0; 
        private int TaskId { get; }
        private int ID = 0;

        public Task (string title, string description, string dueDate, int state)
        {
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.CreationDate = DateTime.Now.ToString("'yyyy'-'MM'-'dd'");
            this.State = state;
            this.TaskId = ID;
            ID += 1;

        }
        private void EditTitle(string newTitle)
        {
            this.Title = newTitle;

        }

        public int GetState()
        {
            return this.State;
        }
        public void SetState(int state)
        {
             this.State=state;
        }

        private void EditDescription(string newDescription)
        {
            this.Description = newDescription;
        }

        private void EditDueDate(string newDueDate)
        {
            this.DueDate = newDueDate;
        }
    }
}
