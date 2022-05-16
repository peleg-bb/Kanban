using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class Board
    {/*
        private Dictionary<int,Task> backlog;
        private Dictionary<int, Task> inProgress;
        private Dictionary<int, Task> done;*/
        private Dictionary<int, Task> tasks;
        private Dictionary<int, Task> inProgress;
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
        { //would i need to raise Exception if the max tasks is changed after exist tasks more then the max?? 
            this.maxTasks = newMaxTask;
        }
        public Dictionary<int,Task> GetInProgress()   // property
        {
            return this.inProgress;
        }
        public Dictionary<int, Task> GetTasks()   // property
        {
            return this.tasks;
        }
        public void SetTasks(Task newTask)   // property
        {
            if (tasks.ContainsKey(newTask.TaskId))
            {
                throw new ArgumentException("TASK ALREADY EXIST");
            } 
            else if (numTasks < maxTasks)
            {
                tasks[newTask.TaskId] = newTask;
                numTasks++;
            }
            else
            {
                throw new ArgumentException("REACHED MAX TASK LIMIT");
            }
            
        }


        public void AddTask(string title, string description, DateTime dueDate)
        {
            Task newTask = new Task(title, description, dueDate,0);
            try
            {
                SetTasks(newTask);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            
        }

        public void ChangeState(int taskId)
        {
            int state = tasks[taskId].GetState();
            if (state == 0)
            {
                inProgress.Add(taskId,tasks[taskId]);
                tasks[taskId].SetState(1);
            }
            else if (state == 1)
            {
                tasks[taskId].SetState(2);
                inProgress.Remove(taskId);
            }
            else
            {
                throw new ArgumentException("TASK STATE CAN'T BE CHANGED! ALREADY AT DONE ");
            }

        }


    }
}
