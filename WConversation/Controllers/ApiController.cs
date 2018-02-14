using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatsonSamples.Models;

namespace WatsonSamples.Controllers
{
    public class ApiController : Controller
    {
        public string Initialize()
        {
            var res = "";

            WCInitialMessage msg = new WCInitialMessage("");
            WCMessageResponse watsonResponse = new WCMessageResponse();
            watsonResponse = WatsonConversationClient.ConverseWatsonInitial(msg);
            Session["watsonResponse"] = watsonResponse;

            foreach (var text in watsonResponse.output.text)
                res += "<section class=\"post\"><header class=\"post-header-watson\">" +
                    "<img width = \"48\" height=\"48\" alt=\"img\" class=\"post-avatar-w\"" +
                    "src=\"/Content/img/avatar-watson.png\"><h2 class=\"post-title-watson\">@IBM Watson</h2>" +
                    "</header><div class=\"post-description-watson\">" + text +
                    "</div></section>";

            return res;
        }

        public string SendMessage(string id)
        {
            var res = "";
            bool callbackWatson = false;

            string userInput = id;
            //string wccontext = Session["wccontext"];
            WCMessageResponse watsonResponse = (WCMessageResponse)Session["watsonResponse"];
            watsonResponse.input.text = userInput;
            watsonResponse = WatsonConversationClient.ConverseWatson(watsonResponse);

            res += "<section class=\"post\"><header class=\"post-header-watson\">" +
                    "<img width = \"48\" height=\"48\" alt=\"img\" class=\"pre-avatar-w\"" +
                    "src=\"/Content/img/avatar-user.png\"><h2 class=\"post-title-user\">@Usuario</h2>" +
                    "</header><div class=\"post-description-user\">" + userInput +
                    "</div></section>";

            if(watsonResponse.context.ban_accion != null)
            {
                switch (watsonResponse.context.ban_accion)
                {
                    case "valida_rfc":
                        callbackWatson = true;
                        watsonResponse.context.ban_accion = null;
                        watsonResponse.context.rfc_valido = ApiClients.ValidateRFC(watsonResponse.context.rfc);
                        break;
                    case "consulta_precios":
                        //consulta BD de precios
                        break;
                }
            }

            foreach (var text in watsonResponse.output.text)
                res += "<section class=\"post\"><header class=\"post-header-watson\">" +
                    "<img width = \"48\" height=\"48\" alt=\"img\" class=\"post-avatar-w\"" +
                    "src=\"/Content/img/avatar-watson.png\"><h2 class=\"post-title-watson\">@IBM Watson</h2>" +
                    "</header><div class=\"post-description-watson\">" + text +
                    "</div></section>";

            if (callbackWatson)
            {
                watsonResponse = WatsonConversationClient.ConverseWatson(watsonResponse);
                foreach (var text in watsonResponse.output.text)
                    res += "<section class=\"post\"><header class=\"post-header-watson\">" +
                        "<img width = \"48\" height=\"48\" alt=\"img\" class=\"post-avatar-w\"" +
                        "src=\"/Content/img/avatar-watson.png\"><h2 class=\"post-title-watson\">@IBM Watson</h2>" +
                        "</header><div class=\"post-description-watson\">" + text +
                        "</div></section>";
            }

            Session["watsonResponse"] = watsonResponse;

            return res;
        }

        // GET: Conversation
        public ActionResult Index()
        {
            //Initial call to the service for getting a conversationID and a conversation context
            WCInitialMessage msg = new WCInitialMessage("");
            WCMessageResponse watsonResponse = new WCMessageResponse();
            watsonResponse = WatsonConversationClient.ConverseWatsonInitial(msg);

            foreach(var text in watsonResponse.output.text)
                ViewBag.wc_ResponseWelcome += text + "";

            //Subsequent Watson Conversation calls
            string userInput = "Quiero reservar un vuelo"; //Read user input from UI
            //Assign userinput to the message object, and then pass it to watson client
            watsonResponse.input.text = userInput;
            watsonResponse = WatsonConversationClient.ConverseWatson(watsonResponse);

            //Starting from the watson Response, do the necessary application logic from here to show the conversation on the UI
            foreach (var text in watsonResponse.output.text)
                ViewBag.wc_Response += text + " ";

            return View();
        }
    }
}