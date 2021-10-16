using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Services.Parsers
{
    public interface IParseFactory
    {
        public Task<string> ParseAsync(OfficeDocument clientDocument, 
            OfficeDocument parseDocument, string filePath);
    }
}
