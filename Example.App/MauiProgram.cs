﻿using Example.WebComponents.Data;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Example.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<Insert>();
            builder.Services.AddScoped<Login>();
            builder.Services.AddScoped<Select>();
            return builder.Build();
        }
    }
}