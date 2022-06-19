using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Mappers
{
    internal class TaskDTOMapper
    {
        private List<TaskDTO> taskDTOs;
        const string taskIDColumnName = "Task_ID";
        const string boardIDColumnName = "Board_ID";
        const string assigneeColumnName = "Assignee";
        const string statusColumnName = "Status";
        const string titleColumnName = "Title";
        const string descriptionColumnName = "Description";
        const string dueDateColumnName = "Due_Date";
        const string creationDateColumnName = "Creation_Time";
        const string tableName = "Tasks";
        internal TaskDTOMapper()
        {
            this.taskDTOs = new List<TaskDTO>();
        }
        public TaskDTO CreateTask(int taskID, int boardID, string assignee, string status, string title, string description, string dueDate, string creationTime)
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
                    command.CommandText = $"INSERT INTO {tableName} ({taskIDColumnName}, {boardIDColumnName},{assigneeColumnName}, {statusColumnName},{titleColumnName}, {descriptionColumnName},{dueDateColumnName}, {creationDateColumnName}) " +
                                        $"VALUES (@taskID_val, @boardID_val, @assignee_val, @status_val, @title_val, @description_val, @dueDate_val, @creationTime_val)";

                    SQLiteParameter taskIDParam = new SQLiteParameter(@"taskID_val", taskID);
                    SQLiteParameter boardIDParam = new SQLiteParameter(@"boardID_val", boardID);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"assignee_val", assignee);
                    SQLiteParameter statusParam = new SQLiteParameter(@"status_val", status);
                    SQLiteParameter titleParam = new SQLiteParameter(@"title_val", title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"description_val", description);
                    SQLiteParameter dueDateParam = new SQLiteParameter(@"dueDate_val", dueDate);
                    SQLiteParameter creationTimeParam = new SQLiteParameter(@"creationTime_val", creationTime);

                    command.Parameters.Add(taskIDParam);
                    command.Parameters.Add(boardIDParam);
                    command.Parameters.Add(assigneeParam);
                    command.Parameters.Add(statusParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(dueDateParam);
                    command.Parameters.Add(creationTimeParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    // Console.WriteLine(res);
                    // Console.WriteLine("success!");

                    TaskDTO newTask = new TaskDTO(taskID, boardID, assignee, status, title, description, dueDate, creationTime);
                    taskDTOs.Add(newTask);
                    return newTask;


                    //
                    // command.CommandText = "Select * FROM Users";
                    //
                    // SQLiteDataReader reader = command.ExecuteReader();
                    // while (reader.Read())
                    // {
                    //      Console.WriteLine(reader["email"] + ", " + reader["password"]);
                    // }
                }
                catch (SQLiteException ex)
                {
                    //Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    throw new DALException($"Create task failed because " + ex.Message);
                    // log error
                }
                finally
                {

                    command.Dispose();
                    connection.Close();
                }
            }
            return null; // If failed to create user
        }

        public void EditTitle(int taskID, string newTitle)
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

                    command.Prepare();

                    // Console.WriteLine(res);
                    // Console.WriteLine("success!");
                    command.CommandText = $"UPDATE {tableName} SET Title = @title_val Where Task_ID = @taskID_val";
                    SQLiteParameter taskIDParam = new SQLiteParameter(@"taskID_val", taskID);
                    SQLiteParameter titleParam = new SQLiteParameter(@"title_val", newTitle);
                    command.Parameters.Add(taskIDParam);
                    command.Parameters.Add(titleParam);
                    res = command.ExecuteNonQuery();
                    //
                    // command.CommandText = "Select * FROM Users";
                    //
                    // SQLiteDataReader reader = command.ExecuteReader();
                    // while (reader.Read())
                    // {
                    //      Console.WriteLine(reader["email"] + ", " + reader["password"]);
                    // }
                }
                catch (SQLiteException ex)
                {
                    //Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    throw new DALException($"Change task title failed because " + ex.Message);
                    // log error
                }
                finally
                {

                    command.Dispose();
                    connection.Close();
                }
            }
        }
            public void EditDescription(int taskID, string newDescription)
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

                        command.Prepare();

                        // Console.WriteLine(res);
                        // Console.WriteLine("success!");
                        command.CommandText = $"UPDATE {tableName} SET Description = @description_val Where Task_ID = @taskID_val";
                        SQLiteParameter taskIDParam = new SQLiteParameter(@"taskID_val", taskID);
                        SQLiteParameter descriptionParam = new SQLiteParameter(@"description_val", newDescription);
                        command.Parameters.Add(taskIDParam);
                        command.Parameters.Add(descriptionParam);
                        res = command.ExecuteNonQuery();
                        //
                        // command.CommandText = "Select * FROM Users";
                        //
                        // SQLiteDataReader reader = command.ExecuteReader();
                        // while (reader.Read())
                        // {
                        //      Console.WriteLine(reader["email"] + ", " + reader["password"]);
                        // }
                    }
                    catch (SQLiteException ex)
                    {
                        //Console.WriteLine(command.CommandText);
                        Console.WriteLine(ex.Message);
                        throw new DALException($"Change task title failed because " + ex.Message);
                        // log error
                    }
                    finally
                    {

                        command.Dispose();
                        connection.Close();
                    }
                }
                // If failed to create user
            }
        public void EditDueDate(int taskID, string newDueDate)
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

                    command.Prepare();

                    // Console.WriteLine(res);
                    // Console.WriteLine("success!");
                    command.CommandText = $"UPDATE {tableName} SET Due_Date = @dueDate_val Where Task_ID = @taskID_val";
                    SQLiteParameter taskIDParam = new SQLiteParameter(@"taskID_val", taskID);
                    SQLiteParameter dueDateParam = new SQLiteParameter(@"dueDate_val", newDueDate);
                    command.Parameters.Add(taskIDParam);
                    command.Parameters.Add(dueDateParam);
                    res = command.ExecuteNonQuery();
                    //
                    // command.CommandText = "Select * FROM Users";
                    //
                    // SQLiteDataReader reader = command.ExecuteReader();
                    // while (reader.Read())
                    // {
                    //      Console.WriteLine(reader["email"] + ", " + reader["password"]);
                    // }
                }
                catch (SQLiteException ex)
                {
                    //Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    throw new DALException($"Change task title failed because " + ex.Message);
                    // log error
                }
                finally
                {

                    command.Dispose();
                    connection.Close();
                }
            }
            // If failed to create user
        }
        internal List<TaskDTO> LoadTasks()
        {
            throw new NotImplementedException("Redundant method, DO NOT USE!");
            /*
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
                    command.CommandText = $"Select * FROM {tableName}";
                    command.Prepare();
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int taskID = Convert.ToInt32(reader["TaskID"]);
                        int boardID = Convert.ToInt32(reader["BoardID"]);
                        string assignee = reader["email"].ToString();
                        string status = reader["password"].ToString();
                        string title = reader["email"].ToString();
                        string description = reader["password"].ToString();
                        string dueDate = reader["email"].ToString();
                        string creationTime = reader["password"].ToString();
                        TaskDTO task = new TaskDTO(taskID, boardID, assignee, status, title, description, dueDate, creationTime);
                        taskDTOs.Add(task);
                        Console.WriteLine("Task " + title + " loaded successfully");
                    }


                    return taskDTOs;

                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    throw new DALException($"Create task failed because " + ex.Message);
                    // log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }

            List<TaskDTO> ifFailed = new List<TaskDTO>();
            return ifFailed;
        }


        public void DeleteAllData()
        {
            throw NotImplementedException("Redundant method, DO NOT USE!");
            /*
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
                    command.CommandText = $"DELETE FROM {tableName}";
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    Console.WriteLine($"SQL execution finished without errors. Result: {res} rows changed");
                    taskDTOs.Clear();

                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    throw new DALException($"Delete data failed because " + ex.Message);
                    // log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
    }
}
