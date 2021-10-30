using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MukiSearchBot.Interfaces;
using MukiSearchBot.Services;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MukiSearchBot
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ITelegramBotClient _bot;
        private readonly ITelegramService _teleService;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ITelegramService teleService)
        {
            _configuration = configuration;
            _teleService = teleService;
            _logger = logger;
            _bot = new TelegramBotClient(_configuration["TelegramBotId"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bot.StartReceiving(null, new CancellationToken());
            _bot.OnMessage += SendMessage;
            Console.ReadLine();
        }

        private void SendMessage(object sender, MessageEventArgs e)
        {
            string message = _teleService.FindTittle(e.Message.Text);
            _bot.SendTextMessageAsync(e.Message.Chat.Id, message);
        }
    }
}
