using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class QuoteService
{

    private QuoteAPIAdapter quoteApiAdapter;

    public QuoteService(HttpClient client)
    {
        quoteApiAdapter = new QuoteAPIAdapter(client);
    }


    public Quote GetQuote()
    {
        return quoteApiAdapter.GetRandomQuoteAsync().GetAwaiter().GetResult();
    }
}
