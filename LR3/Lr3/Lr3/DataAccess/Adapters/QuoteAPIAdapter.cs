
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class QuoteAPIAdapter
{
    private readonly HttpClient _httpClient;

    public QuoteAPIAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://zenquotes.io/api/random");
    }

    public async Task<Quote> GetRandomQuoteAsync()
    {
        var response = await _httpClient.GetAsync("random");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var quotes = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(content);

        QuoteDTO quot = new QuoteDTO();

        quot.Content = quotes[0]["q"];
        quot.Author = quotes[0]["a"];

        return QuoteFactory.Create(quot);
    }
}


