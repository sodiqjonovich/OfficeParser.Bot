using Microsoft.Extensions.DependencyInjection;
using OfficeParser.Bot.Logger;
using OfficeParser.Bot.Services.Cache;
using OfficeParser.Bot.Services.Commands;
using OfficeParser.Bot.Services.Managers;
using OfficeParser.Bot.Services.Parsers;
using Telegram.Bot;

namespace OfficeParser.Bot.Services
{
    public static class Dependency_Injection
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddTransient<IDocumentManager, DocumentManager>();
            services.AddTransient<IParseFactory, ParserFactory>();
            services.AddTransient<IParser, Parser>();
            services.AddSingleton<ICleaner, Cleaner>();
            services.AddSingleton<ILogger, Logger.Logger>();
            services.AddTransient<IFileManager, FileManager>();
            TelegramBotClient client = 
                new TelegramBotClient("2045751211:AAFMDnxOl_ErYCVkhn7h7PP9nVl1AIEpGTs");
            services.AddSingleton<TelegramBotClient>(client);
            services.AddScoped<ICommandManager, CommandManager>();
        }
    }
}
