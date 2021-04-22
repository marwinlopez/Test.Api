using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Test.Api.Common
{
        public class ResponseApi<T>
        {
            public bool Success { get; internal set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public int? total_elements { get; internal set; }
            public int StatusCode { get; internal set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public T ObjectResponse { get; internal set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public IReadOnlyList<T> ListObjectResponse { get; internal set; }

            public ResponseApi(HttpStatusCode statusCode, T item, List<T> items = null)
            {
                ListObjectResponse = items == null ? null : items;
                ObjectResponse = item;
                Success = (statusCode == HttpStatusCode.Accepted || statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created);
                StatusCode = (int)statusCode;
                if ((item != null) || (items != null))
                    total_elements = items == null ? 1 : items.Count;

            }



        }
}
