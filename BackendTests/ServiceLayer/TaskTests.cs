using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace BackendTests.ServiceLayer
{[TestClass]
    internal class TaskTests

    {
        /// <summary>
        /// This method test if we can create a new task and we get it in the JSON format
        /// for requirement 17 and requirement 4
        /// </summary>
        [TestMethod]
        public void validCreateTaskTest()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            Task task = new Task();
            //act
            Assert.Equals(task.createTask('""Itay@gmail.com',"To Do", title, description, duedate),
                "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        /// <summary>
        /// This method test if we get exeption when create a new task with invalid title
        /// for requirement 17 and requirement 4
        /// </summary>
        [TestMethod]
        public void invalidCreateTaskTest()
        {
            string title = "";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            Task task = new Task();
            //act
            Assert.Equals(task.createTask("Itay@gmail.com","To Do", title, description, duedate),
                "some Exception"
            );
        }
        /// <summary>
        /// This method test if we can edit the title of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void validEditTitleTest()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            int columnOrdinal = 1
            Task task = new Task();
            string newTitle = "new title";
            //act
            string jsonup = task.editTitle("Itay@gmail.com", "ToDo", columnOrdinal, taskid, newTitle );
            Assert.Equals(jsonup,
                "{\"Title\" : \"new title\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        /// <summary>
        /// This method test if we can edit the title of task and we get the exception that you cant't change title to nothing
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void invalidEditTitleTest()
        {
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            int columnOrdinal = 1
            Task task = new Task();
            string newTitle = "";
            //act
            string jsonup = task.editTitle("Itay@gmail.com", "ToDo", columnOrdinal, taskid, newTitle );
            Assert.Equals(jsonup,
                "some Exception"
            );
        }
        /// <summary>
        /// This method test if we can edit the description of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void validEditDescriptionTest()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            int columnOrdinal = 1
            Task task = new Task();
            string newDescription = "Does it change?";
            //act
            string jsonup = task.editDescription("Itay@gmail.com", "ToDo", columnOrdinal, taskid, newDescription );
            Assert.Equals(jsonup,
                "{\"Title\" : \"New Task\", \"Description\" : \"Does it change?\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        /// <summary>
        /// This method test if we can edit the description of task and we get the exception that you cant't change description to nothing
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void invalidEditDescriptionTest()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            int columnOrdinal = 1
            Task task = new Task();
            string newDescription = "";
            //act
            string jsonup = task.editDescription("Itay@gmail.com", "ToDo", columnOrdinal, taskid, newDescription );
            Assert.Equals(jsonup,
                "some Exception"
            );
        }
        /// <summary>
        /// This method test if we can edit the due date of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void validEditDescriptionTest()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            Task task = new Task();
            string newDueDate = "22.04.22";
            //act
            string jsonup = task.editDueDate("Itay@gmail.com", "ToDo", columnOrdinal, taskid, newDueDate );
            Assert.Equals(jsonup,
                "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"22.04.22\"}"
            );
        }
        /// <summary>
        /// This method test if we can edit the due date of task we get the exception that you cant't change DueDate to invalid date
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void validEditDescriptionTest()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            Task task = new Task();
            string newDueDate = "220422";
            //act
            string jsonup = task.editDueDate("Itay@gmail.com", "ToDo", columnOrdinal, taskid, newDueDate );
            Assert.Equals(jsonup,
                "some Exception"
            );
        }
    };


        }

    

