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
            string password = "Ai123456";
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


            string email1 = "tamar@gmail.com";
            string password = "Ai123456";
            string boardName = "testName";
            string title = "HW";
            string description = "EX3";
            DateTime dueDate = new DateTime(2025,6,15);
            Console.WriteLine("Hello");
            // userService.CreateUser(email1, password);
            // userService.Login(email1, password);
            // boardService.CreateBoard(boardName, email1);
            // boardService.AddTask(email1, boardName, title, description, dueDate);
            //
            // TaskTests tests = new TaskTests(taskService, userService, boardService);
            // tests.ValidEditDescriptionTest();



            BoardTest boraTest = new BoardTest(boardService);



            //BoardTest boraTest = new BoardTest(boardService);
            //GradingService gradingService = new GradingService();
            //string email = "rrr@gmial.com";
            //string board = "one";
            //gradingService.Register(email, "Aka123k123");
            //gradingService.Login(email, "Aka123k123");
            //string invalid = "jgiosejiooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            //Console.WriteLine(gradingService.AddBoard(email, "one"));
            //Console.WriteLine(gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DateTime.Now));
            //Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 1));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 0));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 1));
            //Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 2, 0));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0)); // no such task in column 0
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 2));
            //Console.WriteLine(gradingService.InProgressTasks(email));
            //Console.WriteLine(gradingService.LimitColumn(email, board, 1, 5));
            //Console.WriteLine(gradingService.LimitColumn(email, board, 1, 4));
            //Console.WriteLine(gradingService.LimitColumn(email, board, 1, 10));
            //Console.WriteLine(gradingService.GetColumnLimit(email, board, 1));
            //Console.WriteLine(gradingService.GetColumnName(email, board, 5)); // INVALID NUMBER
            //Console.WriteLine(gradingService.AddTask(email, "three", "new", "HELLOW WORLD", DateTime.Now)); // no such board three
            //Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 1, 0, DateTime.Now));// not good , changes to task that not in true coloumn number
            //Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 9, 2, DateTime.Now));// not good , changes to invalid coloumn number
            //Console.WriteLine(gradingService.RemoveBoard(email, "one"));
            //Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));

            GradingService gradingService = new GradingService();
            string email = "tamar@gmail.com";
            string board = "one";
            Console.WriteLine(gradingService.Register(email, "Aka123k123"));
            Console.WriteLine(gradingService.Login(email, "Aka123k123"));
            string invalid = "jgiosejiooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            Console.WriteLine(gradingService.Register(email, "Aka123k123"));
            Console.WriteLine(gradingService.Login(email, "Aka123k123"));
            Console.WriteLine(gradingService.Register("klk@klmlk.co.uk", "EEew12221!"));
            Console.WriteLine(gradingService.Register("klk@asd.com", ""));
            Console.WriteLine(gradingService.Register("kl#", "EEew12221!"));
            Console.WriteLine(gradingService.Register("klk@asd.com", "EEew12221!"));
            Console.WriteLine(gradingService.Login("klk@asd.co1", "EEew12221!"));
            Console.WriteLine(gradingService.Register("klk", "EEew12221!"));
            Console.WriteLine(gradingService.Register("klk", "EEew12221!"));

            Console.WriteLine(gradingService.AddBoard(email, "one"));
            Console.WriteLine(gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DateTime.Now));
            Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0));
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 1));
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 0));
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 1));
            Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 2, 0));
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0)); // no such task in column 0
            Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 2));
            Console.WriteLine(gradingService.InProgressTasks(email));
            Console.WriteLine(gradingService.LimitColumn(email, board, 1, 5));
            Console.WriteLine(gradingService.LimitColumn(email, board, 1, 4));
            Console.WriteLine(gradingService.LimitColumn(email, board, 1, 10));
            Console.WriteLine(gradingService.GetColumnLimit(email, board, 1));
            Console.WriteLine(gradingService.GetColumnName(email, board, 5)); // INVALID NUMBER
            Console.WriteLine(gradingService.AddTask(email, "three", "new", "HELLOW WORLD", DateTime.Now)); // no such board three
            Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 1, 0, DateTime.Now));// not good , changes to task that not in true coloumn number
            Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 9, 2, DateTime.Now));// not good , changes to invalid coloumn number
            Console.WriteLine(gradingService.RemoveBoard(email, "one"));
            Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            
            Console.WriteLine(gradingService.AddBoard(email, "two"));
            Console.WriteLine(gradingService.AddTask(email, "two", "new", "HELLOW WORLD", DateTime.Now));
            Console.WriteLine(gradingService.UpdateTaskDueDate(email, "two", 0, 0, DateTime.Now));//
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 0, "new title"));//
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 1, "new title"));//no such task
            Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 0));
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, "new title"));//
            Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, invalid));//invalid title
            Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, "new descp"));
            Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, invalid));//
            Console.WriteLine(gradingService.LimitColumn(email, "two", 1, 1));
            Console.WriteLine(gradingService.AddTask(email, "two", "new task", "HELLOW WORLD", DateTime.Now));
            Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 1)); // the column in full
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
            //boraTest.ValidGetColum();
            //boraTest.InvalidGetColum();
            //boraTest.ValidGetColumnLimit();
            //boraTest.InvalidGetColumnLimit();
            //boraTest.ValidGetColumnName();
            //boraTest.InvalidGetColumnName();
            //boraTest.ValidInProgress();
            //boraTest.InvalidInProgress();
            //boraTest.ValidLimitColumn();
            //boraTest.InvalidLimitColumn();


