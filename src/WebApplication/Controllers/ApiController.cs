using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ApiController : Controller
    {
        // GET: /<controller>/initialize
        public string Initialize()
        {
            WCMessageResponse watsonResponse = new WCMessageResponse();
            watsonResponse = WatsonConversationClient.ConverseWatsonInitial().GetAwaiter().GetResult();
            var result = "";

            foreach (var text in watsonResponse.output.text)
                result += text + "";

            return result;
        }
    }
}
