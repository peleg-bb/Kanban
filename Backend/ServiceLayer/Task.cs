using System;
using System.Text.Json;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
namespace IntroSE.Kanban.Backend.ServiceLayer

{
    public class Task 
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string createTask(string title,string description, string dueDate)
        {
            string json = JsonConvert.SerializeObject
            (
                {Title=title; Description=description; DueDate=dueDate;}
                );
            return json;
        }
        public string editTitle(string newTitle, string userMail, string boardName, string title)
        {

            string json = 

        }
    }
}