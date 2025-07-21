using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public static class MauiProgram
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .Services.AddSingleton<LibraryService>();

        var app = builder.Build();
        ServiceProvider = app.Services;

        return app;
    }
}
