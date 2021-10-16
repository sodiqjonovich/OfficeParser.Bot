using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OfficeParser.Bot.Logger;

namespace OfficeParser.Bot.Services.Cache
{
    public class Cleaner : ICleaner
    {
        private readonly ILogger _logger;

        public Cleaner(ILogger logger)
        {
            this._logger = logger;
        }
        public List<string> CacheFiles { get; set; } = new List<string>();
        public void Clear()
        {
            try
            {
                foreach (var item in CacheFiles)
                    File.Delete(item);
            }
            catch (Exception error)
            {
                _logger.Handle(error);
            }
        }
    }
}
