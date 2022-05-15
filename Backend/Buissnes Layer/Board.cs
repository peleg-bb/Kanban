using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    internal class Board
    {
        private Task[] backlog;
        private Task[] inProgress;
        private Task[] done;
        public string name;
        private int maxTasks = -1;
        private int indexNewTask = 0;
        private int numTasks = 0;

        public Board(string name, int maxTasks=-1)
        {
            this.name = name;
           this. maxTasks = maxTasks;
        }
        public string GetName()   // property
        {
            return this.name; 
        }
        public void SetName(string newName)   // property
        {
            this.name = newName;
        }
        public int GetMaxTask()   // property
        {
            return this.maxTasks;
        }
        public void SetMaxTask(int newMaxTask)   // property
        {
            this.maxTasks = newMaxTask;
        }
        public Task[] GetInProgress()   // property
        {
            return inProgress;
        }
        public void SetBacklog(Task newTask)   // property
        {
            this.inProgress[indexNewTask]=newTask;
            indexNewTask++;
            numTasks++;
        }




        public void AddTask(string title, string description, string dueDate)
        {

        }

        public void ChangeState(Task task)
        {

        }


    }
}
