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
        public void test1()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            Task task = new Task();
            //act
            Assert.Equals(task.createTask('Itay@gmail.com',"To Do", title, description, duedate),
                "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        /// <summary>
        /// This method test if we can edit the title of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void test2()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            Task task = new Task();
            string newTitle = "new title";
            //act
            string jsonup = task.editTitle(newTitle, "Itay@gmail.com", "ToDo", taskid);
            Assert.Equals(jsonup,
                "{\"Title\" : \"new title\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        /// <summary>
        /// This method test if we can edit the description of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void test3()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            Task task = new Task();
            string newDescription = "Does it change?";
            //act
            string jsonup = task.editDescription(newDescription, "Itay@gmail.com", "ToDo", taskid);
            Assert.Equals(jsonup,
                "{\"Title\" : \"New Task\", \"Description\" : \"Does it change?\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        /// <summary>
        /// This method test if we can edit the due date of task and we get it in the JSON format
        /// for requirement 17 and requirement 14,15
        /// </summary>
        [TestMethod]
        public void test4()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            string taskid = "123";
            Task task = new Task();
            string newDueDate = "22.04.22";
            //act
            string jsonup = task.editDueDate(newDueDate, "Itay@gmail.com", "ToDo", taskid);
            Assert.Equals(jsonup,
                "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"22.04.22\"}"
            );
        }
    };


        }

    

