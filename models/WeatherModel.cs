public class WeatherModel
{
  public string Condition { get; set; }
  public float Temp { get; set; }
  public float TempMin { get; set; }
  public float TempMax { get; set; }

  public WeatherModel(string condition, float temp, float tempMin, float tempMax)
  {
    Condition = condition;
    Temp = temp;
    TempMin = tempMin;
    TempMax = tempMax;
  }
}
