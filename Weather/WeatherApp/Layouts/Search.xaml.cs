using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        RestService _restService;
        List<WeatherData> Cities;
        bool disable = false;
        public Search()
        {
            InitializeComponent();
            _restService = new RestService();
            BindingContext = this;
            InitList();
            InitSearchBar();
        }

        void InitList()
        {
            Cities = new List<WeatherData>();
            listOfCities.ItemsSource = Cities;
            listOfCities.ItemTapped += CityTapped;
        }

        private void CityTapped(object sender, ItemTappedEventArgs e)
        {
            WeatherData item = (WeatherData)e.Item;

            if (this.disable)
                return;

            this.disable = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("", "Do you want to add " + item.Title + "?", "Yes", "No");
                if (result)
                {
                    AddToList(item.Title);
                    this.disable = false;
                    return;
                }
                else
                {
                    this.disable = false;
                    return;
                }

            });
        }

        private void AddToList(string name)
        {
            if (Application.Current.Properties.ContainsKey("list"))
            {
                var list = Application.Current.Properties["list"] as string;
                List<string> listOfCities = JsonConvert.DeserializeObject<List<string>>(list);
                if (listOfCities.Contains(name))
                {
                    this.DisplayAlert("", "Item was already in your list", "OK");
                }
                else
                {
                    listOfCities.Add(name);
                    Application.Current.Properties["list"] = JsonConvert.SerializeObject(listOfCities);
                    this.DisplayAlert("", "Item added", "OK");
                }
            }
            else
            {
                List<string> listOfCities = new List<string>();
                listOfCities.Add(name);
                Application.Current.Properties["list"] = JsonConvert.SerializeObject(listOfCities);
                this.DisplayAlert("", "Item addedd", "OK");
            }


        }

        void InitSearchBar()
        {
            sb_search.TextChanged += (s, e) => FilterItem(sb_search.Text);
            sb_search.SearchButtonPressed += (s, e) => FilterItem(sb_search.Text);
        }

        async void FilterItem(string filter)
        {
            listOfCities.BeginRefresh();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint, filter));

                Cities = new List<WeatherData>();
                if (weatherData != null)
                {
                    Cities.Add(new WeatherData { Title = weatherData.Title });
                    listOfCities.ItemsSource = Cities;
                    listOfCities.ItemTapped += CityTapped;
                    BindingContext = weatherData;
                }
            }
            listOfCities.EndRefresh();
        }

        string GenerateRequestUri(string endpoint, string city)
        {
            string requestUri = endpoint;
            requestUri += $"?q={city}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

    }
}