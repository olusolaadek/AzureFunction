using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json.Linq;

namespace Function_cs_entity_counter
{
    public   class Counter
    {
        private readonly ILogger _logger;
        public Counter(ILogger logger   )
        {
            _logger = logger;
        }

        public int Count = 0;
        public void Increment()=>Count ++;
        public void Decrement()=>Count--;
        public void End () => Entity.Current.DeleteState();

        [FunctionName( nameof( Counter ) )]
        public static Task Run(
            [EntityTrigger] IDurableEntityContext ctx, ILogger log) 
            => ctx.DispatchAsync<Counter>(log);
        }
    }
}