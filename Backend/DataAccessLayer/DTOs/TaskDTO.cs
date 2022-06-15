using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class TaskDTO
    {
        int taskID;
        int boardID;
        string assignee;
        string status;
        string title;
        string description;
        string dueDate;
        string creationTime;

        public TaskDTO(int taskID, int boardID, string assignee, string status, string title, string description, string dueDate, string creationTime)
        {
            this.taskID = taskID;
            this.boardID = boardID;
            this.assignee = assignee;
            this.status = status;
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.creationTime = creationTime;
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