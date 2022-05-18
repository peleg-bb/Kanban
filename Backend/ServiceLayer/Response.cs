using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = System.Xml.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response
    {


        private string ErrorMessage;
        private object ReturnValue;

        // Currently OK Json and BadJson are exactly the same.. Perhaps we should change to GetResponse()?

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
