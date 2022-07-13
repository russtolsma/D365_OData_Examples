using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OData_GeneralJournals
{
    public class ODataClientConfiguration
    {
        public string Authority { get; set; }
        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        public string ActiveDirectoryClientAppSecret { get; set; }
        public string[] Scopes { get; set; } 

        public static ODataClientConfiguration Config { get { return ODataClientConfiguration.InitialConfig; } }

        public static ODataClientConfiguration InitialConfig = new ODataClientConfiguration()
        {
            TLSVersion = "",
            Authority = "https://login.microsoftonline.com",
            UriString = "",
            UserName = "",
            Password = "",
            ActiveDirectoryResource = "https://test.axcloud.dynamics.com",
            ActiveDirectoryTenant = "",
            ActiveDirectoryClientAppId = "",
            ActiveDirectoryClientAppSecret = "",
            Scopes = new string[] { "https://test.axcloud.dynamics.com/.default" },
        };
    }
}
