﻿using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Mappers
{
    internal class BoardUsersMapper
    {
        private List<BoardUsersDTO> _boardUsersDTOs;
        const string boardColumnName = "Board_Id";
        const string userColumnName = "User_email";
        const string tableName = "Board_Users";

        public void CreateBoard(int boardID, string userEmail)
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
                    command.CommandText = $"INSERT INTO {tableName} ({boardColumnName}, {userColumnName}) " +
                                        $"VALUES (@board_id, @username)";

                    SQLiteParameter boardParam = new SQLiteParameter(@"board_id", boardID);
                    SQLiteParameter userParam = new SQLiteParameter(@"username", userEmail);

                    command.Parameters.Add(boardParam);
                    command.Parameters.Add(userParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    

                    BoardUsersDTO boardUser = new BoardUsersDTO(boardID, userEmail);
                    _boardUsersDTOs.Add(boardUser);

                    // Console.WriteLine(res);
                    // Console.WriteLine("success!");
                    //return boardUser; // In case we want to return to return a boardUser object.


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

        public void DeleteBoard(int boardID)
        {
            //_boards.RemoveAll(itemCollection => boardID == itemCollection.BoardID);
            // Note that this syntax represents a predicate lambda function as studied in the Principles of OOP course.
        }

        public void AddUser(int boardID, string userEmail)
        {
            //_boards.Add(new BoardUsersDTO(boardID, userEmail));
        }

        public void RemoveUser(int boardID, string userEmail)
        {
            //_boards.RemoveAll(item => item.BoardID == boardID && item.userName == userEmail);
        }
    }
}