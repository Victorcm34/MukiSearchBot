using System.Collections.Generic;
using System.Threading.Tasks;
using MukiSearchBot.Models;

namespace MukiSearchBot.Interfaces
{
    public interface IimdbApi
    {
         public string Url { get; }

         public Task<SearchData> GetTittle(string tittle);
         
         
    }
}