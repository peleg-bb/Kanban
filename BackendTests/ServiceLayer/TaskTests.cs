﻿//using System.Runtime.CompilerServices;
//using IntroSE.Kanban.Backend.Buissnes_Layer;
//using IntroSE.Kanban.Backend.ServiceLayer;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace BackendTests.ServiceLayer
//{
//    [TestClass]
//    internal class TaskTests

//    {
//        private readonly TaskService taskService;
//        private readonly BoardService boardService;
//        private UserService user;
//        public TaskTests(TaskService TS, UserService us, BoardService bs )
//        {
//            this.taskService = TS;
//            this.user = us;
//            this.boardService = bs;
//        }
//        /// <summary>
//        /// This method test if we can create a new task and we get it in the JSON format
//        /// for requirement 17 and requirement 4
//        /// </summary>
//        //[TestMethod]
//        //public void ValidCreateTaskTest()
//        //{
//        //    string title = "New Task";
//        //    string description = "this is check for new task";
//        //    string duedate = "21.04.22";
//        //    Task task = new Task();
//        //    //act
//        //    Assert.Equals(task.CreateTask("Itay@gmail.com","To Do", title, description, duedate),
//        //        "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
//        //    );
//        //}
//        ///// <summary>
//        ///// This method test if we get exception when create a new task with invalid title
//        ///// for requirement 17 and requirement 4
//        ///// </summary>
//        //[TestMethod]
//        //public void InvalidCreateTaskTest1()
//        //{
//        //    string title = "";
//        //    string description = "this is check for new task";
//        //    string duedate = "21.04.22";
//        //    Task task = new Task();
//        //    //act
//        //    Assert.Equals(task.CreateTask("Itay@gmail.com","To Do", title, description, duedate),
//        //        "some Exception"
//        //    );
//        //}
//        ///// <summary>
//        ///// This method test if we get exception when create a new task with invalid description
//        ///// for requirement 17 and requirement 4
//        ///// </summary>
//        //[TestMethod]
//        //public void InvalidCreateTaskTest2()
//        //{
//        //    string title = "hello";
//        //    string description = "";
//        //    string duedate = "21.04.22";
//        //    Task task = new Task();
//        //    //act
//        //    Assert.Equals(task.CreateTask("Itay@gmail.com", "To Do", title, description, duedate),
//        //        "some Exception"
//        //    );
//        //}
//        ///// <summary>
//        ///// This method test if we get exception when create a new task with invalid due date
//        ///// for requirement 17 and requirement 4
//        ///// </summary>
//        //[TestMethod]
//        //public void InvalidCreateTaskTest3()
//        //{
//        //    string title = "hello";
//        //    string description = "new test for new task";
//        //    string duedate = "210422";
//        //    Task task = new Task();
//        //    //act
//        //    Assert.Equals(task.CreateTask("Itay@gmail.com", "To Do", title, description, duedate),
//        //        "some Exception"
//        //    );
//        //}
//        /// <summary>
//        /// This method test if we can edit the title of task and we get it in the JSON format
//        /// for requirement 17 and requirement 14,15
//        /// </summary>
//        [TestMethod]
//        public void ValidEditTitleTest()
//        {
//            string title = "New TaskService";
//            string description = "this is check for new taskService";
//            string duedate = "21.04.22";
//            int taskid = 123;
//            string newTitle = "new title";
//            //act
//            string jsonup = this.taskService.EditTitle("Itay@gmail.com", taskid, newTitle);
//            Assert.Equals(jsonup,
//                "{\"Title\" : \"new title\", \"Description\" : \"this is check for new taskService\", \"DueDate\" : \"21.04.22\"}"
//            );
//        }
//        /// <summary>
//        /// This method test if we can edit the title of task and we get the exception that you cant't change title to nothing
//        /// for requirement 17 and requirement 14,15
//        /// </summary>
//        [TestMethod]
//        public void InvalidEditTitleTest()
//        {
//            string description = "this is check for new taskService";
//            string duedate = "21.04.22";
//            int taskid = 123;
//            string newTitle = "";
//            //act
//            string jsonup = this.taskService.EditTitle("Itay@gmail.com", boardName, taskid, newTitle);
//            Assert.Equals(jsonup,
//                "some Exception"
//            );
//        }
//        /// <summary>
//        /// This method test if we can edit the description of task and we get it in the JSON format
//        /// for requirement 17 and requirement 14,15
//        /// </summary>
//        [TestMethod]
//        public void ValidEditDescriptionTest()
//        {
//            string title = "New TaskService";
//            string description = "this is check for new taskService";
//            string duedate = "21.04.22";
//            int taskid = 123;
//            string newDescription = "Does it change?";
//            //act
//            string jsonup = this.taskService.EditDescription("Itay@gmail.com", taskid, newDescription);
//            Assert.Equals(jsonup,
//                "{\"Title\" : \"New TaskService\", \"Description\" : \"Does it change?\", \"DueDate\" : \"21.04.22\"}"
//            );
//        }
//        /// <summary>
//        /// This method test if we can edit the description of task and we get the exception that you cant't change description to nothing
//        /// for requirement 17 and requirement 14,15
//        /// </summary>
//        [TestMethod]
//        public void InvalidEditDescriptionTest()
//        {
//            string title = "New TaskService";
//            string description = "this is check for new taskService";
//            string duedate = "21.04.22";
//            int taskid = 123;
//            string newDescription = "";
//            //act
//            string jsonup = this.taskService.EditDescription("Itay@gmail.com", taskid, newDescription);
//            Assert.Equals(jsonup,
//                "some Exception"
//            );
//        }
//        /// <summary>
//        /// This method test if we can edit the due date of task and we get it in the JSON format
//        /// for requirement 17 and requirement 14,15
//        /// </summary>
//        [TestMethod]
//        public void ValidEditDueDateTest()
//        {
//            string title = "New TaskService";
//            string description = "this is check for new taskService";
//            string duedate = "21.04.22";
//            int taskid = 123;
//            string newDueDate = "22.04.22";
//            //act
//            string jsonup = this.taskService.EditDueDate("Itay@gmail.com", taskid, newDueDate);
//            Assert.Equals(jsonup,
//                "{\"Title\" : \"New TaskService\", \"Description\" : \"this is check for new taskService\", \"DueDate\" : \"22.04.22\"}"
//            );
//        }
//        /// <summary>
//        /// This method test if we can edit the due date of task we get the exception that you cant't change DueDate to invalid date
//        /// for requirement 17 and requirement 14,15
//        /// </summary>
//        [TestMethod]
//        public void InValidEditDueDateTest()
//        {
//            string title = "New TaskService";
//            string description = "this is check for new taskService";
//            string duedate = "21.04.22";
//            int taskid = 123;
//            string newDueDate = "220422";
//            //act
//            string jsonup = this.taskService.EditDueDate("Itay@gmail.com", taskid, newDueDate);
//            Assert.Equals(jsonup,
//                "some Exception"
//            );
//        }
//    };


//}



