using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WatsonSamples.Models.Json;

namespace WatsonSamples.Models
{
    public static class WatsonConversationClient
    {
        /// <summary>
        /// Función para las subsecuentes llamadas al servicio de Watson Conversation.
        /// </summary>
        /// <param name="message">Objeto WCMMessage de respuesta obtenido por la función ConverseWatsonInitial</param>
        /// <returns></returns>
        public static WCMessageResponse ConverseWatson(WCMessageResponse message)
        {
            string wc_apiendp = "https://gateway.watsonplatform.net/conversation/api/v1";
            string wc_username = ConfigurationManager.AppSettings["wconversation_username"];
            string wc_password = ConfigurationManager.AppSettings["wconversation_password"];
            string wc_workspaceId = ConfigurationManager.AppSettings["wconversation_workspaceID"];
            string wc_apiv = ConfigurationManager.AppSettings["wconversation_apiversion"];
            WCMessageRequest body = new WCMessageRequest();

            //Initialize restsharp client
            var client = new RestClient(wc_apiendp);
            client.Authenticator = new HttpBasicAuthenticator(wc_username, wc_password);
            //client.AddHandler("application/json", NewtonsoftJsonSerializer.Default);
            var request = new RestRequest("workspaces/{workspaceID}/message?version={apiv}", Method.POST);

            request.AddUrlSegment("workspaceID", wc_workspaceId); // replaces matching token in request.Resource   
            request.AddUrlSegment("apiv", wc_apiv); // replaces matching token in request.Resource   
            request.AddHeader("Content-Type", "application/json"); // easily add HTTP Headers
            request.RequestFormat = DataFormat.Json;

            body.input = message.input;
            body.context = message.context;
            request.AddBody(body);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            
            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse<WCMessageResponse> result = client.Execute<WCMessageResponse>(request);
            
            return result.Data;

        }

        /// <summary>
        /// Función para llamar inicialmente al servicio de Conversation, el objeto WCInitialMessage no contiene más que una propiedad "input" con el atributo "text"
        /// </summary>
        /// <param name="message">Objeto con el atributo input.text seteado en cadena vacía ("")</param>
        /// <returns></returns>
        public static WCMessageResponse ConverseWatsonInitial(WCInitialMessage message)
        {
            string wc_apiendp = "https://gateway.watsonplatform.net/conversation/api/v1";
            string wc_username = ConfigurationManager.AppSettings["wconversation_username"];
            string wc_password = ConfigurationManager.AppSettings["wconversation_password"];
            string wc_workspaceId = ConfigurationManager.AppSettings["wconversation_workspaceID"];
            string wc_apiv = ConfigurationManager.AppSettings["wconversation_apiversion"];
            WCInitialMessage body = new WCInitialMessage();

            //Initialize restsharp client
            var client = new RestClient(wc_apiendp);
            client.Authenticator = new HttpBasicAuthenticator(wc_username, wc_password);
            //client.AddHandler("application/json", NewtonsoftJsonSerializer.Default);
            var request = new RestRequest("workspaces/{workspaceID}/message?version={apiv}", Method.POST);

            request.AddUrlSegment("workspaceID", wc_workspaceId); // replaces matching token in request.Resource   
            request.AddUrlSegment("apiv", wc_apiv); // replaces matching token in request.Resource   
            request.AddHeader("Content-Type", "application/json"); // easily add HTTP Headers
            request.RequestFormat = DataFormat.Json;
            
            body.input = message.input;
            request.AddBody(body);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse<WCMessageResponse> result = client.Execute<WCMessageResponse>(request);

            return result.Data;

        }
    }

    
}