using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
            if (Application.Current.Properties.ContainsKey("city"))
            {
                var city = Application.Current.Properties["city"] as string;
                OnGetWeather(city);
            }

        }

        async void OnDetailsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Details());
        }

        async void OnGetWeather(string city)
        {
            if (!string.IsNullOrWhiteSpace(city))
            {
                WeatherDataForecast weatherData = await _restService.GetForecastData(GenerateRequestUri(Constants.OpenWeatherMapEndpointForecast, city));
                if (weatherData == null)
                {
                    this.DisplayAlert("", "No internet connection", "OK");
                }
                else
                {
                    weatherData.List[0].Main.Temp = weatherData.List[0].Main.Temp + "°C ";
                    weatherImage.Source = "http://openweathermap.org/img/w/" + weatherData.List[0].Weather[0].Icon + ".png";
                    Day_0.Text = System.DateTime.Now.AddDays(1).DayOfWeek.ToString();
                    Day_1.Text = System.DateTime.Now.AddDays(2).DayOfWeek.ToString();
                    Day_2.Text = System.DateTime.Now.AddDays(3).DayOfWeek.ToString();
                    BindingContext = weatherData;
                    Application.Current.Properties["city"] = city;
                }
            }
        }

        async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CityList());
        }

        protected override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("city"))
            {
                var city = Application.Current.Properties["city"] as string;
                OnGetWeather(city);
            }
            base.OnAppearing();
        }

        string GenerateRequestUri(string endpoint,string city)
        {
            string requestUri = endpoint;
            requestUri += $"?q={city}";
            requestUri += "&units=metric"; // or units= imperial
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
    }
}
