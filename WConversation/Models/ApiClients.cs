using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatsonSamples.Models
{
    public class ApiClients
    {
        public static string ValidateRFC(string rfc)
        {
            //Initialize restsharp client
            var rfc_endp = "http://lgbc-contademo-ws-endamebic-gittern.mybluemix.net";
            var client = new RestClient(rfc_endp);
            var request = new RestRequest("validateRFC?rfc={rfc}", Method.GET);

            request.AddUrlSegment("rfc", rfc); // replaces matching token in request.Resource   
            request.AddHeader("Content-Type", "text/plain"); // easily add HTTP Headers
            //request.RequestFormat = DataFormat.Json;

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            var res = "inválido o no pudo evaluarse.";
            if (content != null)
                if (content == "true")
                    res = "válido";
                else
                    res = "inválido";

            return res;
        }
    }
}