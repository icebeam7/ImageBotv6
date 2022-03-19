namespace ImageBot.Services
{
    public class WeatherService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<string> GetWeather(string city)
        {
            var weatherFullURL = $"{Constants.OpenWeatherMapUrl}" +
                $"?appid={Constants.OpenWeatherMapApiKey}" +
                $"&q={city}";

            var weatherResult = await httpClient.GetStringAsync(weatherFullURL);
            var weatherModel = JsonConvert.DeserializeObject<WeatherModel>(weatherResult);
            weatherModel.main.temp -= 273.15;

            return $"{weatherModel.weather.First().main} ({weatherModel.main.temp.ToString("N2")} °C)";
        }
    }
}
