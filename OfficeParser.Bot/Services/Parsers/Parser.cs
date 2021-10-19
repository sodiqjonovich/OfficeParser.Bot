using OfficeParser.Bot.Logger;
using SautinSoft;
using System;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Services.Parsers
{
    public enum Documents
    {
        doc, docx, xls, xlsx, nul
    }
    public class Parser : IParser
    {
        private readonly ILogger _logger;
        public Parser(ILogger logger)
        {
            this._logger = logger;
        }
        private Documents GetType(string file)
        {
            if (file.EndsWith(".docx")) return Documents.docx;
            else if (file.EndsWith(".doc")) return Documents.doc;
            else if (file.EndsWith(".xls")) return Documents.xls;
            else if (file.EndsWith(".xlsx")) return Documents.xlsx;
            else return Documents.nul;
        }

        public async Task<string> ExcelToPdfAsync(string excelFilePath, 
            string pdfFilePath)
        {
            UseOffice u = new UseOffice();

            int ret = u.InitExcel();
             
            if (ret == 1)
                _logger.Handle("MS Excel library Error!");

            if (GetType(excelFilePath).Equals(Documents.xls))
                await Task.Run(() => ret = u.ConvertFile(excelFilePath, pdfFilePath,
                    UseOffice.eDirection.XLS_to_PDF));
            else if (GetType(excelFilePath).Equals(Documents.xlsx))
                await Task.Run(() => ret = u.ConvertFile(excelFilePath, pdfFilePath,
                    UseOffice.eDirection.XLSX_to_PDF));

            u.CloseExcel();

            return pdfFilePath;
        }

        public async Task<string> PdfToWordAsync(string PdfFilePath, 
            string wordFilePath)
        {
            try
            {
                PdfFocus pdf = new PdfFocus();
                pdf.OpenPdf(PdfFilePath);
                if (pdf.PageCount > 0)
                {
                    pdf.WordOptions.Format = PdfFocus.CWordOptions.eWordDocument.Docx;
                    await System.Threading.Tasks.Task.Run(()
                        => pdf.ToWord(wordFilePath));
                }
                pdf.ClosePdf();
                return wordFilePath;
            }
            catch(Exception error)
            {
                _logger.Handle(error);
                return null;
            }
        }

        public async Task<string> WordToPdfAsync(string wordFilePath, 
            string pdfFilePath)
        {
            UseOffice u = new UseOffice();

            int ret = u.InitWord();

            if (ret == 1)
                _logger.Handle("MS Word library Error!");

            if(GetType(wordFilePath).Equals(Documents.doc))
                await Task.Run(()=> ret = u.ConvertFile(wordFilePath, pdfFilePath, 
                    UseOffice.eDirection.DOC_to_PDF));
            else if(GetType(wordFilePath).Equals(Documents.docx))
                await Task.Run(() => ret = u.ConvertFile(wordFilePath, pdfFilePath,
                    UseOffice.eDirection.DOCX_to_PDF));

            u.CloseWord();
            
            return pdfFilePath;
        }

    }

}
