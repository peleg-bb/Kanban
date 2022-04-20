using System;
using System.Text.Json;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
namespace IntroSE.Kanban.Backend.ServiceLayer

{
    public class Task 
    {

        public string createTask(string title,string description, string dueDate)
        {
            string json = "Not Implemented Yet";
            return json;
        }
        public string editTitle(string newTitle, string userMail, string boardName, string title)
        {

            string json = "Not Implemented Yet";
            return json;

        }
        public string editDescription(string newDescription, string userMail, string boardName, string title)
        {
            string json = "Not Implemented Yet";
            return json;

        }
        public string editDueDate(string newDueDate, string userMail, string boardName, string title)
        {
            string json = "Not Implemented Yet";
            return json;

        }
    }
}