namespace Microshaoft
{
    using System.Threading.Tasks;
    using System.Web.Http;using System.Threading.Tasks;
    using System.Web.Http;
    public class ValuesController : ApiController
    {
        [Route("get1")]
        // GET api/values 
        public async Task<string> Get1()
        {
            await Task.Delay(1000);
            return "I am Get1 !";
        }
        [Route("get2")]
        // GET api/values 
        public async Task<string> Get2()
        {
            await Task.Delay(1000);
            return "I am Get2 !";
        }
        [Route("get3")]
        // GET api/values 
        public async Task<string> Get3()
        {
            await Task.Delay(1000);
            return "I am Get3 !";
        }
    }
}
