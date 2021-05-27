using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SMarketplace.Application.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AtribuirUserEncoder(this HttpClient client, string username, string password)
        {
            client.AtribuirJsonMediaType();
            string btoa = @$"{username}:{password}";
            byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(btoa);
            string encodedTxt = Convert.ToBase64String(encodedBytes);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", btoa);
        }

        public static void AtribuirJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(" charset=UTF-8"));

        }
    }
}
