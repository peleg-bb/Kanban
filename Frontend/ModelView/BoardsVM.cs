using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;

namespace Frontend.ModelView
{
    internal class BoardsVM
    {
        private BoardsModel _boardsModel;
        private string _email;
        public BoardsVM(string email)
        {
            _boardsModel = new BoardsModel(email);
            _email = email;
        }

        public Dictionary<int, string> GetBoards(string email)
        {
            return _boardsModel.GetBoardNames(email);
        }
    }
}
