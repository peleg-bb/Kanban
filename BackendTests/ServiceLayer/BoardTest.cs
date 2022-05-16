using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using IntroSE.Kanban.Backend.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Board = IntroSE.Kanban.Backend.ServiceLayer.Board;

namespace BackendTests.ServiceLayer
{
    internal class BoardTest
    {
         Board board = new Board();

        /// <summary>
        /// This method tests a valid creation of a new board in the system according to requirement 9.
        /// </summary>
        [TestMethod()]
        public void ValidCreateBoardTest()
        {

            Assert.Equals(board.CreateBoard("schoolBorad", "ptamar@post.bgu.ac.il"), "{true}");
        }
        /// <summary>
        /// This method tests a invalid creation of a new board in the system according to requirement 9.
        /// </summary>
        [TestMethod()]
        public void InvalidCreateBoardTest()
        {

            Assert.Equals(board.CreateBoard("schoolBorad", "wrong@post.bgu.ac.il"),"Error");
        }
        /// <summary>
        /// This method tests a valid add of a new Task to a board in the system according to requirement  12.
        /// check the task was added at the right place(backlog only), and its name is different from the other tasks at the board.
        /// </summary>
        [TestMethod()]
        public void AddValidTaskTest()
        {
            Assert.Equals( board.AddTask("ptamar@post.bgu.ac.il","schoolBorad", 365879, "first homewoek assignmemt", new DateTime(23/04/22)),"{}");
        }
        /// <summary>
        /// This method tests a invalid add of a new Task to a board in the system according to requirement  12.
        /// check the task was added at the right place(backlog only) and has invalid taskId.
        [TestMethod()]
        public void AddInvalidTaskTest()
        {
            Assert.Equals(board.AddTask("ptamar@post.bgu.ac.il", "schoolBorad", -365879, "first homewoek assignmemt", new DateTime(23 / 04 / 22)),"Error");
        }
        /// <summary>
        /// This method tests a invalid add of a new Task to a board in the system according to requirement  12.
        /// check the task was added at the right place(backlog only) and has invalid email.
        /// </summary>
        [TestMethod()]
        public void AddInvalidTaskTest2()
        {
            Assert.Equals(board.AddTask("wrong@post.bgu.ac.il", "schoolBorad", 365879, "first homewoek assignmemt", new DateTime(23 / 04 / 22)), "Error");
        }
        /// <summary>
        /// This method tests a valid change  state of a Task from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test.
        /// </summary>
        [TestMethod]
        public void ValidNextStateTest()
        {
            Assert.Equals(board.NextState("ptamar@post.bgu.ac.il", "schoolBorad", 365879),"{}");
        }
        /// <summary>
        /// This method tests a invalid change state of a Task from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test and invalid email user.
        /// </summary>
        [TestMethod]
        public void InvalidNextStateTest()
        {
            Assert.Equals(board.NextState("wrong@post.bgu.ac.il", "schoolBorad", 365879),"Error");
        }
        /// <summary>
        /// This method tests a invalid change  state of a Task from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test and invalid taskId.
        /// </summary>
        [TestMethod]
        public void InvalidNextStateTest2()
        {
            Assert.Equals(board.NextState("ptamar@post.bgu.ac.il", "schoolBorad", -365879), "Error");
        }
        /// <summary>
        /// This method tests a valid deletion of a  board in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void ValidDeleteBoardTest()
        {
            Assert.Equals(board.DeleteBoard("schoolBorad","ptamar@post.bgu.ac.il"),"{}");
        }
        /// <summary>
        /// This method tests a invalid deletion of a  board in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void InvalidDeleteBoardTest()
        {
            board.DeleteBoard("NotExcist", "ptamar@post.bgu.ac.il");
        }
    }
}
