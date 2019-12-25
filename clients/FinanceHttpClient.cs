using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.WebUtilities;

using Newtonsoft.Json.Linq;

public class FinanceHttpClient
{
  private const string ENDPOINT = "https://api.twelvedata.com/time_series";
  private const string DEFAULT_INTERVAL = "1min";
  private const string DEFAULT_OUTPUT_SIZE = "1";

  private HttpClient httpClient;
  private readonly string API_KEY;

  public FinanceHttpClient()
  {
    API_KEY = System.Environment.GetEnvironmentVariable("TWELVEDATA_API_KEY");
    httpClient = new HttpClient();
  }

  public async Task<float> GetClosingPrice(string symbol)
  {
    var queryArguments = new Dictionary<string, string>()
        {
            { "symbol", symbol },
            { "interval", DEFAULT_INTERVAL },
            { "outputsize", DEFAULT_OUTPUT_SIZE },
            { "apikey", API_KEY }
        };

    var url = QueryHelpers.AddQueryString(ENDPOINT, queryArguments);
    var response = await httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();

    JObject obj = JObject.Parse(content);
    float exchangeRate = (float)obj["values"][0]["close"];

    return exchangeRate;
  }
}
