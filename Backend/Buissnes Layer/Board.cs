using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class Board
    {
        private Dictionary<int,Task> backlog;
        private Dictionary<int, Task> inProgress;
        private Dictionary<int, Task> done;
        public string name;
        private int maxTasks = -1;
        //private int indexNewTask = 0;
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
        public Dictionary<int,Task> GetInProgress()   // property
        {
            return inProgress;
        }
        public void SetBacklog(Task newTask)   // property
        {
            if (numTasks<maxTasks)
            {
                this.inProgress.Add(newTask.GetId, newTask);
                // indexNewTask++;
                numTasks++;
            }
            else
            {
                throw new ArgumentException("REACHED MAX TASK LIMIT");
            }
            
        }


        public void AddTask(string title, string description, string dueDate)
        {
            Task newTask = new Task(title, description, dueDate, 0);
            try
            {
                SetBacklog(newTask);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            
        }

        public void ChangeState(Task task)
        {
            int state = task.GetState();
            if (state == 0)
            {
                inProgress.Add(task.GetId,task);
                backlog.Remove(task.GetId);
            }
            else if (state == 1)
            {
                done.Add(task.GetId, task);
                inProgress.Remove(task.GetId);
            }
            else
            {
                throw new ArgumentException("TASK STATE CAN'T BE CHANGED! ALREADY AT DONE ");
            }

        }


    }
}
