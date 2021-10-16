using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Services.Cache
{
    public interface ICleaner
    {
        public List<string> CacheFiles { get; set; }
        public void Clear();
    }
}
