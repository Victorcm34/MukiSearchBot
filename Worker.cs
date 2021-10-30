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

namespace MukiSearchBot
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ITelegramBotClient _bot;
        private readonly ITelegramService _teleService;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ITelegramBotClient bot, ITelegramService teleService)
        {
            _configuration = configuration;
            _bot = new TelegramBotClient(_configuration["TelegramBotKey"]);
            _teleService = teleService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bot.StartReceiving(null, new CancellationToken());

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
