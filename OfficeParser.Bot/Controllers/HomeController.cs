using Microsoft.AspNetCore.Mvc;
using OfficeParser.Bot.Services.Cache;
using OfficeParser.Bot.Services.Commands;
using OfficeParser.Bot.Services.Managers;
using OfficeParser.Bot.Services.Parsers;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace OfficeParser.Bot.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentManager _documentManager;
        private readonly ICleaner _cleaner;
        private TelegramBotClient _client;
        private readonly ICommandManager _commandManager;

        public HomeController(IDocumentManager manager, 
            ICleaner cleaner, TelegramBotClient client,
            ICommandManager commanManager)
        {
            this._documentManager = manager;
            this._cleaner = cleaner;
            this._client = client;
            this._commandManager = commanManager;
        }

        [Obsolete]
        public string Index()
        {
            _client.OnMessage += Notify;

            _client.OnCallbackQuery += CallBack;
            
            _client.StartReceiving();

            return "Bot hozir ishlamoqda";
        }

        [Obsolete]
        private async void CallBack(object sender, CallbackQueryEventArgs e)
        {
            Document document = e.CallbackQuery.Message.Document;
            
            OfficeDocument documentType = 
                _documentManager.DefineDocumentType(e.CallbackQuery.Data);

            string path = await _documentManager.ConvertAsync(document, documentType);

            await _documentManager.SendDocumentAsync(e.CallbackQuery.Message.Chat.Id, path);
            
            _cleaner.Clear();
        }

        [Obsolete]
        private async void Notify(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == "/start")
                await _commandManager.StartAsync(e);

            else if (e.Message.Type.Equals(Telegram.Bot.Types.Enums.MessageType.Document))
                await _commandManager.GetDocumentInfoAsync(e);
        }
    }
}
