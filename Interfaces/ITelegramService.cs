using System.Collections.Generic;
using System.Threading.Tasks;
using MukiSearchBot.Models;

namespace MukiSearchBot.Interfaces
{
    public interface ITelegramService
    {
        public Task<List<SearchResult>> FindTittle(string tittle);
    }
}