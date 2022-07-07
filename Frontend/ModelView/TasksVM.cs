using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Frontend.Model;
using IntroSE.Kanban.Backend.Buissnes_Layer;

namespace Frontend.ModelView
{
    internal class TasksVM
    {
        private TasksModel tasksModel;
        public TasksVM()
        {
            tasksModel = new TasksModel("email", "boardName");
        }

        public List<Task> GetColumn(string email, string boardName, int colId)
        {
            return tasksModel.GetColumn(email, boardName, colId);
        }
       
    }
}
