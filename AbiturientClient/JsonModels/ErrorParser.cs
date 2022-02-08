using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.JsonModels
{
    class ErrorParser
    {
        public static List<string> GetErrorsList(string response)
        {
            List<string> list = new List<string>();

            var myJObject = JObject.Parse(response);

            var jsonError = myJObject["errors"];
            var jsonErrors = jsonError.Children();
            foreach(var val in jsonErrors)
            {
                foreach (var stack in val.Children())
                {
                    foreach (var desc in stack.Children())
                    {
                        list.Add(desc.ToString());
                    }
                }
            }

            return list;
        }

    }
}
