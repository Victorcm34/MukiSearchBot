using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using MukiSearchBot.Interfaces;
using Telegram.Bot;

namespace MukiSearchBot.Services
{
    public class TelegramService : ITelegramService
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
            Console.WriteLine("Bot started");
        }
    }
}