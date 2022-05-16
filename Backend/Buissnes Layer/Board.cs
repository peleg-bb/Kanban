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
        //private int indexNewTask = 0; 
        private Dictionary<int, Task> tasks;
        private Dictionary<int, Task> inProgress;
        public string name;
        private int[] maxTasks = new int[] {-1,-1,-1};
        private int[] numTasks =new int[3];

        public Board(string name)
        {
            this.name = name;
        }
        public string GetName()   // property
        {
            return this.name; 
        }
        public void SetName(string newName)   // property
        {
            this.name = newName;
        }
        public int GetMaxTask(int whichBoard)   // property
        {
            return this.maxTasks[whichBoard];
        }
        public void SetMaxTask(int newMaxTask, int whichBoard)   // property
        {
            if (this.maxTasks[whichBoard] != -1)
            {
                this.maxTasks[whichBoard] = newMaxTask;
            }
            else
            {
                throw new ArgumentException("CAN'T CHANGE MAX, MAX ALREADY BEEN CHANGED");
            }
           
        }
        public Dictionary<int,Task> GetInProgress()   // property
        {
            return this.inProgress;
        }
        public Dictionary<int, Task> GetTasks()   // property
        {
            return this.tasks;
        }
        private void SetTasks(Task newTask)   // property
        {
           if (numTasks[0] < maxTasks[0] || maxTasks[0]== -1) 
           {
                tasks[newTask.TaskId] = newTask;
                numTasks[0]++;
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
                if (numTasks[1] < maxTasks[1] || maxTasks[1] == -1)
                {
                    inProgress.Add(taskId, tasks[taskId]);
                    tasks[taskId].SetState(1);
                    numTasks[0]--;
                    numTasks[1]++;
                }
                else
                {
                    throw new ArgumentException("TASK STATE CAN'T BE CHANGED! Reached max task limit at the next board! ");
                }
            }
            else if (state == 1)
            {
                if (numTasks[2] < maxTasks[2] || maxTasks[2] == -1)
                {
                    tasks[taskId].SetState(2);
                    inProgress.Remove(taskId);
                    numTasks[1]--;
                    numTasks[2]++;
                }
                else
                {
                    throw new ArgumentException("TASK STATE CAN'T BE CHANGED! Reached max task limit at the next board! ");
                }
            }
            else
            {
                throw new ArgumentException("TASK STATE CAN'T BE CHANGED! ALREADY AT DONE ");
            }

        }


    }
}
