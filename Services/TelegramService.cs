using System.Threading;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace MukiSearchBot.Services
{
    public class TelegramService
    {
        private readonly IConfiguration _configuration;
        private ITelegramBotClient _bot;
        public TelegramService(IConfiguration configuration)
        {
            _configuration = configuration;
            _bot = new TelegramBotClient(_configuration["TelegramBotId"]);
        }
        
        public void StartBot()
        {
            _bot.StartReceiving(null, new CancellationToken());
        }
    }
}