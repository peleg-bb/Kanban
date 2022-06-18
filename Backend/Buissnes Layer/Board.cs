﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.ServiceLayer;
using log4net;
using log4net.Config;


namespace IntroSE.Kanban.Backend.Buissnes_Layer
{
    public class Board
    {
        private Dictionary<int, Task> tasks = new Dictionary<int, Task>();
        private List<Task> inProgress = new List<Task>();
        public string name { get; set; } // Change to conventional C# getters and setters
        private const int InfinityTask = -1;
        private int[] maxTasks = new int[] {InfinityTask,InfinityTask,InfinityTask};
        private int[] numTasks =new int[] {0,0,0};
        private int BoardId { get; }
        public int BoardID => BoardId;
        private string Owner;
        private List<string> listOfJoiners = new List<string>();
        private BoardDTO boardDTO;
        private const int BacklogState = 0;
        private const int InProgressState = 1;
        private const int Done = 2;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        /// <summary>
        /// Do Not Use!! This is an old constructor - consult Peleg before using
        /// </summary>
        public Board(string name , int BID , string owner)
        {
            throw new NotImplementedException("Do Not Use!! This is an old constructor - consult Peleg before using");
            this.name = name;
            this.Owner = owner;
            this.BoardId = BID;
            //this.boardDTO = new BoardDTO(); // Load empty? Need to sort out in DAL
            // Do NOT Load Data in constructor!
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting log!");
        }

        /// <summary>
        /// Constructor using a BoardDTO.
        /// Assumes the creation of a board was successful in the Database. 
        /// </summary>
        /// <param name="boardDto">
        /// BoardDTO object in DAL. 
        /// </param>
        internal Board(BoardDTO boardDto)
        {
            this.boardDTO = boardDto;
            this.BoardId = boardDto.ID;
            this.name = boardDto.Name;
            this.Owner = boardDto.Owner;
        }

        public string GetName()   
        {
            return this.name;
        }
        public void SetName(string newName)   
        {
            this.name = newName;
        }
        public string GetOwner()
        {
            return this.Owner;
        }
        public void AddToJoinList(string newMember)
        {
            this.listOfJoiners.Add(newMember); 
        }
        public void DeleteFromJoinList(string newMember)
        {
            try
            {
                this.listOfJoiners.Remove(newMember);
            }
            catch 
            {
                throw new ArgumentException("THE USER IS NOT A MEMBER OF THIS BOARD");
            }
            
        }
        public List<string> GetListOfJoiners()
        {
            return this.listOfJoiners;
        }
        public bool IsInListOfJoiners(string userEmail)
        {
            return this.listOfJoiners.Contains(userEmail);
        }
        public void SetOwner(string newOwner)
        {
            this.Owner = newOwner;
        }

