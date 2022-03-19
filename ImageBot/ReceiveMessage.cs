namespace ImageBot
{
    public static class ReceiveMessage
    {
        [FunctionName("receive-message")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string data = await new StreamReader(req.Body).ReadToEndAsync();
            var formValues = data.Split('&')
                .Select(value => value.Split('='))
                .ToDictionary(pair => Uri.UnescapeDataString(pair[0]).Replace("+", " "),
                              pair => Uri.UnescapeDataString(pair[1]).Replace("+", " "));

            var isImage = formValues["NumMedia"].ToString() == "1";
            var message = string.Empty;

            if (isImage) // Picture
            {
                var url = formValues["MediaUrl0"].ToString();
                message = await ComputerVisionService.AnalyzeImage(url);
            }
            else // Text
            {
                var text = formValues["Body"].ToString();
                var city = await LuisService.GetCity(text, log);
                var weather = await WeatherService.GetWeather(city);
                message = $"The weather of {city} is: {weather}";
            }

            var twiml = TwilioService.GetTwilioMessage(message);
            
            return new ContentResult
            {
                Content = twiml, 
                ContentType = "application/xml"
            };
        }
    }
}
