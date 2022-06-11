using CardShow.Shared.Interfaces;
using CardShow.Shared.Models;
using CardShow.Shared.Services;
using Serilog;

namespace CardShow.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddLogging(builder);

        builder.Services.AddTransient<IAPIService<Card>, APIService<Card>>();
        builder.Services.AddTransient<IAPIService<CardSet>, APIService<CardSet>>();
        builder.Services.AddTransient<IAPIService<Assessment>, APIService<Assessment>>();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapBlazorHub();

        var logger = app.Services
            .GetRequiredService<
            ILogger<Program>>();
        logger.LogInformation($"Starting CardShow Web at {DateTime.Now}");

        app.Run();
    }

    private static void AddLogging(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .WriteTo.File("../.logs/" +
                $"{DateTime.Now:yyMMdd}" +
                $"_web.txt"));
    }
}

