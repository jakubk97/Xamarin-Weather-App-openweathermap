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
    public partial class CityList : ContentPage
    {
        List<WeatherData> Cities;
        public CityList()
        {
            InitializeComponent();
            BindingContext = this;
            InitList();
        }

        void InitList()
        {
            Cities = new List<WeatherData>();
            if (Application.Current.Properties.ContainsKey("list"))
            {
                listOfCities.BeginRefresh();
                string list = Application.Current.Properties["list"] as string;
                List<string> cityList = JsonConvert.DeserializeObject<List<string>>(list);

                foreach (string item in cityList.ToArray())
                {
                    Cities.Add(new WeatherData { Title = item });
                }
                listOfCities.ItemsSource = Cities;
                listOfCities.ItemTapped += CityTapped;
                BindingContext = this;
                listOfCities.EndRefresh();
            }
        }

        private void CityTapped(object sender, ItemTappedEventArgs e)
        {
            WeatherData item = (WeatherData)e.Item;

            Application.Current.Properties["city"] = item.Title;
            Application.Current.MainPage.Navigation.PopAsync();

        }

        async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Search());
        }

        protected override void OnAppearing()
        {
            BindingContext = this;
            InitList();
            base.OnAppearing();
        }
    }
}