using CardShow.Core.Data;
using CardShow.Data.Contexts;
using CardShow.Data.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSingleton<ICardShowDbFixture, SqliteFixture>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}