using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;


namespace WeatherApp
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
                WeatherData weatherData = null;
                try
                {
                    var response = await _client.GetAsync(query);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\t\tERROR {0}", ex.Message);
                }

                return weatherData;
            }
            else
            {
                return null;
            }
        }

        public async Task<WeatherDataForecast> GetForecastData(string query)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // NO CONNECTION
                WeatherDataForecast weatherData = null;
                try
                {
                    var response = await _client.GetAsync(query);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        weatherData = JsonConvert.DeserializeObject<WeatherDataForecast>(content);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\t\tERROR {0}", ex.Message);
                }

                return weatherData;
            }
            else
            {
                return null;
            }
        }


    }
}
