using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using IntroSE.Kanban.Backend.ServiceLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task = IntroSE.Kanban.Backend.ServiceLayer.Task;

namespace BackendTests.ServiceLayer
{[TestClass]
    internal class TaskTests

    {
        [TestMethod]
        public void test1()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            Task task = new Task();
            //act
            Assert.Equals(task.createTask(title, description, duedate),
                "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        [TestMethod]
        public void test2()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            Task task = new Task();
            string newTitle = "new title";
            //act
            string jsonup = task.editTitle(newTitle, "Itay@gmail.com", "ToDo", title);
            Assert.Equals(jsonup,
                "{\"Title\" : \"new title\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        [TestMethod]
        public void test3()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            Task task = new Task();
            string newDescription = "Does it change?";
            //act
            string jsonup = task.editDescription(newDescription, "Itay@gmail.com", "ToDo", title);
            Assert.Equals(jsonup,
                "{\"Title\" : \"New Task\", \"Description\" : \"Does it change?\", \"DueDate\" : \"21.04.22\"}"
            );
        }
        [TestMethod]
        public void test4()
        {
            string title = "New Task";
            string description = "this is check for new task";
            string duedate = "21.04.22";
            Task task = new Task();
            string newDueDate = "22.04.22";
            //act
            string jsonup = task.editDueDate(newDueDate, "Itay@gmail.com", "ToDo", title);
            Assert.Equals(jsonup,
                "{\"Title\" : \"New Task\", \"Description\" : \"this is check for new task\", \"DueDate\" : \"22.04.22\"}"
            );
        }
    };


        }

    

