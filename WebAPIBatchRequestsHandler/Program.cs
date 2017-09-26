namespace Microshaoft
{
    using Microsoft.Owin.Hosting;
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    class Program
    {
        const string serviceBaseAddress = "http://localhost:9000/";
        static void Main(string[] args)
        {
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: serviceBaseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();
                var requestBaseAddress = serviceBaseAddress;
                requestBaseAddress = "http://localhost:9000/";
                client.BaseAddress = new Uri(requestBaseAddress);

                //no batch
                Profile("SingleRequest", () =>
                    {
                        Task.WaitAll(
                        client.GetAsync("get1"),
                        client.GetAsync("get2"),
                        client.GetAsync("get3"));
                    });


                //sync batch
                Profile("SyncBatch", () =>
                {
                    var batchrequest = CreateBatch(client, "api/batch");
                    var response = client.SendAsync(batchrequest).Result;
                    Console.WriteLine(response.StatusCode);
                });
                //async batch
                Profile("AsyncBatch", () =>
                {
                    var batchrequest = CreateBatch(client, "api/asyncbatch");
                    var response = client.SendAsync(batchrequest).Result;
                    Console.WriteLine(response.StatusCode);
                });


                Console.ReadLine();
            }

            Console.ReadLine();
        }

        private static void Profile(string name, Action action)
        {
            var watcher = Stopwatch.StartNew();

            action();

            watcher.Stop();

            Console.WriteLine("{0} : {1} ms", name, watcher.ElapsedMilliseconds);
        }

        private static HttpRequestMessage CreateBatch(HttpClient client, string endpoint)
        {
            HttpRequestMessage get1Values = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress+ "get1");
            HttpRequestMessage get2Values = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get2");
            HttpRequestMessage get3Values = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + "get3");


            HttpMessageContent get1Content = new HttpMessageContent(get1Values);
            HttpMessageContent get2Content = new HttpMessageContent(get2Values);
            HttpMessageContent get3Content = new HttpMessageContent(get3Values);

            MultipartContent content = new MultipartContent("mixed", "batch_" + Guid.NewGuid().ToString());
            content.Add(get1Content);
            content.Add(get2Content);
            content.Add(get3Content);

            HttpRequestMessage batchRequest = new HttpRequestMessage(HttpMethod.Post, endpoint);
            //Associate the content with the message
            batchRequest.Content = content;

            return batchRequest;
           
        }
    }
}
