using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Services.Parsers
{
    public interface IParser
    {
        public Task<string> PdfToWordAsync(string pdfFilePath, 
            string wordFilePath);

        public Task<string> WordToPdfAsync(string wordFilePath,
            string pdfFilePath);

        public Task<string> ExcelToPdfAsync(string excelFilePath,
            string pdfFilePath);

    }
}
