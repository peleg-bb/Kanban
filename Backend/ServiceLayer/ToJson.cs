using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ToJson
    {
        public static string toJson(object i)
        {
            return JsonSerializer.Serialize(i);
        }
        
    }
}
