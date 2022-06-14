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
        private int boardCount;
        const string tableName = "Boards";
        const string BoardUsersTable = "Board_Users";
        private const string TasksTable = "Tasks";

        internal BoardDTOMapper()
        {
            this.boardUsersMapper = new BoardUsersMapper();
            this.boardCount = 0;// LoadData and update count
            this.boardDTOs = new List<BoardDTO>();
            
        }

        internal BoardDTO CreateBoard(string ownerEmail, string boardName)
        {
            // Add to DB
            // Create boardDTO and insert to table
            BoardDTO boardDTO = new BoardDTO();
            this.boardDTOs.Add(boardDTO);
            boardUsersMapper.CreateBoard(boardCount, ownerEmail);
            boardCount++;
            return new BoardDTO();
        }

        /// <summary>
        /// Not yet Implemented!!
        /// </summary>
        /// <param name="board_Id"></param>
        /// <returns></returns>
        internal bool DeleteBoard(int board_Id)
        {
            boardUsersMapper.DeleteBoard(board_Id); // 
            return true;
        }

        /// <summary>
        /// Not yet Implemented!!
        /// </summary>
        /// <param name="ownerEmail"></param>
        /// <param name="boardName"></param>
        /// <returns></returns>
        internal bool DeleteBoard(string ownerEmail, string boardName)
        {

            boardUsersMapper.DeleteBoard(0);
            boardUsersMapper.DeleteBoard(1);
            return true;
        }

        internal int GetCount()
        {
            return this.boardCount;
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
