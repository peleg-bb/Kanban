﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class Task
    {
        private string Title { set; get; }

        private string Description { set; get; }
        private DateTime DueDate { set; get; }
        public readonly string CreationDate;
        private int State; 
        public int TaskId { get; }
        private int ID = 0;

        public Task (string title, string description, DateTime dueDate)
        {
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            this.CreationDate = DateTime.Now.ToString("'yyyy'-'MM'-'dd'");
            this.State = 0;
            this.TaskId = ID;
            ID += 1;

        }
        public void EditTitle(string newTitle)
        {

            if (newTitle == "")
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
            if (newDescription == "")
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
            if (!DateTime.TryParseExact(newDueDate.ToString(),"dd/mm/yyyy",CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out newDueDate))
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
