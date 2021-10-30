using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MukiSearchBot.Interfaces;
using MukiSearchBot.Models;
using MukiSearchBot.Services;
using Newtonsoft.Json;
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

        private async void SendMessage(object sender, MessageEventArgs e)
        {
            try
            {
                List<SearchResult> results = await _teleService.FindTittle(e.Message.Text);

                // if (results.Count() > 1)
                // {
                //     IEnumerable<string> options = GetOptions(results);
                //     await _bot.SendPollAsync(e.Message.Chat.Id, "¿Cual estás buscando?", options.Take(4));
                // }
                await _bot.SendPhotoAsync(e.Message.Chat.Id, results[0].Image);
            }
            catch (System.Exception)
            {
                await _bot.SendTextMessageAsync(e.Message.Chat.Id, "Escribe un título de serie o película válido");
            }

        }

        private IEnumerable<string> GetOptions(List<SearchResult> results)
        {
            List<string> options = new List<string>();

            foreach (var item in results)
            {
                options.Add(item.Image);
            }
            return options;
        }
    }
}
