using OfficeParser.Bot.Logger;
using System;
using System.IO;

namespace OfficeParser.Bot.Services.Managers
{
    public class FileManager : IFileManager
    {
        private readonly ILogger _logger;

        private string docxFilePath = "D://5.docx";
        private string excelFilePath = "D://5.xlsx";
        private string pdfFilePath = "D://5.pdf";
        public FileManager(ILogger logger)
        {
            this._logger = logger;
        }

        public string CreateFileDocx()
        {
            try
            {
                File.Open(docxFilePath, FileMode.Create, FileAccess.ReadWrite);
            }
            catch (Exception error)
            {
                _logger.Handle("Word file yaratishda muammo bor",error);
            }
            return docxFilePath;
        }

        public string CreateFileXls()
        {
            try
            {
                File.Open(excelFilePath, FileMode.Create, FileAccess.ReadWrite);
            }
            catch (Exception error)
            {
                _logger.Handle("Excel file yaratishda muammo bor", error);
            }
            return excelFilePath;
        }

        public string CreateFilePdf()
        {
            return pdfFilePath;
        }
    }
}
