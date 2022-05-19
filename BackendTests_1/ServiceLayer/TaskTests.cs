using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.Buissnes_Layer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = IntroSE.Kanban.Backend.Buissnes_Layer.Task;

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
        public void ValidEditTitleTest()
        {
            string email = "tamar@gmail.com";
            string boardName = "testName";
            int taskId = 0;
            string newTitle = "new title";
            string description = "Does it change?";
            //act
            Response response = new Response(null, new Task(newTitle, new DateTime(2025, 8, 14), description));
            string jsonup = this.taskService.EditTitle(email, boardName, taskId, newTitle);
            Assert.AreEqual(jsonup,
                response.OKJson()
            );
        }

        /// <summary>
        /// This method test if we can edit the title of task and we get the exception that you cant't change title to nothing
        /// for requirement 17 and requirement 14,15
        /// </summary>

        public void InvalidEditTitleTest()
        {
            string email = "test@gmail.com";
            string boardName = "testName";
            int taskId = 0;
            string newTitle = "";
            string description = "Does it change?";
            //act
            Response response = new Response("Value cannot be null.", new Task(newTitle, new DateTime(2025, 8, 14), description));

            string jsonup = this.taskService.EditTitle(email, boardName, taskId, newTitle);
            Assert.AreEqual(jsonup,
                response.BadJson()
            );
        }

        /// <summary>
        /// This method test if we can edit the description of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        public void ValidEditDescriptionTest()
        {
            
            string email = "test@gmail.com";
            string boardName = "testName";
            int taskId = 0;
            string newDescription = "Does it change?";
            string title = "HW";
            Response response = new Response(null,new Task(title,new DateTime(2025, 6, 15), newDescription));
            //act
            string jsonup = this.taskService.EditDescription(email, boardName, taskId, newDescription);
            Assert.AreEqual(jsonup,
                response.OKJson()
            );
        }

        /// <summary>
        /// This method test if we can edit the description of task and we get the exception that you cant't change description to nothing
        /// for requirement 17 and requirement 14,15
        /// </summary>
        public void InvalidEditDescriptionTest()
        {
            string email = "test@gmail.com";
            string boardName = "testName";
            int taskId = 0;
            string newDescription = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids.";
            string title = "HW";
            Response response = new Response("Value cannot be null.", new Task(title, new DateTime(2025, 6, 15), newDescription));

            //act
            string jsonup = this.taskService.EditDescription(email, boardName, taskId, newDescription);
            Assert.AreEqual(jsonup,
                response.BadJson()
            );
        }

        /// <summary>
        /// This method test if we can edit the due date of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        public void ValidEditDueDateTest()
        {
            string email = "test@gmail.com";
            string boardName = "testName";
            int taskId = 0;
            string title = "HW";
            string description = "Does it change?";
            DateTime newDueDate = new DateTime(2025,8,14);
            //act
            Response response = new Response(null, new Task(title, newDueDate, description));

            string jsonup = this.taskService.EditDueDate(email, boardName, taskId, newDueDate);
            Assert.AreEqual(jsonup,
                response.OKJson()
            );
        }

        /// <summary>
        /// This method test if we can edit the due date of task we get the exception that you cant't change DueDate to invalid date
        /// for requirement 17 and requirement 14,15
        /// </summary>
        public void InValidEditDueDateTest()
        {
            string email = "test@gmail.com";
            string boardName = "testName";
            int taskId = 0;
            string title = "HW";
            string description = "EX3";
            DateTime newDueDate = new DateTime(2025 / 08 / 14);
            //act
            Response response = new Response("Value does not fall within the expected range.", new Task(title, newDueDate, description));
            string jsonup = this.taskService.EditDueDate(email, boardName, taskId, newDueDate);
            Assert.AreEqual(jsonup,
                response.BadJson()
            );
        }
    }
}


//}



