using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MukiSearchBot.Interfaces;
using MukiSearchBot.Models;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MukiSearchBot.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly IConfiguration _configuration;
        private readonly IimdbApi _imbd;

        public TelegramService(IConfiguration configuration, IimdbApi imbd)
        {
            _configuration = configuration;
            _imbd = imbd;
        }

        public async Task<List<SearchResult>> FindTittle(string tittle)
        {
            SearchData data = await _imbd.GetTittle(tittle);
            return data.Results;
        }
    }
}