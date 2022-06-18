using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Mappers
{
    internal class UserDTOMapper
    {
        private List<UserDTO> userDTOs = new List<UserDTO>();
        const string emailColumnName = "email";
        const string passwordColumnName = "password";
        const string tableName = "Users";

        public UserDTO CreateUser(string email, string password)
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
                    command.CommandText = $"INSERT INTO {tableName}" +
                                          $" ({emailColumnName}, {passwordColumnName}) " +
                                        $"VALUES (@email_val, @password_val)";
                    SQLiteParameter emailParam = new SQLiteParameter(@"email_val", email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"password_val", password);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);
                    command.Prepare();
                    Console.WriteLine(path);
                    res = command.ExecuteNonQuery();
                    // Console.WriteLine(res);
                    // Console.WriteLine("success!");

                    UserDTO user = new UserDTO(email, password);
                    userDTOs.Add(user);
                    return user;


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
                    throw new DALException($"Create user failed because " + ex.Message);
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

        internal List<UserDTO> LoadUsers()
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
                    command.CommandText = $"Select * FROM {tableName}";
                    command.Prepare();
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string username = reader["email"].ToString();
                        string password = reader["password"].ToString();
                        UserDTO user = new UserDTO(username, password);
                        userDTOs.Add(user);
                        Console.WriteLine("User " + username + " loaded successfully");
                        
                    }

                    
                    return userDTOs;

                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    throw new DALException($"Create user failed because " + ex.Message);
                    // log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }

            List<UserDTO> ifFailed = new List<UserDTO>();
            return ifFailed;
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
                    command.CommandText = $"DELETE FROM {tableName}";
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    Console.WriteLine($"SQL execution finished without errors. Result: {res} rows changed");
                    userDTOs.Clear();

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
