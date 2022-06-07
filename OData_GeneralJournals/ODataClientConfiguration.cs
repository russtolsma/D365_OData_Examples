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
            ActiveDirectoryResource = "https://rt-dev68c6fa3e95c1f964devaos.axcloud.dynamics.com",
            ActiveDirectoryTenant = "96502bb1-655b-4e84-9ba8-5be61d4cbe5f",
            ActiveDirectoryClientAppId = "78dc099c-ee78-44cc-9b5d-be2f064bc387",
            ActiveDirectoryClientAppSecret = "Rtm8Q~94kHqtG4stmJfCPvYKD61_caUHCpU0vdrE",
            Scopes = new string[] { "https://rt-dev68c6fa3e95c1f964devaos.axcloud.dynamics.com/.default" },
        };
    }
}
