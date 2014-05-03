using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Explorers.Web.Infrastructure
{
    public class JsonResponse
    {
        public string Result { get; set; } // ERROR or SUCCESS
        public string Message { get; set; }
        public string Description { get; set; }
        public object Content { get; set; }

        //create success resp
        public JsonResponse(object content)
        {
            Result = "SUCCESS";
            this.Content = content;
        }
        //create error resp
        public JsonResponse(string errorMsg)
        {
            Result = "ERROR";
            Message = errorMsg;
        }

        public JsonResponse(string errorMsg, string errorDescription)
        {
            Result = "ERROR";
            Message = errorMsg;
            Description = errorDescription;
        }
    }
}
