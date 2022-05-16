using CardShow.Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace CardShow.Core;

public class Program
{
    const string connStrKey = "ConnectionString";

    static Dictionary<string, string> Config { get; }
        = new Dictionary<string, string>()
            { [connStrKey] = "Data Source=:memory:" };


    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddLogging(builder);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSingleton<IDbFixture, DbFixture>();

        var app = builder.Build();
        app.MapControllers();
        
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation($"Starting CardShow Core at {DateTime.Now}");
        app.Run();
    }

    private static void AddLogging(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .WriteTo.File(
        $"../.logs/" +
        $"{DateTime.Now:yyMMdd}_core.txt"));
    }
}