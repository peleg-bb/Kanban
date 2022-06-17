using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Mappers
{
    internal class BoardDTOMapper
    {

        private BoardUsersMapper boardUsersMapper;
        private List<BoardDTO> boardDTOs;
        private TaskDTOMapper taskDTOMapper;
        private int boardCount;
        const string tableName = "Boards";
        const string BoardUsersTable = "Board_Users";
        private const string TasksTable = "Tasks";
        private const string idColumn = "ID";
        private const string nameColumn = "Name";
        private const string ownerColumn = "Owner_email";
        private const string backlogMaxColumn = "Backlog_max";
        private const string inProgressMaxColumn = "In_Progress_Max";
        private const string doneMaxColumn = "Done_Max";
        private const int backlogMax = -1; //Default values
        private const int inProgressMax = -1; //Default values
        private const int doneMax = -1; //Default values

        internal BoardDTOMapper()
        {
            this.boardUsersMapper = new BoardUsersMapper();
            this.boardCount = 0;// LoadData and update count
            this.boardDTOs = new List<BoardDTO>();
            this.taskDTOMapper = new TaskDTOMapper();
        }

        internal void AddUserToBoard(int boardID, string email)
        {
            this.boardUsersMapper.AddUserToBoard(boardID, email);
        }

        internal void RemoveUserFromBoard(int boardID, string email)
        {
            this.boardUsersMapper.RemoveUser(boardID, email);
        }

        internal BoardDTO CreateBoard(string ownerEmail, string boardName)
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
                    command.CommandText = $"INSERT INTO {tableName} ({idColumn}, {nameColumn}, {ownerColumn}," +
                                          $"{backlogMaxColumn}, {inProgressMaxColumn}, {doneMaxColumn}) " +
                                          $"VALUES (@id_val, @name_val,@email_val," +
                                          $" @backlog_val,@inProgress_val, @done_val);";

                    SQLiteParameter ownerParam = new SQLiteParameter(@"email_val", ownerEmail);
                    SQLiteParameter nameParam = new SQLiteParameter(@"name_val", boardName);
                    SQLiteParameter idParam = new SQLiteParameter(@"id_val", boardCount + 1);
                    SQLiteParameter backlogParam = new SQLiteParameter(@"backlog_val", backlogMax);
                    SQLiteParameter inProgressParam = new SQLiteParameter(@"inProgress_val", inProgressMax);
                    SQLiteParameter doneParam = new SQLiteParameter(@"done_val", doneMax);


                    command.Parameters.Add(ownerParam);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(backlogParam);
                    command.Parameters.Add(inProgressParam);
                    command.Parameters.Add(doneParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();


                    BoardDTO board = new BoardDTO(owner: ownerEmail,
                        name: boardName, iD: boardCount+1, backlogMax: backlogMax,
                        inProgressMax: inProgressMax, doneMax: doneMax);
                    boardDTOs.Add(board);
                    boardUsersMapper.CreateBoard(boardCount, ownerEmail);
                    boardCount++;
                    return board;
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

                return null; // If failed to create user
            }
        }

        /// <summary>
        /// Do NOT use! Use DeleteBoard(string ownerEmail, string boardName, int boardID)
        /// </summary>
        /// <param name="board_Id"></param>
        /// <returns></returns>
        internal bool DeleteBoard(int board_Id)
        {
            throw new NotImplementedException("Do NOT use! Use DeleteBoard(string ownerEmail, string boardName, int boardID");
        }

        /// <summary>
        /// Deletes a board from the DB
        /// </summary>
        /// <param name="ownerEmail">Email of the board owner</param>
        /// <param name="boardName">Name of the board</param>
        /// <param name="boardID">ID of the board</param>
        internal void DeleteBoard(string ownerEmail, string boardName, int boardID)
        {

            {
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
                string connectionString = $"Data Source={path}; Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    SQLiteCommand command = new SQLiteCommand(null, connection);
                    int res = -1;
                    try
                    {
                        connection.Open();
                        command.CommandText = $"DELETE FROM {tableName} " +
                                              $"WHERE {nameColumn} = @board_name AND" +
                                              $"{ownerColumn} = @username";

                        SQLiteParameter boardParam = new SQLiteParameter(@"board_name", boardName);
                        SQLiteParameter userParam = new SQLiteParameter(@"username", ownerEmail);
                        command.Parameters.Add(boardParam);
                        command.Parameters.Add(userParam);

                        command.Prepare();
                        res = command.ExecuteNonQuery();
                        boardDTOs.RemoveAll(x => x.Owner == ownerEmail && x.Name == boardName && x.ID==boardID);
                        boardUsersMapper.DeleteBoard(boardID);

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

        internal int GetCount()
        {
            return this.boardCount;
        }

        public void ChangeOwnership()
        {

        }

        public List<BoardDTO> LoadBoards()
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
                        int ID = (int)reader["ID"];
                        string owner = reader["Owner_email"].ToString();
                        string name = reader["Name"].ToString();
                        int BacklogMax = (int)reader["Backlog_max"];
                        int InProgressMax = (int)reader["In_Progress_max"];
                        int DoneMax = (int)reader["Done_Max"];
                        BoardDTO board = new BoardDTO(owner: owner,
                            name: name, iD: ID, backlogMax: BacklogMax,
                            inProgressMax: InProgressMax, doneMax: DoneMax);
                        boardDTOs.Add(board);
                        Console.WriteLine("Board " + ID + " loaded successfully");
                    }

                    return boardDTOs;

                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(ex.Message);
                    command.Dispose();
                    connection.Close();
                    throw new DALException($"Delete data failed because " + ex.Message);
                    // log error
                    // Maybe throw an exception? Probs not, might not reach finally
                }
                finally
                {
                    // Console.WriteLine("Reached Finally");
                    command.Dispose();
                    connection.Close();

                }
            }

            List<BoardDTO> ifFailed = new List<BoardDTO>();
            return ifFailed;
        }

        /// <summary>
        /// Deletes all database data from Boards, Board_Users and Tasks
        /// </summary>
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
                    command.CommandText = $"DELETE FROM {tableName};" +
                                          $"DELETE FROM {BoardUsersTable};" +
                                          $"DELETE FROM {TasksTable};";
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    boardUsersMapper.DeleteAllData(); // Deletes all Board_Users table
                    foreach (var boardDTO in boardDTOs)
                    {
                        boardDTO.DeleteAllData();// Deletes all tasks
                    }
                    boardDTOs.Clear();
                    Console.WriteLine($"SQL execution finished without errors. Result: {res} rows changed(deleted)");
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
