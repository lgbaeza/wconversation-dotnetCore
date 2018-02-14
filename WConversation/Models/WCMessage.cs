using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatsonSamples.Models
{
    public class WCInitialMessage
    {
        public WCInitialMessage()
        {

        }
        public WCInitialMessage(string inputText)
        {
            this.input = new WCInput();
            this.input.text = inputText;
        }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WCInput input { get; set; }
    }

    public class WCMessageResponse
    {
        public WCMessageResponse()
        {
            
        }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WCInput input { get; set; }
        public WCOutput output { get; set; }
        public WCContext context { get; set; }
        public List<WCEntity> entities { get; set; }
        public List<WCIntent> intents { get; set; }
    }

    public class WCMessageRequest
    {
        public WCMessageRequest()
        {
        }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WCInput input { get; set; }
        public WCContext context { get; set; }
    }

    public class WCInput
    {
        public WCInput() { }
        public string text { get; set; }
    }

    public class WCOutput
    {
        public WCOutput() { }
        public List<string> text { get; set; }
        public List<string> nodes_visited { get; set; }
        public List<string> log_messages { get; set; }
    }

    public class WCEntity
    {
        public WCEntity() { }
        public string entity { get; set; }
        public string value { get; set; }
        public List<int> location { get; set; }
    }

    public class WCIntent
    {
        public WCIntent() { }
        public string intent { get; set; }
        public float confidence { get; set; }
    }

    public class WCContext
    {
        public WCContext() { }
        public string conversation_id { get; set; }
        public ContextSystem system { get; set; }
        public string ban_accion { get; set; }
        public string rfc { get; set; }
        public string rfc_valido { get; set; }
        public string producto { get; set; }
        public string cantidad_usuarios { get; set; }
        public string precio_producto { get; set; }
    }

    public class ContextSystem
    {
        public ContextSystem() { }
        public List<SystemDialogStack> dialog_stack { get; set; }
        public int dialog_turn_counter { get; set; }
        public int dialog_request_counter { get; set; }
    }

    public class SystemDialogStack
    {
        public SystemDialogStack() { }
        public string dialog_node { get; set; }

    }

}