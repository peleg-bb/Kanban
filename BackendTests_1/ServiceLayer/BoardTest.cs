
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using IntroSE.Kanban.Backend.Buissnes_Layer;
using IntroSE.Kanban.Backend.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendTests.ServiceLayer
{
    internal class BoardTest
    {
        private BoardService _boardService;
        public BoardTest(BoardService BS)
        {
            this._boardService = BS;
        }

        /// <summary>
        /// This method tests a valid creation of a new boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod()]
        public void ValidCreateBoardTest()
        {

            Response r = new Response(null, true);
            string email = "tamar@gmail.com";
            string boardName = "test2";
            Assert.AreEqual(_boardService.CreateBoard(boardName, email), r.OKJson());

        }

        /// <summary>
        /// This method tests a invalid creation of a new boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod()]
        public void InvalidCreateBoardTest()
        {
            Response r = new Response("User does not exist", false);
            string email = "test@gmail.com";
            string boardName = "testName";
            Assert.AreEqual(_boardService.CreateBoard(boardName, email), r.BadJson());

        }
        /// <summary>
        /// This method tests a invalid creation of a new boardService with the same name of a board that already exist  according to requirement  6.
        /// </summary>
        [TestMethod()]
        public void InvalidCreateBoardTest2()
        {
            Response r = new Response("USER CANNOT CREATE A THIS BOARD! USER HAS A BOARD WITH THIS NAME ALREADY", false);
            string email = "tamar@gmail.com";
            string boardName = "testName";
            _boardService.CreateBoard(boardName, email);
            Assert.AreEqual(_boardService.CreateBoard(boardName, email), r.BadJson());

        }
        /// <summary>
        /// This method tests a valid add of a new Task to a boardService in the system according to requirement  12.
        /// check the task was added at the right place(backlog only), and its name is different from the other tasks at the boardService.
        /// </summary>
        [TestMethod()]
        public void AddValidTaskTest()
        {
            
            string email = "tamar@gmail.com";
            string boardName = "testName";
            string title = "HW";
            string description = "EX3";
            DateTime dueDate = new DateTime(14 / 07 / 2025);
            Response r = new Response(null, email);
            Assert.AreEqual(_boardService.AddTask(email, boardName, title, description, dueDate), r.OKJson());

        }
        
        /// <summary>
        /// This method tests a invalid add of a new Task to a boardService in the system according to requirement  12.
        /// check the task was added at the right place(backlog only) and has invalid email.
        /// </summary>
        [TestMethod()]
        public void AddInvalidTaskTest2()
        {
            
            string email = "wrong@gmail.com";
            string password = "1234";
            string boardName = "testName";
            string title = "HW";
            string description = "EX3";
            DateTime dueDate = new DateTime(14 / 07 / 2025);
            Response r = new Response("User does not exist", false);

            Assert.AreEqual(_boardService.AddTask(email, boardName, title, description, dueDate), r.BadJson());
        }
        /// <summary>
        /// This method tests a valid change  state of a TaskService from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test.
        /// </summary>
        [TestMethod]
        public void ValidNextStateTest()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            Response r = new Response(null, true);
            Assert.AreEqual(_boardService.NextState(email, boardName, 0), r.OKJson());

        }
        /// <summary>
        /// This method tests a invalid change state of a TaskService from one state to the next in the system according to requirement  13.
        /// check the task moved for the right next test and invalid email user.
        /// </summary>
        [TestMethod]
        public void InvalidNextStateTest()
        {
            string email = "wrong@gmail.com";
            string boardName = "testName";
            Response r = new Response("User does not exist", false);
            Assert.AreEqual(_boardService.NextState(email, boardName, 0), r.BadJson());

        }
        /// <summary>
        /// This method tests a invalid change  state of a TaskService from one state to the next in the system according to requirement  13.
        /// check the task moved for the right next test and invalid taskId.
        /// </summary>
        [TestMethod]
        public void InvalidNextStateTest2()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            Response r = new Response("TASK Does not exist! ", false);
            Assert.AreEqual(_boardService.NextState(email, boardName, 55), r.BadJson());
        }
        /// <summary>
        /// This method tests a invalid change  state of a TaskService from one state to the next in the system according to requirement  13.
        /// check the task can not be moved over Done state.
        /// </summary>
        [TestMethod]
        public void InvalidNextStateTest3()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            Response r = new Response("TASK STATE CAN'T BE CHANGED! ALREADY AT DONE ", false);
            _boardService.NextState(email, boardName, 0);
            _boardService.NextState(email, boardName, 0);
            Assert.AreEqual(_boardService.NextState(email, boardName, 0), r.BadJson());
        }
        // <summary>
        // This method tests a valid deletion of a  boardService in the system according to requirement 9.
        // </summary>
        [TestMethod]
        public void ValidDeleteBoardTest()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            Response r = new Response(null, true);
            Assert.AreEqual(_boardService.DeleteBoard(boardName, email), r.OKJson());

        }
        /// <summary>
        /// This method tests a invalid deletion of a  boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void InvalidDeleteBoardTest()
        {
            Response r = new Response("BOARD IS NOT EXIST AT THIS USER ! ", false);
            string email = "tamar@gmail.com";
            string boardName = "NotExist";
            Assert.AreEqual(_boardService.DeleteBoard(boardName, email), r.BadJson());
        }
        /// <summary>
        /// This method tests a valid get of InProgress tasks of a user in the system according to requirement 16.
        /// </summary>
        [TestMethod()]
        public void ValidInProgress()
        {
            string email = "tamar@gmail.com";
            Response r = new Response(null, _boardService.boardController.GetAllInPrograss(email));
            Assert.AreEqual(_boardService.InProgress(email), r.OKJson());

        }

        /// <summary>
        /// This method tests a invalid get of InProgress tasks of a user in the system according to requirement 16.
        /// </summary>
        [TestMethod()]
        public void InvalidInProgress()
        {
            Response r = new Response("User does not exist", false);
            string email = "test@gmail.com";
            Assert.AreEqual(_boardService.InProgress(email), r.BadJson());

        }
    
        /// <summary>
        /// This method tests a valid get of a task column of certain board user in the system .
        /// </summary>
        [TestMethod()]
        public void ValidGetColum()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int columnOrdinal = 0;
            Response r = new Response(null, _boardService.boardController.GetBoard(email, boardName).GEtColList(columnOrdinal));
            Assert.AreEqual(_boardService.GetColum(email, boardName, columnOrdinal), r.OKJson());
        }

        /// <summary>
        /// This method tests a invalid get of a task column of certain board user in the system .
        /// check the method throws exception when column does not exist.
        /// </summary>
        [TestMethod()]
        public void InvalidGetColum()
        {

            string email = "tamar@gmail.com";
            string boardName = "testName";
            int columnOrdinal = 3;
            Response r = new Response("this column state does not exist!", false);
            Assert.AreEqual(_boardService.GetColum(email, boardName, columnOrdinal), r.BadJson());
        }
        /// <summary>
        /// This method tests a valid set of a column limit of certain board user in the system according to requirement  10.
        /// </summary>
        [TestMethod]
        public void ValidLimitColumn()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int limit = 10;
            int columnOrdinal = 2;
            Response r = new Response(null, true);
            Assert.AreEqual(_boardService.LimitColumn(email, boardName, columnOrdinal,limit), r.OKJson());

        }
        /// <summary>
        /// This method tests a invalid set of a column limit of certain board user in the system according to requirement  10.
        /// check the method throws exception when column does not exist.
        /// </summary>
        [TestMethod]
        public void InvalidLimitColumn()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int limit = 10;
            int columnOrdinal = 4;
            Response r = new Response("this column state does not exist!", false);
            Assert.AreEqual(_boardService.LimitColumn(email, boardName, columnOrdinal, limit), r.BadJson());

        }
        /// <summary>
        /// This method tests a invalid change  state of a TaskService from one state to the next in the system.
        /// check the task moved for the right next test and invalid taskId.
        /// </summary>
        [TestMethod]
        public void ValidGetColumnName()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int columnOrdinal = 0;
            string colVal = _boardService.boardController.GetBoard(email, boardName).GetNameOrdinal(columnOrdinal);
            Response r = new Response(null, colVal);
            Assert.AreEqual(_boardService.GetColumnName(email, boardName, columnOrdinal), "backlog");
        }
        /// <summary>
        /// This method tests a invalid change
        ///state of a TaskService from one state to the next in the system .
        /// check the task can not be moved over Done state.
        /// </summary>
        [TestMethod]
        public void InvalidGetColumnName()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int columnOrdinal = 3;
            Response r = new Response("this column state does not exist!", false);
            Assert.AreEqual(_boardService.GetColumnName(email, boardName, columnOrdinal), r.BadJson());
        }
        // <summary>
        // This method tests a valid deletion of a  boardService in the system according to requirement 9.
        // </summary>
        [TestMethod]
        public void ValidGetColumnLimit()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int columnOrdinal = 2;
            int colVal = _boardService.boardController.GetBoard(email, boardName).GetMaxTask(columnOrdinal);
            Response r = new Response(null, colVal);
            Assert.AreEqual(_boardService.GetColumnLimit(email, boardName,columnOrdinal),"-1");

        }
        /// <summary>
        /// This method tests a invalid deletion of a  boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void InvalidGetColumnLimit()
        {
            Response r = new Response("this column state does not exist!", false);
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int columnOrdinal = 4;
            Assert.AreEqual(_boardService.GetColumnLimit(email, boardName,columnOrdinal), r.BadJson());
        }
    }
}

