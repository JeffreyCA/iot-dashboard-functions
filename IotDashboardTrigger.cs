using System;
using System.Net;
using System.Net.Http;

using System.Text;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace ca.jeffrey
{
  public static class IotDashboardTrigger
  {
    private static FinanceHttpClient financeHttpClient = new FinanceHttpClient();
    private static WeatherHttpClient weatherHttpClient = new WeatherHttpClient();

    [FunctionName("IotDashboardTrigger")]
    public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");

      // Parse query parameters
      string currencyPair = req.Query["currencypair"];
      string ticker = req.Query["stock"];
      string location = req.Query["location"];

      if (currencyPair == null || ticker == null || location == null)
      {
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
      }

      // Get information
      float exchangeRate = await financeHttpClient.GetClosingPrice(currencyPair);
      float stockPrice = await financeHttpClient.GetClosingPrice(ticker);

      Tuple<string, float> weatherData = await weatherHttpClient.GetWeather(location);
      string condition = weatherData.Item1;
      float temp = weatherData.Item2;

      // Construct response object
      var responseObj = new
      {
        exchange_rate = exchangeRate,
        stock_price = stockPrice,
        condition = condition,
        temp = temp
      };
      var responseJson = JsonConvert.SerializeObject(responseObj);

      // Return resposne message
      return new HttpResponseMessage(HttpStatusCode.OK)
      {
        Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
      };
    }
  }
}
