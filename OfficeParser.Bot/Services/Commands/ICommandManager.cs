using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace OfficeParser.Bot.Services.Commands
{
    public interface ICommandManager
    {
        [System.Obsolete]
        public Task StartAsync(MessageEventArgs e);

        [System.Obsolete]
        public Task GetDocumentInfoAsync(MessageEventArgs e);

    }
}
