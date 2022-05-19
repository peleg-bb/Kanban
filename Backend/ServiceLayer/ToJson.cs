using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ToJson
    {
        /// <summary>
        /// A class to convert a response object into a JSON
        /// </summary>
        public static string toJson(object i)
        {
            return JsonConvert.SerializeObject(i);
        }
        
    }
}
