using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.Buissnes_Layer;
using Microsoft.VisualBasic;

namespace BackendTests.ServiceLayer
{
    // [TestClass()]
    internal class TestsMain
    {
        private UserController userController;
        public UserService userService;
        public BoardService boardService;
        public TaskService taskService;
        public TestsMain()
        {
            this.userController = new UserController();
            this.userService = new UserService(this.userController);
            this.boardService = new BoardService(this.userController);
            this.taskService = new TaskService(boardService.boardController);
            string email = "test@gmail.com";
            string password = "1234";
            string boardName = "testName";
            string title = "HW";
            string description = "EX3";
            DateTime dueDate = new DateTime(14 / 07 / 2025);
            Console.WriteLine("yayy");
            userService.CreateUser(email, password);
            userService.Login(email, password);

            boardService.CreateBoard(boardName, email);
            boardService.AddTask(email, boardName, title, description, dueDate);
            //TaskTests tests = new TaskTests(taskService, userService, boardService);
        }
        // [TestMethod()]
        static void Main(string[] args)
        {
            // Display the number of command line arguments.
            UserController userController = new UserController();
            UserService userService = new UserService(userController);
            BoardService boardService = new BoardService(userController);
            TaskService taskService = new TaskService(boardService.boardController);
            UserTests userTests = new UserTests(userController, userService);


            string email = "tamar@gmail.com";
            string password = "Ai123456";
            string boardName = "testName";
            string title = "HW";
            string description = "EX3";
            DateTime dueDate = new DateTime(2025,6,15);
            Console.WriteLine("Hello");
            userService.CreateUser(email, password);
            userService.Login(email, password);
            boardService.CreateBoard(boardName, email);
            boardService.AddTask(email, boardName, title, description, dueDate);

            TaskTests tests = new TaskTests(taskService, userService, boardService);

            BoardTest boraTest = new BoardTest(boardService);
            //boraTest.ValidCreateBoardTest();
            //boraTest.InvalidCreateBoardTest();
            //boraTest.InvalidCreateBoardTest2();
            //boraTest.AddValidTaskTest();
            //boraTest.AddInvalidTaskTest2();
            //boraTest.ValidNextStateTest();
            //boraTest.InvalidNextStateTest();
            //boraTest.InvalidNextStateTest2();
            //boraTest.InvalidNextStateTest3();
            //boraTest.ValidDeleteBoardTest();
            //boraTest.InvalidDeleteBoardTest();
            Console.WriteLine("bye");

            //BoardTest boraTest = new BoardTest(boardService);
            //boraTest.AddInvalidTaskTest();
            //UserTests userTests = new UserTests(userController, userService);
            userTests.createUserTest();
            userTests.validUserLoginTest();
            userTests.invalidUserLoginTest();
            userTests.invalidUserCreation();
            userTests.invalidLoginTest_2();
            userTests.invalidUserCreation_2();
            userTests.logoutTest();
            userTests.invalidLogoutTest();



        }

    }
}
