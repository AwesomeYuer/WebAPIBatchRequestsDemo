namespace Microshaoft
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using WebApi.OutputCache.V2;

    public class ValuesController : ApiController
    {
        [Route("get1")]
        // GET api/values 
        [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        [HttpGet]
        public async Task<string> Get1
                            (
                                string data
                            )
        {
            await Task.Delay(1000);
            return string.Format("I am Get1 ({0})! @ [{1}]", data, DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss.fff"));
        }

        [Route("get2")]
        [HttpGet]
        // GET api/values 
        public async Task<string> Get2()
        {
            await Task.Delay(1000);
            return string.Format("I am Get2 ! @ [{0}]", DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss.fff"));
        }

        [Route("get3")]
        // GET api/values
        [HttpGet]
        public async Task<string> Get3()
        {
            await Task.Delay(1000);
            return string.Format("I am Get3 ! @ [{0}]", DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss.fff"));
        }


        [Route("Post1")]
        // GET api/values
        [HttpPost]
        [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public async Task<string> Post1
                        (
                            [FromBody]
                            JObject data
                        )
        {
            await Task.Delay(1000);
            return string.Format("I am Post1({0}) ! @ [{1}]", data ,DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss.fff"));
        }
    }
}
