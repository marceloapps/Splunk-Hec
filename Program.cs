using System;
using System.Linq;
using Serilog;
using Serilog.Sinks.Splunk;
using Serilog.Events;
using Serilog.Core;
using Serilog.Formatting.Compact;

namespace Splunk_Hec
{
    public class Program
    {
        const string SPLUNK_FULL_ENDPOINT = "http://localhost:8088/services/collector";
        const string SPLUNK_HEC_TOKEN = "10efbfc2-4a98-4558-abaa-51fc89aa5fe6";

        public static void Main(string[] args)
        {
            Console.WriteLine("Iniciando aplicação");

            var logger = new LoggerConfiguration()
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.EventCollector(SPLUNK_FULL_ENDPOINT, SPLUNK_HEC_TOKEN)
                .Enrich.WithProperty("Timestamp",DateTime.Now)
                .Enrich.WithProperty("Host",System.Net.Dns.GetHostName())
                .CreateLogger();

            var millisecsToWait = 4000;
            logger.Information("Sample app starting up...");
            System.Threading.Thread.Sleep(millisecsToWait);
            logger.Information($"Sending another event after waiting forever....");
            System.Threading.Thread.Sleep(millisecsToWait);
            logger.Information("All done....");            
            System.Threading.Thread.Sleep(millisecsToWait);                            

            Log.CloseAndFlush();
        }
    }
}
