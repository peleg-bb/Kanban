using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Mappers
{
    internal class BoardDTOMapper
    {

        private BoardUsersMapper usersMapper;
        private int boardCount;

        internal BoardDTOMapper()
        {
            this.usersMapper = new BoardUsersMapper();
            this.boardCount = 0;// LoadData and update count
        }

        internal BoardDTO CreateBoard(string ownerEmail, string boardName)
        {
            usersMapper.CreateBoard(boardCount, ownerEmail);
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
            usersMapper.DeleteBoard(board_Id); // 
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

            usersMapper.DeleteBoard(0);
            usersMapper.DeleteBoard(1);
            return true;
        }

        internal int getCount()
        {
            return this.boardCount;
        }
    }
}
