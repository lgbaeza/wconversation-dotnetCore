using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace WatsonSamples.Models
{
    public class ApiClients
    {
        public static int GetProductPrice(string product, int users, string planType)
        {
            var res = 0;

            string server = "sl-us-south-1-portal.20.dblayer.com",
            port = "34087",
            database = "demo_conta",
            uid = "demo_conta",
            password = "demo_conta";
            string connectionString = "SERVER=" + server + "; port = " + port + ";DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            var connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                string query = "SELECT price from products where productname = '" + product + "' and producttype = '" + planType + "' and users = " + users;

                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                    res = int.Parse(dataReader[0].ToString());

                connection.Close();

            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    
                }
            }

            return res;
        }

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