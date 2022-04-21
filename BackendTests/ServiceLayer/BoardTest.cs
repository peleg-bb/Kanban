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
        public void CreateBoardTest()
        {

            board.CreateBoard("schoolBorad", "ptamar@post.bgu.ac.il");
        }
        /// <summary>
        /// This method tests a valid add of a new Task to a board in the system according to requirement  12.
        /// check the task was added at the right place(backlog only), and its name is different from the other tasks at the board.
        /// </summary>
        [TestMethod()]
        public void AddTaskTest()
        {
            board.AddTask("ptamar@post.bgu.ac.il","schoolBorad","HW1", "first homewoek assignmemt", "23.04.22");
        }
        /// <summary>
        /// This method tests a valid deletion of a Task from a board in the system.
        /// check the task was deleted from the right place, and the right board.
        /// </summary>
        [TestMethod]
        public void DeleteTaskTest()
        {
            board.DeleteTask(  "HW1");
        }
        /// <summary>
        /// This method tests a valid change  state of a Task from one state to the next in the system according to requirement  13.
        /// check the task mooved for the right next test.
        /// </summary>
        [TestMethod]
        public void NextStateTest()
        {
            board.NextState("backlog");
        }

        /// <summary>
        /// This method tests a valid deletion of a  board in the system according to requirement 9.
        /// </summary>
        [TestMethod]
        public void DeleteBoardTest()
        {
            board.DeleteBoard("schoolBorad","ptamar@post.bgu.ac.il");
        }
    }
}
