﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ModelView
{
    internal class TasksVM
    {
        private List<Task> tasks;
        public TasksVM()
        {
            tasks = new List<Task>();
        }

        public List<string> GetCol(int colId,string email)
        {
            return new List<string>();
        }
    }
}