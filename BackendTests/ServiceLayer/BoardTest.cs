using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using IntroSE.Kanban.Backend.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackendTests.ServiceLayer
{
    internal class BoardTest
    {
        BoardService _boardService = new BoardService();

        /// <summary>
        /// This method tests a valid creation of a new boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod()]
        public void ValidCreateBoardTest()
        {

            Assert.Equals(_boardService.CreateBoard("schoolBorad", "ptamar@post.bgu.ac.il"), "{schoolBorad}");

        }
        /// <summary>
        /// This method tests a invalid creation of a new boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod()]
        public void InvalidCreateBoardTest()
        {

            Assert.Equals(_boardService.CreateBoard("schoolBorad", "wrong@post.bgu.ac.il"), "Error");
        }
        /// <summary>
        /// This method tests a valid add of a new Task to a boardService in the system according to requirement  12.
        /// check the task was added at the right place(backlog only), and its name is different from the other tasks at the boardService.
        /// </summary>
        [TestMethod()]
        public void AddValidTaskTest()
        {
            Assert.Equals(_boardService.AddTask("ptamar@post.bgu.ac.il", "schoolBorad", "HW", "first homewoek assignmemt", new DateTime(23 / 04 / 22)), "{}");
        }
        /// <summary>
        /// This method tests a invalid add of a new Task to a boardService in the system according to requirement  12.
        /// check the task was added at the right place(backlog only) and has invalid taskId.
        [TestMethod()]
        public void AddInvalidTaskTest()
        {

            Assert.Equals(_boardService.AddTask("ptamar@post.bgu.ac.il", "schoolBorad", "HW111", "first homewoek assignmemt", new DateTime(23 / 04 / 22)), "Error");
        }
        /// <summary>
        /// This method tests a invalid add of a new Task to a boardService in the system according to requirement  12.
        /// check the task was added at the right place(backlog only) and has invalid email.
        /// </summary>
        [TestMethod()]
        public void AddInvalidTaskTest2()
        {
            Assert.Equals(_boardService.AddTask("wrong@post.bgu.ac.il", "schoolBorad", "HW", "first homewoek assignmemt", new DateTime(23 / 04 / 22)), "Error");
        }
        /// <summary>
        /// This method tests a valid change  state of a TaskService from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test.
        /// </summary>
        [TestMethod]
        public void ValidNextStateTest()
        {
            Assert.Equals(_boardService.NextState("ptamar@post.bgu.ac.il", "schoolBorad", 365879), "{}");
        }
        /// <summary>
        /// This method tests a invalid change state of a TaskService from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test and invalid email user.
        /// </summary>
        [TestMethod]
        public void InvalidNextStateTest()
        {
            Assert.Equals(_boardService.NextState("wrong@post.bgu.ac.il", "schoolBorad", 365879), "Error");
        }
        /// <summary>
        /// This method tests a invalid change  state of a TaskService from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test and invalid taskId.
        /// </summary>
        [TestMethod]
        public void InvalidNextStateTest2()
        {
            Assert.Equals(_boardService.NextState("ptamar@post.bgu.ac.il", "schoolBorad", -365879), "Error");
        }
        /// <summary>
        /// This method tests a valid deletion of a  boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void ValidDeleteBoardTest()
        {
            Assert.Equals(_boardService.DeleteBoard("schoolBorad", "ptamar@post.bgu.ac.il"), "{}");
        }
        /// <summary>
        /// This method tests a invalid deletion of a  boardService in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void InvalidDeleteBoardTest()
        {
            _boardService.DeleteBoard("NotExcist", "ptamar@post.bgu.ac.il");
        }
    }
}
