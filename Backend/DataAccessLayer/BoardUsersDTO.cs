using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardUsersDTO
    {
        public int BoardID;
        public string userName;
        
        public BoardUsersDTO(int boardID, string username)
        {
            this.BoardID = boardID;
            this.userName = username;
        }

        public void AddUser(){}

        public void RemoveUser(){}

        public void DeleteBoard(){}
    }
}
