using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.Buissnes_Layer;

namespace BackendTests.ServiceLayer
{
    [TestClass()]
    internal class TestsMain
    {
        private UserController userController;
        private BoardController boardController;
        public UserService userService;
        public BoardService boardService;

        public TestsMain()
        {
            this.userController = new UserController();
            this.userService = new UserService(this.userController);
            this.boardService = new BoardService(this.userController);


        }


    }
}
