using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;


namespace CapaDatos
{
    public class CD_Api
    {

        public class DataObject
        {
            public string token { get; set; }
        }

        public void loging()
        {
            string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/integration/admin/token";
            string urlParameters = "?username=erp_user&password=gX7uEBvVyjuH";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}", d.token);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }

    }
}