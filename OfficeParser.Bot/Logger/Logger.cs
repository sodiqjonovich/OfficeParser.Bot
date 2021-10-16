using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeParser.Bot.Logger
{
    public class Logger : ILogger
    {
        public void Handle(Exception error)
        {
            throw new NotImplementedException();
        }

        public void Handle(string log)
        {
            throw new NotImplementedException();
        }

        public void Handle(string log, Exception error)
        {
            throw new NotImplementedException();
        }
    }
}
