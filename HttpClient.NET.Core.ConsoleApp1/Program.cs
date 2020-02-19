using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using Microshaoft;
namespace GL.Multipart.Client
{
    class Program
    {
        const string serviceBaseAddress = "http://localhost:9000/";
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(serviceBaseAddress);
            var batchrequest = client
                                    .CreateBatchHttpRequestMessage
                                            (
                                                "api/asyncbatch"
                                                , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get1?id=11111")
                                                , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "post1?id=2222")
                                            );
            var response = client.SendAsync(batchrequest).Result;
            var ss = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(ss);
            Console.ReadLine();
        }
    }
}