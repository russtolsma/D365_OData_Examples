using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OData_GeneralJournals
{
    public class ODataAuthenticationHelper
    {
        /// <summary>
        /// The header to use for OAuth authentication.
        /// </summary>
        public const string OAuthHeader = "Authorization";

         

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public  static async Task<string> GetAuthenticationHeader()
        {
            string aadTenant = ODataClientConfiguration.Config.ActiveDirectoryTenant;
            string aadClientAppId = ODataClientConfiguration.Config.ActiveDirectoryClientAppId;
            string aadClientAppSecret = ODataClientConfiguration.Config.ActiveDirectoryClientAppSecret;
            string aadResource = ODataClientConfiguration.Config.ActiveDirectoryResource;
            string authority = ODataClientConfiguration.Config.Authority;
            string[] scopes = ODataClientConfiguration.Config.Scopes;


            AuthenticationResult authenticationResult = null;

            try
            {
                if (string.IsNullOrEmpty(aadClientAppSecret))
                {
                    Console.WriteLine("Please fill AAD application secret in ODataClientConfiguration if you choose authentication by the application.");
                    throw new Exception("Failed OAuth by empty application secret.");
                }

                var app = ConfidentialClientApplicationBuilder.Create(aadClientAppId)
                    .WithAuthority(authority, aadTenant)
                    .WithClientSecret(aadClientAppSecret)
                    .Build();

                if (app != null)
                {
                    try
                    {
                        authenticationResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                    }
                    catch (MsalException msalex1)
                    {
                        Debug.WriteLine($"MsalException: {msalex1.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to authenticate with AAD by application with exception {0} and the stack trace {1}", ex.ToString(), ex.StackTrace));
                throw new Exception("Failed to authenticate with AAD by application.");
            }

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }
    }
}
