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

            board.CreateBoard("schoolBorad", "ptamar@post.bgu.ac.il");
        }
        /// <summary>
        /// This method tests a invalid creation of a new board in the system according to requirement 9.
        /// </summary>
        [TestMethod()]
        public void InvalidCreateBoardTest()
        {

            board.CreateBoard("schoolBorad", "wrong@post.bgu.ac.il");
        }
        /// <summary>
        /// This method tests a valid add of a new Task to a board in the system according to requirement  12.
        /// check the task was added at the right place(backlog only), and its name is different from the other tasks at the board.
        /// </summary>
        [TestMethod()]
        public void AddValidTaskTest()
        {
            board.AddTask("ptamar@post.bgu.ac.il","schoolBorad","HW1", "first homewoek assignmemt", "23.04.22");
        }
        /// <summary>
        /// This method tests a invalid add of a new Task to a board in the system according to requirement  12.
        /// check the task was added at the right place(backlog only), and its name is different from the other tasks at the board.
        /// </summary>
        [TestMethod()]
        public void AddInvalidTaskTest()
        {
            board.AddTask("wrong@post.bgu.ac.il", "schoolBorad", "HW1", "first homewoek assignmemt", "23.04.22");
        }
        /// <summary>
        /// This method tests a valid change  state of a Task from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test.
        /// </summary>
        [TestMethod]
        public void ValidNextStateTest()
        {
            board.NextState("ptamar@post.bgu.ac.il", "schoolBorad", 0, 1281938);
        }
        /// <summary>
        /// This method tests a invalid change  state of a Task from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test.
        /// </summary>
        [TestMethod]
        public void invalidNextStateTest()
        {
            board.NextState("wrong@post.bgu.ac.il", "schoolBorad", 0, 1281938);
        }
        /// <summary>
        /// This method tests a valid deletion of a  board in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void ValidDeleteBoardTest()
        {
            board.DeleteBoard("schoolBorad","ptamar@post.bgu.ac.il");
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
