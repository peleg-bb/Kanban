using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
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

        public TaskDTO(string title, DateTime dueDate, string description, DateTime creationTime, int state, int id, string assignee)
        {
            Title = title;
            DueDate = dueDate;
            Description = description;
            CreationTime = creationTime;
            State = state;
            Id = id;
            Assignee = assignee;
        }

        public void EditTitle(string newTitle)
        {
            Title = newTitle;
        }
        public void SetState(int state)
        {
            State = state;
        }

        public void EditDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void EditDueDate(DateTime newDueDate)
        {
            DueDate = newDueDate;
        }

        public void EditAssignee(string userEmail)
        {
            Assignee = userEmail;
        }
    }
}