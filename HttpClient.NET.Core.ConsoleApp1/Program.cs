namespace Microshaoft.Tests
{
    using Microshaoft;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;


    class Program
    {
        
        const string serviceBaseAddress = "http://localhost:9000/";
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(serviceBaseAddress);
            var response = await client
                                    .SendBatchAsync
                                        (
                                            "api/asyncbatch"
                                            , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get1?id=1111")
                                            , new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get2?id=2222")
                                        );
            var headers = JsonConvert.SerializeObject(response.Headers);
            Console.WriteLine($"{nameof(headers)}:{headers}");
            var ss = response.GetContentBodyStringsAsEnumerable();
            foreach (var s in ss)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(response.StatusCode);
            
            Console.ReadLine();
        }
    }
}