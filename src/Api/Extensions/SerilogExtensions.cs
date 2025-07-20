using Serilog.Events;
using Serilog;
using Serilog.Formatting.Elasticsearch;

namespace Api.Extensions;

public static class SerilogExtensions
{
    public static void AddSerilog(this IHostBuilder host)
    {
        host.UseSerilog()
            .ConfigureLogging(logging =>
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                    .MinimumLevel.Override("System", LogEventLevel.Error)
                    .Enrich.FromLogContext()
                    .WriteTo.Async(x =>
                    {
                        x.Console(new ElasticsearchJsonFormatter());
                    })
                    .CreateLogger();
                logging
                    .ClearProviders()
                    .AddSerilog(Log.Logger, dispose: true);
            });
    }
}
