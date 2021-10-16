using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Services.Managers
{
    public interface IFileManager
    {
        public string CreateFileDocx();

        public string CreateFileXls();

        public string CreateFilePdf();
    }
}
