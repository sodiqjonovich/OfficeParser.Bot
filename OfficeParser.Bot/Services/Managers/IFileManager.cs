using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Services.Managers
{
    public interface IFileManager
    {
        public bool CreateFolder(string folderPath);

        public bool CheckFolder(string folderPath);

        public bool CreateFile(string filePath);

        public bool CheckFile(string filePath);

        public void WriteToFile(string filePath, List<string> text);

        public string CreateFileDocx();

        public string CreateFileXls();

        public string CreateFilePdf();
    }
}
