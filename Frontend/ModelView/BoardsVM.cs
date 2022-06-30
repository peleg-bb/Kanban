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

        public BoardsVM()
        {
            _boardsModel = new BoardsModel();
        }

        public string GetBoards()
        {
            return _boardsModel.GetBoards();
        }
    }
}
