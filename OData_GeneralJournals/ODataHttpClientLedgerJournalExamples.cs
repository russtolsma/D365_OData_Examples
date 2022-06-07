using Microsoft.Dynamics.DataEntities;
using Microsoft.OData.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OData_GeneralJournals
{
    internal class ODataHttpClientLedgerJournalExamples
    {
        private bool initialized = false;
        private HttpClient client = null;
        private string authHeader = null;

        public ODataHttpClientLedgerJournalExamples()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            bool result = false;

            client = new HttpClient();

            Console.WriteLine("Retrieving auth token from Azure...");
            authHeader = ODataAuthenticationHelper.GetAuthenticationHeader().Result;

            if ((client != null) && (authHeader != ""))
            {
                if ((authHeader.Length > 0) && (authHeader != ""))
                {
                    Console.WriteLine("Token retrieved successfully...");

                    // Get the D365 URI and assign to the client
                    client.BaseAddress = new Uri(ODataClientConfiguration.Config.ActiveDirectoryResource);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", authHeader);

                    initialized = true;
                    result = true;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve auth token...");
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public async void GetLedgerJournalsViaODataURI()
        {
            if (initialized)
            {
                HttpResponseMessage response = client.GetAsync("data/LedgerJournalHeaders?$format=json&$filter=IsPosted eq Microsoft.Dynamics.DataEntities.NoYes'No'").Result;

                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string resultSet = await response.Content.ReadAsStringAsync();

                        Console.WriteLine(resultSet);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode.ToString());
                        Console.WriteLine(response.ReasonPhrase.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Cannot retrieve journal as {0} has not been initialized with required values.", this.GetType().ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async void CreateLedgerJournalViaODataURI()
        {
            if (initialized)
            {
                //var journalHeader = new LedgerJournalHeader()
                //{
                //    dataAreaId = "usmf",
                //    JournalName = "GenJrn"
                //};
                //string payLoad = JsonConvert.SerializeObject(journalHeader);

                // Would be great to use the Dynamics Entities for JSON serialization unfortunately the Entity will not serialize correctly due to a case issue
                // within the LedgerJournalHeader entity definition.  So instead in this example I use a literal string to create the simple JSON format.


                string payLoad = @"{""dataAreaId"":""usmf"", ""JournalName"":""GenJrn""}";

                HttpContent httpBody = new StringContent(payLoad, Encoding.UTF8, "application/json");

                if (httpBody != null)
                {
                    HttpResponseMessage response = client.PostAsync("data/LedgerJournalHeaders", httpBody).Result;

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string resultSet = await response.Content.ReadAsStringAsync();

                            Console.WriteLine(resultSet);
                        }
                        else
                        {
                            Console.WriteLine(response.StatusCode.ToString());
                            Console.WriteLine(response.ReasonPhrase.ToString());
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Cannot retrieve journal as {0} has not been initialized with required values.", this.GetType().ToString());
            }
        }
    }
}
