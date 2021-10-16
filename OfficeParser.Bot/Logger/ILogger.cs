using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Logger
{
    public interface ILogger
    {
        public void Handle(Exception error);

        public void Handle(string log);

        public void Handle(string log, Exception error);
    }
}
