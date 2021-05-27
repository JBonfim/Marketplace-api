using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SMarketplace.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AtribuirUserEncoder(this HttpClient client, string username, string password)
        {
            client.AtribuirJsonMediaType();
            string btoa = @$"{username}:{password}";
            byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(btoa);
            string encodedTxt = Convert.ToBase64String(encodedBytes);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedTxt);
        }

        public static void AtribuirJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded; charset=UTF-8"));

        }
    }
}
