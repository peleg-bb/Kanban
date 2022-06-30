using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

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
    }
}