//             BoardTest boraTest = new BoardTest(boardService);
//             boraTest.ValidCreateBoardTest();
//             boraTest.InvalidCreateBoardTest();
//             boraTest.InvalidCreateBoardTest2();
//             boraTest.AddValidTaskTest();
//             boraTest.AddInvalidTaskTest2();
//             boraTest.ValidNextStateTest();
//             boraTest.InvalidNextStateTest();
//             boraTest.InvalidNextStateTest2();
//             boraTest.InvalidNextStateTest3();
//             boraTest.InvalidDeleteBoardTest();
//             boraTest.ValidGetColum();
//             boraTest.InvalidGetColum();
//             boraTest.ValidGetColumnLimit();
//             boraTest.InvalidGetColumnLimit();
//             boraTest.ValidGetColumnName();
//             boraTest.InvalidGetColumnName();
//             boraTest.ValidInProgress();
//             boraTest.InvalidInProgress();
//             boraTest.ValidLimitColumn();
//             boraTest.InvalidLimitColumn();
//             boraTest.ValidDeleteBoardTest();

            Console.WriteLine("bye");

            //BoardTest boraTest = new BoardTest(boardService);
            //boraTest.AddInvalidTaskTest();
            //UserTests userTests = new UserTests(userController, userService);

            //userTests.createUserTest();
            //userTests.validUserLoginTest();
            //userTests.invalidUserLoginTest();
            //userTests.invalidUserCreation();
            //userTests.invalidLoginTest_2();
            //userTests.invalidUserCreation_2();
            //userTests.logoutTest();
            //userTests.invalidLogoutTest();
            // Console.WriteLine("hey again");
            // GradingService gradingService = new GradingService();
            // string emailll = "whyy@gmail.com";
            // string board1 = "first";
            // gradingService.Register(emailll, "Aka123k123");
            // gradingService.Login(emailll, "Aka123k123");
            // string invalid1 = "jgiosejiooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            // gradingService.AddBoard(emailll, board1);
            // Console.WriteLine(gradingService.AddTask(emailll, board1, "bRAND", "HELLOW WORLD", DateTime.Now));
            // Console.WriteLine(gradingService.AddTask(emailll, board1, "new", "HELLOW WORLD", DateTime.Now));
            // Console.WriteLine(gradingService.AdvanceTask(emailll, board1, 0, 0));
            // Console.WriteLine(gradingService.AdvanceTask(emailll, board1, 1, 0));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 2, 0));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 1));
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 1));
            //Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now)); // no such task in column 0
            //Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 2));

            // userTests.createUserTest();
            // userTests.validUserLoginTest();
            // userTests.invalidUserLoginTest();
            // userTests.invalidUserCreation();
            // userTests.invalidLoginTest_2();
            // userTests.invalidUserCreation_2();
            // userTests.logoutTest();
            // userTests.invalidLogoutTest();
            Console.WriteLine(" ");
            Console.WriteLine("bye bye!");




        }

    }
}
