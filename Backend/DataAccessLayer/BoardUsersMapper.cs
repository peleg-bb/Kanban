using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardUsersMapper
    {
        private List<BoardUsersDTO> _boards;

        public void CreateBoard(int boardID, string ownerEmail)
        {
            _boards.Add(new BoardUsersDTO(boardID, ownerEmail));
        }

        public void DeleteBoard(int boardID)
        {
            _boards.RemoveAll(itemCollection => boardID == itemCollection.BoardID);
            // Note that this syntax represents a predicate lambda function as studied in the Principles of OOP course.
        }

        public void AddUser(int boardID, string userEmail)
        {
            _boards.Add(new BoardUsersDTO(boardID, userEmail));
        }

        public void RemoveUser(int boardID, string userEmail)
        {
            _boards.RemoveAll(item => (item.BoardID == boardID && item.userName == userEmail));
        }
    }
}