        /// <summary>
        /// This method gets the limit of a specific column.
        /// </summary>
        /// <param name="whichBoard">The name of the board</param>
        /// <returns>Response with column limit value, unless an error occurs </returns>
        public int GetMaxTask(int whichBoard)  
        {
            if (whichBoard == BacklogState || whichBoard == InProgressState || whichBoard == Done)
            {
                String msg = String.Format("Got task max Successfully in BuissnesLayer! at column :{0}", GetNameOrdinal(whichBoard));
                log.Info(msg);
                return this.maxTasks[whichBoard];
            }
            else
            {
                log.Warn("this column state does not exist!");
                throw new ArgumentException("this column state does not exist!");
            }
           
        }
        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// NOTE! whichBoard seems to actually be the state (columnOrdinal). To be fixed please.
        /// </summary>
        /// <param name="whichBoard">The name of the board</param>
        /// <param name="newMaxTask">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns> void, unless an error occurs </returns>
        public void SetMaxTask(int newMaxTask, int whichBoard)  
        {
            if (whichBoard == BacklogState || whichBoard == InProgressState || whichBoard == Done)
            {
                if ( newMaxTask == InfinityTask)
                {
                    this.maxTasks[whichBoard] = newMaxTask;
                }
                else if(this.numTasks[whichBoard]<newMaxTask)
                {
                    this.maxTasks[whichBoard] = newMaxTask;
                }
                else
                {
                    log.Warn("CAN'T CHANGE MAX, NUMBER OF TASK AT THIS BOARD IS HIGHER!");
                    throw new ArgumentException("CAN'T CHANGE MAX, NUMBER OF TASK AT THIS BOARD IS HIGHER!");
                }

                String msg = String.Format("Set task max Successfully in BuissnesLayer! at column :{0} to new maxTask :{1}", GetNameOrdinal(whichBoard), newMaxTask);
                log.Info(msg);
            }
            else
            {
                log.Warn("this column state does not exist!");
                throw new ArgumentException("this column state does not exist!");
            }
          
           
        }
        /// <summary>
        /// This method returns a column given it's name
        /// </summary>
        /// <param name="columnO">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>Response with  a list of the column's tasks, unless an error occurs .</returns>
        public List<Task> GEtColList(int columnO , string assignee="Unassigned")
        {
            if (columnO == BacklogState || columnO == InProgressState || columnO == Done)
            {
                List<Task> taskListO = new List<Task>();
                if (columnO == InProgressState)
                {
                    return GetInProgressByAssignee(assignee); // get all the func that the user assiagned to that inProgress.
                }
                else
                {
                    for (int i = 0; i < this.tasks.Count; i++)
                    {
                        if (this.tasks[i+1].GetState() == columnO)
                        {
                            taskListO.Add(this.tasks[i+1]);// get all the func that the user has at that column.
                        }
                    }

                    return taskListO;
                }
                String msg = String.Format("Got the column {0} Successfully in BuissnesLayer!", GetNameOrdinal(columnO));
                log.Info(msg);
            }
            else
            {
                log.Warn("this column state does not exist!");
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
            if (columnO == BacklogState)
            {
                return "backlog";
            }
            else if (columnO == InProgressState)
            {
                return "in progress";
            }
            else if(columnO == Done)
            {
                return "Done";
            }
            else
            {
                log.Warn("this column state does not exist!");
                throw new ArgumentException("this column state does not exist!");
            }
            String msg = String.Format("Got the column name Successfully in BuissnesLayer!");
            log.Info(msg);
        }
        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <returns>Response with a list of the in progress tasks, unless an error occurs .</returns>
        // public List<Task> GetInProgress()   // property
        // {
        //     return this.inProgress;
        // }
        public List<Task> GetInProgressByAssignee(string assignee)   // property
        {
            List<Task> taskInProgList = new List<Task>();
            foreach (var taski in this.inProgress)
            {
                if (taski.Assignee == assignee)
                {
                    taskInProgList.Add(taski);
                }
            }

            return taskInProgList;
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
           if (numTasks[BacklogState] < maxTasks[BacklogState] || maxTasks[BacklogState]== InfinityTask) 
           {
               this.tasks[newTask.Id] = newTask;
               this.numTasks[BacklogState]++;
               String msg = String.Format("set task max Successfully in BuissnesLayer! ");
               log.Info(msg);
           }
           else
           {
               log.Warn("REACHED MAX TASK LIMIT");
                throw new ArgumentException("REACHED MAX TASK LIMIT");
           }
            
        }

        public void leaveTasks(string userEmail)
        {
            List<Task> p= GetInProgressByAssignee(userEmail);
            List<Task> e = this.GEtColList(BacklogState);
            if (e!=null && p!=null)
            {
                e.AddRange(p);
                foreach (var taski in e)
                {
                    if (taski.Assignee == userEmail)
                    {
                        taski.EditAssignee("Unassigned");
                    }
                }
            }
            
            String msg = String.Format("leave tasks Successfully in BuissnesLayer! ");
            log.Info(msg);
        }
        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>void, unless an error occurs </returns>
        public void AddTask(string title, string description, DateTime dueDate ,string userEmail)
        {
            Task newTask = new Task(title, dueDate,this.BoardId, description);
            if (this.IsInListOfJoiners(userEmail))
            {
                try
                {
                    SetTasks(newTask);
                    String msg = String.Format("set new task Successfully in BuissnesLayer! ");
                    log.Info(msg);
                    boardDTO.AddTask(newTask.Id, newTask.BoardId, newTask.Assignee, newTask.GetStatus(), newTask.GetTitle(), newTask.GetDescription(), newTask.GetDueDate(), newTask.CreationTime.ToString());
                }
                catch (Exception e)
                {
                    log.Warn(e.Message);
                    throw new ArgumentException(e.Message);
                }
            }
            else
            {
                log.Warn("USER IS NOT A MEMBER !! ONLY A MEMBER OF THIS BOARD CAN ADD TASK TO IT!");
                throw new Exception("USER IS NOT A MEMBER !! ONLY A MEMBER OF THIS BOARD CAN ADD TASK TO IT!");
            }
        }
        /// <summary>
        /// This method updates the state of the  task.
        /// </summary>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>void, throws an exception if error occurs </returns>>
        public void ChangeState(int taskId , string userEmail)
        {

            if (this.tasks.ContainsKey(taskId))
            {
                int state = this.tasks[taskId].GetState();
                if (state == BacklogState)
                {
                    if (numTasks[InProgressState] < maxTasks[InProgressState] || maxTasks[InProgressState] == InfinityTask)
                    {
                        this.inProgress.Add(tasks[taskId]);
                        this.tasks[taskId].SetState(1);
                        this.numTasks[0]--;
                        this.numTasks[1]++;
                    }
                    else
                    {
                        log.Warn("TASK STATE CAN'T BE CHANGED! Reached max task limit at the next board! ");
                        throw new ArgumentException("TASK STATE CAN'T BE CHANGED! Reached max task limit at the next board! ");
                    }
                }
                else if (state == InProgressState)
                {
                    if (numTasks[Done] < maxTasks[Done] || maxTasks[Done] == InfinityTask)
                    {
                        this.tasks[taskId].SetState(Done);
                        this.inProgress.Remove(tasks[taskId]);
                        this.numTasks[InProgressState]--;
                        this.numTasks[Done]++;
                    }
                    else
                    {
                        log.Warn("TASK STATE CAN'T BE CHANGED! Reached max task limit at the next board! ");
                        throw new ArgumentException("TASK STATE CAN'T BE CHANGED! Reached max task limit at the next board! ");
                    }
                }
                else if(state == Done)
                {
                    log.Warn("TASK STATE CAN'T BE CHANGED! ALREADY AT DONE ");
                    throw new ArgumentException("TASK STATE CAN'T BE CHANGED! ALREADY AT DONE ");
                }
                String msg = String.Format("Changed state of a task Successfully in BuissnesLayer! ");
                log.Info(msg);
            }
            else
            {
                log.Warn("TASK Does not exist! ");
                throw new ArgumentException("TASK Does not exist! ");
            }
            

        }

    }
}
