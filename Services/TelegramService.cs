using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using MukiSearchBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MukiSearchBot.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly IConfiguration _configuration;
        public TelegramService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string FindTittle(string tittle)
        {
            return "Título";
        }
    }
}