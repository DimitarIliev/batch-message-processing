using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BulkMessaging
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> log)
        {
            _logger = log;
        }

        [FunctionName("ProcessBulkMessages")]
        public void Run([ServiceBusTrigger("bulk-message", "ConsoleSubscription", Connection = "connectionstring")] string[] mySbMsg)
        {
            foreach (var item in mySbMsg)
            {
                Console.WriteLine(item);
                _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {item}");
            }
        }
    }
}
