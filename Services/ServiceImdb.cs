using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MukiSearchBot.Interfaces;
using MukiSearchBot.Models;
using Newtonsoft.Json;

namespace MukiSearchBot.Services
{
    public class ServiceImdb : IimdbApi
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public string Url {get => "https://imdb-api.com/es/API/Search";}

        public ServiceImdb(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<string> FindImage(string tittle)
        {
            var key = _configuration["ImdbApiKey"];
            var response = await _httpClient.GetAsync($"{Url}/{key}/{tittle}");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SearchResult>(json);
            return null;
        }

        public async Task<SearchData> GetTittle(string tittle)
        {
            var key = _configuration["ImdbApiKey"];
            var response = await _httpClient.GetAsync($"{Url}/{key}/{tittle}");
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SearchData>(data);
        }
    }
}