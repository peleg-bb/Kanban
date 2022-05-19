using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class Board
    {
        private Dictionary<int, Task> tasks = new Dictionary<int, Task>();
        private List<Task> inProgress = new List<Task>();
        public string name;
        private int[] maxTasks = new int[] {-1,-1,-1};
        private int[] numTasks =new int[] {0,0,0};

        public Board(string name)
        {
            this.name = name;
        }
        public string GetName()   
        {
            return this.name;
        }
        public void SetName(string newName)   
        {
            this.name = newName;
        }
        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="whichBoard">The name of the board</param>
        /// <returns>Response with column limit value, unless an error occurs </returns>
        public int GetMaxTask(int whichBoard)  
        {
            if (whichBoard == 0 || whichBoard == 1 || whichBoard == 2)
            {
                return this.maxTasks[whichBoard];
            }
            else
            {
                throw new ArgumentException("this column state does not exist!");
            }
           
        }
        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="whichBoard">The name of the board</param>
        /// <param name="newMaxTask">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns> void, unless an error occurs </returns>
        public void SetMaxTask(int newMaxTask, int whichBoard)  
        {
            if (whichBoard == 0 || whichBoard == 1 || whichBoard == 2)
            {
                if (this.maxTasks[whichBoard] == -1)
                {
                    this.maxTasks[whichBoard] = newMaxTask;
                }
                else
                {
                    throw new ArgumentException("CAN'T CHANGE MAX, MAX ALREADY BEEN CHANGED");
                }
            }
            else
            {
                throw new ArgumentException("this column state does not exist!");
            }
          
           
        }
        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="columnO">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with  a list of the column's tasks, unless an error occurs .</returns>
        public List<Task> GEtColList(int columnO)
        {
            if (columnO == 0 || columnO == 1 || columnO == 2)
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
            else
            { 
                throw new ArgumentException("this column state does not exist!");
            }
            
        }
        /// <summary>
        /// This method gets the name of a specific column
        /// </summary>
        /// <param name="columnO">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with column name value, unless an error occurs </returns>
        public string GetNameOrdinal(int columnO)
        {
            if (columnO == 0)
            {
                return "backlog";
            }
            else if (columnO == 1)
            {
                return "in progress";
            }
            else if(columnO == 2)
            {
                return "Done";
            }
            else
            {
                throw new ArgumentException("this column state does not exist!");
            }
        }
        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <returns>Response with a list of the in progress tasks, unless an error occurs .</returns>
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
               this.tasks[newTask.Id] = newTask;
                Console.WriteLine("taskId"+newTask.Id);
                this.numTasks[0]++;
                Console.WriteLine(numTasks.Length);
           }
           else
           {
                throw new ArgumentException("REACHED MAX TASK LIMIT");
           }
            
        }
        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>void, unless an error occurs </returns>
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
        /// <summary>
        /// This method updates the state of the  task.
        /// </summary>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>void, throws an exception if error occurs </returns>>
        public void ChangeState(int taskId)
        {
            if (this.tasks.ContainsKey(taskId))
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
                else if(state == 2)
                {
                    throw new ArgumentException("TASK STATE CAN'T BE CHANGED! ALREADY AT DONE ");
                }
            }
            else
            {
                throw new ArgumentException("TASK Does not exist! ");
            }
            

        }


    }
}
