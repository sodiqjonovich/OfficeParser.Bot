using OfficeParser.Bot.Services.Managers;
using OfficeParser.Bot.Services.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OfficeParser.Bot.Services.Commands
{
    public class CommandManager : ICommandManager
    {
        private readonly TelegramBotClient _client;
        private readonly IDocumentManager _manager;

        public CommandManager(TelegramBotClient client,
            IDocumentManager manager)
        {
            this._client = client;
            this._manager = manager;
        }
        [Obsolete]
        public async Task StartAsync(MessageEventArgs e)
        {
            long userId = e.Message.Chat.Id;
            int msgId = e.Message.MessageId;
            await _client.SendTextMessageAsync(userId,
                "Assalomu Aleykum aziz foydalanuvchi !!! " +
                "O'zgartirilishi kerak bo'lgan faylni yuboring. " +
                "Masalan : Pdf, word, excell kabi turdagi fayllarni " +
                "yuborishingiz mumkin", replyToMessageId: msgId);
        }

        [Obsolete]
        public async Task GetDocumentInfoAsync(MessageEventArgs e)
        {
            long userId = e.Message.Chat.Id;
            int msgId = e.Message.MessageId;
            Document document = e.Message.Document;

            if(CheckDocumentAsync(document, userId, msgId).Result == true)
            {
                var markup = MakeMarkup(document);
                await _client.SendDocumentAsync(
                    chatId: userId,
                    document: e.Message.Document.FileId,
                    caption: "❓❕ Qaysi tipga o'girmoqchisiz ❓",
                    replyMarkup: markup);
            }
        }

        private InlineKeyboardMarkup MakeMarkup(Document document)
        {
            var pdfKeyboard = new InlineKeyboardButton[] {
                            InlineKeyboardButton.WithCallbackData(
                                text: "Pdf ( .pdf )",
                                callbackData: "document.pdf")
                            };
            var excelKeyboard = new InlineKeyboardButton[] {
                            InlineKeyboardButton.WithCallbackData(
                                text: "Excell (.xls, xlsx )",
                                callbackData: "document.xls")
                            };
            var wordKeyboard = new InlineKeyboardButton[] {
                            InlineKeyboardButton.WithCallbackData(
                                text: "Word ( .doc, docx )",
                                callbackData: "document.docx")
                            };
            InlineKeyboardButton[][] buttons;
            OfficeDocument documentType = _manager.DefineDocumentType(document.FileName);
            if (documentType.Equals(OfficeDocument.PDF))
                buttons = new InlineKeyboardButton[][]
                {
                    wordKeyboard
                };
            else if (documentType.Equals(OfficeDocument.Word))
                buttons = new InlineKeyboardButton[][]
                {
                    pdfKeyboard
                };
            else if (documentType.Equals(OfficeDocument.Excel))
                buttons = new InlineKeyboardButton[][]
                {
                    pdfKeyboard
                };
            else buttons = new InlineKeyboardButton[][]{};
            var markup = new InlineKeyboardMarkup(buttons);
            return markup;
        }

        private async Task<bool> CheckDocumentAsync(Document document,
            long userId, int msgId)
        {
            int maxFileSize = 15;
            if (document.FileSize / 1024 / 1024 >= maxFileSize)
            {
                await _client.SendTextMessageAsync(chatId: userId,
                       $"File hajmi {maxFileSize} MB dan katta", 
                       replyToMessageId: msgId);
                return false;
            }
            else if (_manager.DefineDocumentType(document.FileName.ToString())
                    == OfficeDocument.None)
            {
                await _client.SendTextMessageAsync(chatId: userId,
                       "Kechirasiz bunday file bilan Bot ishlay olmaydi", 
                       replyToMessageId: msgId);
                return false;
            }
            else return true;
        }
    }
}