using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.WebUtilities;

using Newtonsoft.Json.Linq;

public class WeatherHttpClient
{
  private const string ENDPOINT = "http://api.openweathermap.org/data/2.5/weather";
  private const string DEFAULT_UNITS = "metric";

  private readonly string API_KEY;
  private HttpClient httpClient;

  public WeatherHttpClient()
  {
    API_KEY = System.Environment.GetEnvironmentVariable("OPEN_WEATHER_MAP_API_KEY");
    httpClient = new HttpClient();
  }

  public async Task<Tuple<string, float>> GetWeather(string location)
  {
    var queryArguments = new Dictionary<string, string>()
        {
            { "q", location },
            { "units", DEFAULT_UNITS },
            { "appid", API_KEY }
        };

    var url = QueryHelpers.AddQueryString(ENDPOINT, queryArguments);
    var response = await httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();

    JObject obj = JObject.Parse(content);
    string condition = (string)obj["weather"][0]["main"];
    float temp = (float)obj["main"]["temp"];

    return new Tuple<string, float>(condition, temp);
  }
}
