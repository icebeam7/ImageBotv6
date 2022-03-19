namespace ImageBot.Services
{
    public class LuisService
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<string> GetCity(string text, ILogger log)
        {
            try
            {
                var luisFullURL = $"{Constants.LuisEndpoint}&query={text}";
                var luisResult = await httpClient.GetStringAsync(luisFullURL);
                log.LogInformation(luisResult);
                var luisModel = JsonConvert.DeserializeObject<LuisRoot>(luisResult);

                if (luisModel.prediction.topIntent == "GetCityWeather")
                {
                    var entity = luisModel.prediction.entities;
                    return entity?.geographyV2?.FirstOrDefault().value;
                }
                else
                    return "Sorry, I could not understand you!";
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.Message);
            }

            return "Sorry, there was an error!";
        }
    }
}