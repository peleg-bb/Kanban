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

        

        public Response(string errorMessage, object returnValue)
        {
            this.ErrorMessage = errorMessage;
            this.ReturnValue = returnValue;
        }

        public string GradingMessage()
        {
            string json = "{" +
                          $"Error message: {this.ErrorMessage}, ReturnValue: {JsonSerializer.Serialize(this.ReturnValue)}" +
                          "}";
            
            return json;

        }
        
        public string OKJson()
        {
            string json = $"ReturnValue: {JsonSerializer.Serialize(this.ReturnValue)}"; 
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
