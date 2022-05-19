using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = System.Xml.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response
    {


        public string ErrorMessage { get; }
        public object ReturnValue { get; }


        /// <summary>
        /// A class to convert messages in a middle way between objects and JSONs.
        /// Should be activated by methods in the service layer that instantiate the class
        /// with either an error message or a return value.  
        /// </summary>
        public Response(string errorMessage, object returnValue)
        {
            this.ErrorMessage = errorMessage;
            this.ReturnValue = returnValue;
        }

        public string OKJson()
        {
            string json = $"ReturnValue: {JsonConvert.SerializeObject(this.ReturnValue)}"; 
            return json;
        }

        public string BadJson()
        {
            string json = "{" +
                          $"Error message: {this.ErrorMessage}" +
                          "}";
            return json;
        }
    }
}
