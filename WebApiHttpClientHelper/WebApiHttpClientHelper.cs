namespace Microshaoft
{
    using System;
    using System.Net;
    using System.Net.Http;
    public static class HttpClientHelper
    {

        public static HttpRequestMessage CreateBatchHttpRequestMessage(this HttpClient target, string relativeUrl, params HttpRequestMessage[] httpRequestMessages)
        {
            return CreateBatchHttpRequestMessage(relativeUrl, httpRequestMessages);
        }

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
