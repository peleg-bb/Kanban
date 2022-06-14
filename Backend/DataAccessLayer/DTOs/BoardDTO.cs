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
        private string owner;
        private string name;
        private int iD;
        private int backlogMax;
        private int inProgressMax;
        private int doneMax;
        private string BoardsTableName = "Boards";
        private string BoardUsersTable = "Board_Users";
        private List<string> BoardUsers;

        public BoardDTO()
        {
            this.taskDTOs = new List<TaskDTO>();
            this.TasksTableName = "Tasks";
        }

        public BoardDTO(string owner, string name, int iD, int backlogMax, int inProgressMax, int doneMax) //, List<string> boardUsers)
        {
            this.owner = owner;
            this.name = name;
            this.iD = iD;
            this.backlogMax = backlogMax;
            this.inProgressMax = inProgressMax;
            this.doneMax = doneMax;
            //BoardUsers = boardUsers;
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
