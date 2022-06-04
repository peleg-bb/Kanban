using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardDTO
    {
        private List<TaskDTO> taskDTOs;
        private string TasksTableName;

        public BoardDTO()
        {
            this.taskDTOs = new List<TaskDTO>();
            this.TasksTableName = "Tasks";
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
                    command.CommandText = $"DELETE FROM {TasksTableName}";
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    taskDTOs.Clear();
                    Console.WriteLine($"SQL execution finished without errors. Result: {res} rows changed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
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
