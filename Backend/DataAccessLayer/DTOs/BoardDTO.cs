using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using
using IntroSE.Kanban.Backend.DataAccessLayer.Mappers;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardDTO
    {
        private string owner;
        public string Owner => owner;
        private string name;

        public string Name => name;
        private readonly int iD;
        public int ID => iD;
        private int backlogMax;

        public int BacklogMax => backlogMax;
        private int inProgressMax;
        public int InProgressMax => inProgressMax;
        private int doneMax;
        public int DoneMax => doneMax;
        private string TasksTable = "Tasks";
        private string BoardUsersTable = "Board_Users";
        private List<TaskDTO> taskDTOs;
        private List<string> BoardUsers;
        private TaskDTOMapper taskDTOMapper;

        /// <summary>
        /// Do Not Use!! This is an old constructor - consult Peleg before using
        /// </summary>
        public BoardDTO() 
        {
            this.taskDTOs = new List<TaskDTO>();
            this.backlogMax = -1;
            this.inProgressMax = -1;
            this.doneMax = -1;
        }



        public BoardDTO(string owner, string name, int iD, int backlogMax, int inProgressMax, int doneMax)
        {
            //, List<string> boardUsers)
            this.owner = owner;
            this.name = name;
            this.iD = iD;
            this.backlogMax = backlogMax;
            this.inProgressMax = inProgressMax;
            this.doneMax = doneMax;
            //BoardUsers = boardUsers;
            this.taskDTOs = new List<TaskDTO>();
        }

        public BoardDTO LoadBoard()
        {

            string path = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "kanban.db"));
            Console.WriteLine(path);
            string connectionString = $"Data Source={path}; Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"Select * FROM {TasksTable}" +
                                          $"WHERE Board_ID = {this.iD};" +
                                          $"SELECT * FROM {BoardUsers} " +
                                          $"WHERE Board_ID = {this.iD}";
                    command.Prepare();
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    { // Create Task DTOs and add Tasks
                        int taskID = (int)reader["Task_ID"];
                        int boardID = (int)reader["Board_ID"];
                        string assignee = reader["Assignee"].ToString();
                        string status = reader["Status"].ToString();
                        string title = reader["Title"].ToString();
                        string description = reader["Description"].ToString();
                        string dueDate = reader["Due_Date"].ToString();
                        string creationTime = reader["Creation_Time"].ToString();
                        TaskDTO task = new TaskDTO(taskID: taskID, boardID: boardID,
                            assignee: assignee, status: status,
                            title: title, description: description,
                            dueDate: dueDate, creationTime: creationTime);
                        taskDTOs.Add(task);
                        Console.WriteLine("Task " + taskID + " loaded to Board " + boardID);
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        // Add Users
                        string email = reader["User"].ToString();
                        BoardUsers.Add(email);
                        
                    }

                    return this;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    // log error
                    // Maybe throw an exception? Probs not, might not reach finally
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

                BoardDTO ifFailed = null;
                return ifFailed;
            }
        }

        public void DeleteAllData()
        {
            string path = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "kanban.db"));
            string connectionString = $"Data Source={path}; Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);



                int res = -1;

                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM {TasksTable}";
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    taskDTOs.Clear();
                    Console.WriteLine($"SQL execution finished without errors. Result: {res} rows changed");
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    throw new DALException($"Delete tasks failed because " + ex.Message);
                    // log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }

        }
        
        public TaskDTO AddTask()
        {
            taskDTOMapper
        }

        
    }
}
