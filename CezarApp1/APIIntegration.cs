using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CezarApp1
{
    internal class APIIntegration
    {


        public class SearchResult
        {
            [JsonPropertyName("word")]
            public string Word { get; set; }

            [JsonPropertyName("meanings")]
            public List<Meaning> Meanings { get; set; }
        }

        public class Meaning
        {
            [JsonPropertyName("partOfSpeech")]
            public string PartOfSpeech { get; set; }

            [JsonPropertyName("definitions")]
            public List<Definition> Definitions { get; set; }
        }

        public class Definition
        {
            [JsonPropertyName("definition")]
            public string DefinitionText { get; set; }

            [JsonPropertyName("example")]
            public string Example { get; set; }
        }

        private readonly HttpClient _httpClient;

        public APIIntegration()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.dictionaryapi.dev/api/");
        }

        public static async Task<List<SearchResult>> SearchLatinWord(string word)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.dictionaryapi.dev/api/v2/entries/en/");
                var response = await httpClient.GetAsync(word);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var searchResults = JsonSerializer.Deserialize<List<SearchResult>>(jsonString);
                    return searchResults;
                }
                else
                {
                    return new List<SearchResult>();
                }
            }
        }
    }
}
