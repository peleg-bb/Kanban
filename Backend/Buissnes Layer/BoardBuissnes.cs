using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class Board
    {/*
        private Dictionary<int,TaskService> backlog;
        private Dictionary<int, TaskService> inProgress;
        private Dictionary<int, TaskService> done;*/
        //private int indexNewTask = 0; 
        private Dictionary<int, Task> tasks = new Dictionary<int, Task>();
        //private Dictionary<int, TaskService> inProgress = new Dictionary<int, TaskService>();
        private List<Task> inProgress = new List<Task>();
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

        public List<Task> GEtColList(int columnO)
        {
            List<Task> taskListO = new List<Task>();
            if (columnO == 1)
            {
                return GetInProgress();
            }
            else
            {


                for (int i = 0; i < this.tasks.Count; i++)
                {
                    if (this.tasks[i].GetState() == columnO)
                    {
                        taskListO.Add(this.tasks[i]);
                    }
                }

                return taskListO;
            }
        }

        public string GetNameOrdinal(int coulumnO)
        {
            if (coulumnO == 0)
            {
                return "backlog";
            }
            else if (coulumnO == 1)
            {
                return "in progress";
            }
            else
            {
                return "Done";
            }
        }
        public List<Task> GetInProgress()   // property
        {
            return this.inProgress;
        }
        public Dictionary<int, Task> GetTasks()   // property
        {
            return this.tasks;
        }
        public Task GetTask(int taskId)   // property
        {
            return this.tasks[taskId];
        }
        private void SetTasks(Task newTask)   // property
        {
           if (numTasks[0] < maxTasks[0] || maxTasks[0]== -1) 
           {
                this.tasks[newTask.TaskId] = newTask;
                this.numTasks[0]++;
           }
           else
           {
                throw new ArgumentException("REACHED MAX TASK LIMIT");
           }
            
        }


        public void AddTask(string title, string description, DateTime dueDate)
        {
            Task newTask = new Task(title, dueDate, description);
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
            int state = this.tasks[taskId].GetState();
            if (state == 0)
            {
                if (numTasks[1] < maxTasks[1] || maxTasks[1] == -1)
                {

                    this.inProgress.Add(tasks[taskId]);
                    this.tasks[taskId].SetState(1);
                    this.numTasks[0]--;
                    this.numTasks[1]++;
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
                    this.tasks[taskId].SetState(2);
                    this.inProgress.Remove(tasks[taskId]);
                    this.numTasks[1]--;
                    this.numTasks[2]++;
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
