using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public static class WatsonConversationClient
    {
        /// <summary>
        /// Función para las subsecuentes llamadas al servicio de Watson Conversation.
        /// </summary>
        /// <param name="message">Objeto WCMMessage de respuesta obtenido por la función ConverseWatsonInitial</param>
        /// <returns></returns>
        public static async Task<WCMessageResponse> ConverseWatsonInitial()
        {
            
            string wc_username = "99a7746a-9823-403f-be8a-7edb91cbb133";
            string wc_password = "XClGZLxRgk6A";
            string wc_workspaceId = "b4088282-c363-434f-b788-c182f8973dbc";
            string wc_apiv = "2017-05-26";
            string wc_apiendp = "https://gateway.watsonplatform.net/conversation/api/v1/workspaces/" + wc_workspaceId + "/message?version=" + wc_apiv;
            WCMessageRequest body = new WCMessageRequest();
            StringContent inputBody = new StringContent("{\"input\":{\"text\":\"\"}}", Encoding.UTF8,
                                    "application/json");
            WCMessageResponse result = new WCMessageResponse();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
                    wc_username, wc_password))));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage res = await client.PostAsync(wc_apiendp, inputBody))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        
                        Console.WriteLine(data);
                    }
                }
            }


            //body.input = message.input;
            //body.context = message.context;
            
            return result;

        }
        
    }

    
}