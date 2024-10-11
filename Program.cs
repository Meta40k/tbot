using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tbot.Configuration;
using tbot.Controllers;
using tbot.Services;
using Telegram.Bot;

namespace tbot;

public class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.Unicode;

        // Объект, отвечающий за постоянный жизненный цикл приложения
        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
            .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
            .Build(); // Собираем

        Console.WriteLine("Сервис запущен");
        // Запускаем сервис
        await host.RunAsync();
        Console.WriteLine("Сервис остановлен");
    }

    static void ConfigureServices(IServiceCollection services)
    {
        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton(BuildAppSettings());
        
        services.AddSingleton<IStorage, MemoryStorage>();
        
        services.AddTransient<DefaultMessageController>();
        services.AddTransient<TextMessageController>();
        
        // Регистрируем постоянно активный сервис бота
        services.AddHostedService<Bot>();
    }
    
    
    static AppSettings BuildAppSettings()
    {
        return new AppSettings()
        {
            BotToken = "8114718234:AAGmj38qL-1sYJpCdw-Ip2dul8zRpnS63n4"
        };
    }
}