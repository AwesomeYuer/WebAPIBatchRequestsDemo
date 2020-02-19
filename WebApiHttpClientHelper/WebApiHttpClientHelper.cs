namespace Microshaoft
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class HttpClientHelper
    {

        public static IEnumerable<HttpContent> GetHttpContentsAsEnumerable(this HttpResponseMessage target)
        {
            var multipartMemoryStreamProvider = target.Content.ReadAsMultipartAsync().Result;
            var contents = multipartMemoryStreamProvider.Contents;
            foreach (var content in contents)
            {
                yield
                    return
                        content;
            }
        }
        public static IEnumerable<string> GetHttpContentsBodyStringsAsEnumerable(this HttpResponseMessage target)
        {
            var multipartMemoryStreamProvider = target.Content.ReadAsMultipartAsync().Result;
            var contents = multipartMemoryStreamProvider.Contents;
            foreach (var content in contents)
            {
                using (var stream = content.ReadAsStreamAsync().Result)
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        string s;
                        do
                        {
                            s = streamReader.ReadLine();
                        }
                        while
                            (
                                s.Length > 0
                            );
                        s = streamReader.ReadToEnd();
                        streamReader.Close();
                        stream.Close();
                        yield
                            return
                                s;
                    }
                }
            }
        }

        //public static async IAsyncEnumerable<string> GetHttpContentsBodyStringsAsAsyncEnumerable(this HttpResponseMessage target)
        //{
        //    var multipartMemoryStreamProvider = await target.Content.ReadAsMultipartAsync();
        //    var contents = multipartMemoryStreamProvider.Contents;
        //    foreach (var content in contents)
        //    {
        //        using (var stream = await content.ReadAsStreamAsync())
        //        {
        //            using (var streamReader = new StreamReader(stream))
        //            {
        //                string s;
        //                do
        //                {
        //                    s = await streamReader.ReadLineAsync();
        //                }
        //                while
        //                    (
        //                        s.Length > 0
        //                    );
        //                s = await
        //                        streamReader.ReadToEndAsync();
        //                streamReader.Close();
        //                stream.Close();
        //                yield
        //                    return
        //                        s;
        //            }
        //        }
        //    }
        //}


        public static Task<HttpResponseMessage> SendBatchHttpRequestsMessageAsync(this HttpClient target, string relativeUrl, params HttpRequestMessage[] httpRequestMessages)
        {
            var httpRequestMessage = CreateBatchHttpRequestMessage(relativeUrl, httpRequestMessages);
            return
                target.SendAsync(httpRequestMessage);
        }

        //public static HttpRequestMessage CreateBatchHttpRequestsMessage(this HttpClient target, string relativeUrl, params HttpRequestMessage[] httpRequestMessages)
        //{
        //    return CreateBatchHttpRequestMessage(relativeUrl, httpRequestMessages);
        //}
        public static HttpRequestMessage CreateBatchHttpRequestMessage(string relativeUrl, params HttpRequestMessage[] httpRequestMessages)
        {
            MultipartContent multipartContent = null;
            foreach (var httpRequestMessage in httpRequestMessages)
            {
                if (multipartContent == null)
                {
                    multipartContent = new MultipartContent("mixed", "batch_" + Guid.NewGuid().ToString());
                }
                HttpMessageContent httpMessageContent = new HttpMessageContent(httpRequestMessage);
                multipartContent.Add(httpMessageContent);
            }
            HttpRequestMessage batchHttpRequestMessage = null;
            if (multipartContent != null)
            {
                batchHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, relativeUrl)
                {
                    Content = multipartContent
                };
            }
            return batchHttpRequestMessage;
        }
    }
}
