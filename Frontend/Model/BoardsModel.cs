using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.Buissnes_Layer;
using IntroSE.Kanban.Backend.ServiceLayer;
using Newtonsoft.Json;

namespace Frontend.Model
{
    

    internal class BoardsModel
    {
        private BoardService boardService;
        private ServiceFactory serviceFactory;
        private string _email;

        public BoardsModel(string email)
        {
            this.serviceFactory = ServiceFactory.getServiceFactrory();
            this.boardService = serviceFactory.boardService;
            this._email = email;
        }

        public Dictionary<int, string> GetBoardNames(string email)
        {
            return boardService.GetUserBoards(email);
        }
        
        

    }
}
