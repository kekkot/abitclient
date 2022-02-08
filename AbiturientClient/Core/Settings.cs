using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.Core
{
    public static class Settings
    {
        public static string Host = ConfigurationManager.AppSettings["domainName"];
        public static IList<RestResponseCookie> Cookies { get; set; }
    }


}
