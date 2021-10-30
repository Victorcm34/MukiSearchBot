using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MukiSearchBot.Interfaces;
using MukiSearchBot.Services;
using Telegram.Bot;

namespace MukiSearchBot
{
    public class Worker : BackgroundService
    {
        private readonly ITelegramService _bot;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, ITelegramService bot)
        {
            _bot = bot;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bot.StartBot();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
