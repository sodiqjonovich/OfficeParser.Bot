using OfficeParser.Bot.Services.Managers;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Services.Parsers
{
    public enum OfficeDocument
    {
        Excel, Word, PDF, PowerPoint, None
    }
    public class ParserFactory:IParseFactory
    {
        private readonly IParser _parser;
        private readonly IFileManager _fileManager;

        public ParserFactory(IParser parser, IFileManager fileManager)
        {
            this._parser = parser;
            this._fileManager = fileManager;
        }
        public async Task<string> ParseAsync(OfficeDocument clientDocument, 
                                     OfficeDocument parseDocument,
                                     string requestFilePath)
        {
            //Pdf Functions
            if(clientDocument.Equals(OfficeDocument.PDF) 
                && parseDocument.Equals(OfficeDocument.Word))
            {
                string responseFilePath =  _fileManager.CreateFileDocx();
                var filePath = await _parser.PdfToWordAsync(
                    requestFilePath, responseFilePath);
                return filePath;
            }
            
            //Word Functions
            else if (clientDocument.Equals(OfficeDocument.Word)
                && parseDocument.Equals(OfficeDocument.PDF))
            {
                string responseFilePath = _fileManager.CreateFilePdf();
                var filePath = await _parser.WordToPdfAsync(
                    requestFilePath, responseFilePath);
                return filePath;
            }
            
            //Excel Functions
            else if (clientDocument.Equals(OfficeDocument.Excel)
                && parseDocument.Equals(OfficeDocument.PDF))
            {
                string responseFilePath = _fileManager.CreateFilePdf();
                var filePath = await _parser.ExcelToPdfAsync(
                    requestFilePath, responseFilePath);
                return filePath;
            }
            return "";
        }
    }
}
