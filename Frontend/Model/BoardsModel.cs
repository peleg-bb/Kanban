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

        public BoardsModel()
        {
            this.serviceFactory = ServiceFactory.getServiceFactrory();
            this.boardService = serviceFactory.boardService;
        }

        public List<string> GetUserBoards(string email)
        {

            string boardNames = boardService.GetUserBoards(email);

            //deserialize boardNames JSON to List<Board>
            List<string> boards = JsonConvert.DeserializeObject<List<string>>(boardNames); 
            // Change so it happens in ServiceLayer?
            return boards;
        }
    }
}
