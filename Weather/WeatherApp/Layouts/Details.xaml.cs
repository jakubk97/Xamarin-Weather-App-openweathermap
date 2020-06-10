using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {
        RestService _restService;


        public Details()
        {
            InitializeComponent();
            _restService = new RestService();
            if (Application.Current.Properties.ContainsKey("city"))
            {
                var cityName = Application.Current.Properties["city"] as string;
                BindingContext = this;
                GetAPIData(cityName);
            }
        }

        async void GetAPIData(string cityName)
        {
            if (!string.IsNullOrWhiteSpace(cityName))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint, cityName));
                if (weatherData == null)
                {
                    this.DisplayAlert("", "No internet connection", "OK");
                }
                else
                {
                    weatherImage.Source = "http://openweathermap.org/img/w/" + weatherData.Weather[0].Icon + ".png";
                    weatherData.Main.Temperature = weatherData.Main.Temperature + "°C ";
                    weatherData.Main.Humidity = weatherData.Main.Humidity + "%";
                    weatherData.Main.Pressure = weatherData.Main.Pressure + "hPa";
                    BindingContext = weatherData;
                }
            }
        }

        string GenerateRequestUri(string endpoint,string cityName)
        {
            string requestUri = endpoint;
            requestUri += $"?q={cityName}";
            requestUri += "&units=metric"; // or units= imperial
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
    }
}