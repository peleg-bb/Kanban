using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserDTOMapper
    {
        private List<UserDTO> users;

        public void CreateUser(string email, string password)
        {

            const string emailColumnName = "email";
            const string passwordColumnName = "password";
            const string tableName = "Users";

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
                    command.CommandText = ($"INSERT INTO {tableName} ({emailColumnName}, {passwordColumnName}) " +
                                        $"VALUES (@email_val, @password_val) ");

                    SQLiteParameter emailParam = new SQLiteParameter(@"email_val", email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"password_val", password);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    Console.WriteLine(res);
                    Console.WriteLine("success!");
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

        public void ChangePassword(string email, string NewPassword)
        {

        }

        public void LoadUsers()
        {
            // foreach (var VARIABLE in COLLECTION)
            // {
            //     users.Add(new UserDTO());
            // }
        }
    }
}
