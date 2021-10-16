using OfficeParser.Bot.Services.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace OfficeParser.Bot.Services.Managers
{
    public interface IDocumentManager
    {
        public OfficeDocument DefineDocumentType(string filename);

        public Task<string> ConvertAsync(
            Document document, OfficeDocument willEditionType);

        public Task SendDocumentAsync(long chatId, string path);
    }
}
