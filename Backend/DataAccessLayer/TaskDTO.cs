using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskDTO
    {
        private int Id;
        private string Title;
        private DateTime CreationTime;
        private string Description;
        private DateTime DueDate;
        private int State;
        private string Assignee;

        public TaskDTO(string title, DateTime dueDate, string description,DateTime creationTime,int state,int id, string assignee)
        { 
            this.Title = title;
            this.DueDate = dueDate;
            this.Description = description;
            this.CreationTime = creationTime;
            this.State = state;
            this.Id = id;
            this.Assignee = assignee;
        }

        public void EditTitle(string newTitle)
        {
            this.Title = newTitle;
        }
        public void SetState(int state)
        {
            this.State = state;
        }

        public void EditDescription(string newDescription)
        {
            this.Description = newDescription;
        }

        public void EditDueDate(DateTime newDueDate)
        {
            this.DueDate = newDueDate;
        }

        public void EditAssignee(string userEmail)
        {
            this.Assignee= userEmail;
        }
    }
}