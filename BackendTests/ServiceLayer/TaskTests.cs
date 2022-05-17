
﻿using System;
using System.Runtime.CompilerServices;
using IntroSE.Kanban.Backend.Buissnes_Layer;
using IntroSE.Kanban.Backend.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;


 namespace BackendTests.ServiceLayer
 {
     [TestClass]
     internal class TaskTests


     {
         private readonly TaskService taskService;
         private readonly BoardService boardService;
         private UserService user;

         public TaskTests(TaskService TS, UserService us, BoardService bs)
         {
             this.taskService = TS;
             this.user = us;
             this.boardService = bs;
         }

         /// <summary>
         /// This method test if we can create a new task and we get it in the JSON format
         /// for requirement 17 and requirement 4
         /// </summary>
         //[TestMethod]
         //public void ValidCreateTaskTest()
         //{
         //    string title = "New Task";
         //    string description = "this is check for new task";
         //    string duedate = "21.04.22";
         //    Task task = new Task();
         //    //act
         //    Assert.Equals(task.CreateTask("Itay@gmail.com","To Do", title, description, duedate),
         //        "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
         //    );
         //}
         ///// <summary>
         ///// This method test if we get exception when create a new task with invalid title
         ///// for requirement 17 and requirement 4
         ///// </summary>
         //[TestMethod]
         //public void InvalidCreateTaskTest1()
         //{
         //    string title = "";
         //    string description = "this is check for new task";
         //    string duedate = "21.04.22";
         //    Task task = new Task();
         //    //act
         //    Assert.Equals(task.CreateTask("Itay@gmail.com","To Do", title, description, duedate),
         //        "some Exception"
         //    );
         //}
         ///// <summary>
         ///// This method test if we get exception when create a new task with invalid description
         ///// for requirement 17 and requirement 4
         ///// </summary>
         //[TestMethod]
         //public void InvalidCreateTaskTest2()
         //{
         //    string title = "hello";
         //    string description = "";
         //    string duedate = "21.04.22";
         //    Task task = new Task();
         //    //act
         //    Assert.Equals(task.CreateTask("Itay@gmail.com", "To Do", title, description, duedate),
         //        "some Exception"
         //    );
         //}
         ///// <summary>
         ///// This method test if we get exception when create a new task with invalid due date
         ///// for requirement 17 and requirement 4
         ///// </summary>
         //[TestMethod]
         //public void InvalidCreateTaskTest3()
         //{
         //    string title = "hello";
         //    string description = "new test for new task";
         //    string duedate = "210422";
         //    Task task = new Task();
         //    //act
         //    Assert.Equals(task.CreateTask("Itay@gmail.com", "To Do", title, description, duedate),
         //        "some Exception"
         //    );
         //}
         /// <summary>
         /// This method test if we can edit the title of task and we get it in the JSON format
         /// for requirement 17 and requirement 14,15
         /// </summary>
         [TestMethod]
         public void ValidEditTitleTest()
         {
             string email = "test@gmail";
             string boardName = "testName";
             int taskId = 0;
             string newTitle = "new title";
             //act
             string jsonup = this.taskService.EditTitle(email, boardName, taskId, newTitle);
             Assert.Equals(jsonup,
                 "{\"Title\" : \"new title\", \"Description\" : \"this is check for new taskService\", \"DueDate\" : \"21.04.22\"}"
             );
         }

         /// <summary>
         /// This method test if we can edit the title of task and we get the exception that you cant't change title to nothing
         /// for requirement 17 and requirement 14,15
         /// </summary>
         [TestMethod]
         public void InvalidEditTitleTest()
         {
             string email = "test@gmail";
             string boardName = "testName";
             int taskId = 0;
             string newTitle = "";
             //act
             string jsonup = this.taskService.EditTitle(email, boardName, taskId, newTitle);
             Assert.Equals(jsonup,
                 "some Exception"
             );
         }

         /// <summary>
         /// This method test if we can edit the description of task and we get it in the JSON format
         /// for requirement 17 and requirement 14,15
         /// </summary>
         [TestMethod]
         public void ValidEditDescriptionTest()
         {
             string email = "test@gmail";
             string boardName = "testName";
             int taskId = 0;
             string newDescription = "Does it change?";
             //act
             string jsonup = this.taskService.EditDescription(email, boardName, taskId, newDescription);
             Assert.Equals(jsonup,
                 "{\"Title\" : \"New TaskService\", \"Description\" : \"Does it change?\", \"DueDate\" : \"21.04.22\"}"
             );
         }

         /// <summary>
         /// This method test if we can edit the description of task and we get the exception that you cant't change description to nothing
         /// for requirement 17 and requirement 14,15
         /// </summary>
         [TestMethod]
         public void InvalidEditDescriptionTest()
         {
             string email = "test@gmail";
             string boardName = "testName";
             int taskId = 0;
             string newDescription = "";
             //act
             string jsonup = this.taskService.EditDescription(email, boardName, taskId, newDescription);
             Assert.Equals(jsonup,
                 "some Exception"
             );
         }

         /// <summary>
         /// This method test if we can edit the due date of task and we get it in the JSON format
         /// for requirement 17 and requirement 14,15
         /// </summary>
         [TestMethod]
         public void ValidEditDueDateTest()
         {
             string email = "test@gmail";
             string boardName = "testName";
             int taskId = 0;
             DateTime newDueDate = new DateTime(14 / 08 / 2025);
             //act
             string jsonup = this.taskService.EditDueDate(email, boardName, taskId, newDueDate);
             Assert.Equals(jsonup,
                 "{\"Title\" : \"New TaskService\", \"Description\" : \"this is check for new taskService\", \"DueDate\" : \"22.04.22\"}"
             );
         }

         /// <summary>
         /// This method test if we can edit the due date of task we get the exception that you cant't change DueDate to invalid date
         /// for requirement 17 and requirement 14,15
         /// </summary>
         [TestMethod]
         public void InValidEditDueDateTest()
         {
             string email = "test@gmail";
             string boardName = "testName";
             int taskId = 0;
             DateTime newDueDate = new DateTime(2025 / 08 / 14);
             //act
             string jsonup = this.taskService.EditDueDate(email, boardName, taskId, newDueDate);
             Assert.Equals(jsonup,
                 "some Exception"
             );
         }
     }
 }


//}



