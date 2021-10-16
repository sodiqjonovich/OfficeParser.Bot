using OfficeParser.Bot.Logger;
using OfficeParser.Bot.Services.Cache;
using OfficeParser.Bot.Services.Parsers;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace OfficeParser.Bot.Services.Managers
{
    public class DocumentManager : IDocumentManager
    {
        private readonly ILogger _logger;
        private readonly IParseFactory _parserFactory;
        private readonly ICleaner _cleaner;
        private readonly TelegramBotClient _client;

        public DocumentManager(ILogger logger,
            IParseFactory parseFactory, ICleaner cloneable,
            TelegramBotClient client)
        {
            this._logger = logger;
            this._parserFactory = parseFactory;
            this._cleaner = cloneable;
            this._client = client;
        }

        public OfficeDocument DefineDocumentType(string filename)
        {
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
                return OfficeDocument.Excel;
            else if (filename.EndsWith(".doc") || filename.EndsWith(".docx"))
                return OfficeDocument.Word;
            else if (filename.EndsWith("pdf")) return OfficeDocument.PDF;
            else return OfficeDocument.None;
        }

        private async Task<string> DownloadDocumentAsync(
            TelegramBotClient _client, Document document)
        {
            try
            {
                string fs = AppDomain.CurrentDomain.BaseDirectory;
                var fileInfo = await _client.GetFileAsync(document.FileId);
                var filename = fileInfo.FileId + "." + fileInfo.FilePath.Split('.').Last();
                string strFilePath = fs + filename;
                FileStream saveImageStream = System.IO.File.Open(strFilePath, 
                    FileMode.Create, FileAccess.ReadWrite);
                await _client.DownloadFileAsync(fileInfo.FilePath, 
                    saveImageStream, CancellationToken.None);
                saveImageStream.Dispose();
                return strFilePath;
            }
            catch (Exception error)
            {
                _logger.Handle(error);
                return null;
            }
        }

        public async Task<string> ConvertAsync(
            Document document, OfficeDocument willEditionType)
        {
            string downloadedFilePath = 
                await DownloadDocumentAsync(_client, document);

            _cleaner.CacheFiles.Add(downloadedFilePath);

            OfficeDocument downloadedFileType = 
                DefineDocumentType(downloadedFilePath);

            string clientFilePath = await _parserFactory.ParseAsync(downloadedFileType, 
                willEditionType, downloadedFilePath);

            _cleaner.CacheFiles.Add(clientFilePath);

            return clientFilePath;
        }

        public async Task SendDocumentAsync(long chatId, string path)
        {
            await _client.SendChatActionAsync(chatId, 
                Telegram.Bot.Types.Enums.ChatAction.UploadDocument);

            using (var stream = System.IO.File.OpenRead(path))
            {
                InputOnlineFile file = stream;
                file.FileName = GenerateFileName(path);

                await _client.SendDocumentAsync(
                    chatId: chatId,
                    document: file,
                    caption: "✅ Muvaffaqqiyatli \n @officeParser_bot",
                    disableNotification: true);
            }
        }
        private string GenerateFileName(string path)
        {
            var date = DateTime.Now;
            string name = $"{date.Day}.{date.Month}.{date.Year} ";
            var type = DefineDocumentType(path);
            if (type.Equals(OfficeDocument.Excel)) name += ".xlsx";
            else if (type.Equals(OfficeDocument.Word)) name += ".docx";
            else if (type.Equals(OfficeDocument.PDF)) name += ".pdf";
            return name;
        }
    }
}
