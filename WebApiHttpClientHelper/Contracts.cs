using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Contracts
{

        public interface ITopic
        {
            string Topic { get;}

        }
        public class HttpResult<T> : ITopic
        {
            
            public T Body;

            public string Topic
        
                { get; set; }
        
        }

        public class Aa
        {
            public string F1;
            public int F2;
        }
    public class Ab
    {
        public string F1;
        public int F2;
    }

}
