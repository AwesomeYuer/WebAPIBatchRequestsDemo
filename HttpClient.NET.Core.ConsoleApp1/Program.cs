using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;

namespace GL.Multipart.Client
{
    class Program
    {
        const string serviceBaseAddress = "http://localhost:9000/";
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var requestBaseAddress = serviceBaseAddress;
            requestBaseAddress = "http://localhost:9000/";
            client.BaseAddress = new Uri(requestBaseAddress);
            var batchrequest = CreateBatch(client, "api/asyncbatch");
            var response = client.SendAsync(batchrequest).Result;
            var ss = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(ss);
            Console.ReadLine();
        }
        private static HttpRequestMessage CreateBatch(HttpClient client, string endpoint)
        {
            HttpRequestMessage get1Values = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get1?id=11111");
            HttpRequestMessage get2Values = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "post1?id=2222")
            {
                //Content = new StringContent("id=2222")
            };
            

            HttpMessageContent get1Content = new HttpMessageContent(get1Values);
            HttpMessageContent get2Content = new HttpMessageContent(get2Values);
            

            MultipartContent content = new MultipartContent("mixed", "batch_" + Guid.NewGuid().ToString());
            content.Add(get1Content);
            content.Add(get2Content);
            

            HttpRequestMessage batchRequest = new HttpRequestMessage(HttpMethod.Post, endpoint);
            //Associate the content with the message
            batchRequest.Content = content;

            return batchRequest;

        }
    }
}