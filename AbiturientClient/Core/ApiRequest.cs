using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using AbiturientClient.JsonModels;
namespace AbiturientClient.Core
{
    class ApiRequest
    {
        public string Host { get; set; }

        public ApiRequest(string host)
        {

            Host = host;

        }
        
        public async Task<IRestResponse> SendJsonRequestAsync(string url, RequestType type, string json, IList<RestResponseCookie> cookies = null)
        {
            RestClient client = new RestClient(Host);
            IRestRequest request = new RestRequest(url);
            request = request.AddJsonBody(json);

            if (cookies != null)
            {
                foreach (RestResponseCookie cookie in cookies)
                {
                    request.AddCookie(cookie.Name, cookie.Value);
                }
            }

            switch (type)
            {
                case RequestType.Get:
                    return await client.ExecuteGetAsync(request);
                case RequestType.Post:
                    return await client.ExecutePostAsync(request);
                default:
                    return null;
            }
        }

        public async Task<IRestResponse> SendRequestAsync(string url, RequestType type, Dictionary<string, string> parametrs, IList<RestResponseCookie> cookies = null)
        {
            RestClient client = new RestClient(Host);
            var request = new RestRequest(url);

            foreach (var obj in parametrs)
            {
                request.AddParameter(obj.Key, obj.Value, ParameterType.QueryString);
            }
            if (cookies != null)
            {
                foreach (RestResponseCookie cookie in cookies)
                {
                    request.AddCookie(cookie.Name, cookie.Value);
                }
            }

            switch (type)
            {
                case RequestType.Get:
                    return await client.ExecuteGetAsync(request);
                case RequestType.Post:
                    return await client.ExecutePostAsync(request);
                default:
                    return null;
            }
        }
        public async Task<IRestResponse> SendRequestAsync(string url, RequestType type, IList<RestResponseCookie> cookies = null)
        {
            RestClient client = new RestClient(Host);
            var request = new RestRequest(url);

            if (cookies != null)
            {
                foreach (RestResponseCookie cookie in cookies)
                {
                    request.AddCookie(cookie.Name, cookie.Value);
                }
            }

            switch (type)
            {
                case RequestType.Get:
                    return await client.ExecuteGetAsync(request);
                case RequestType.Post:
                    return await client.ExecutePostAsync(request);
                default:
                    return null;
            }
        }
    }
}
