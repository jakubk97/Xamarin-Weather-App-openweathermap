namespace WeatherApp
{
    using System;
    using Newtonsoft.Json;

    public class WeatherDataForecast
    {

        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("list")]
        public List[] List { get; set; }

        [JsonProperty("message")]
        public long Message { get; set; }

        [JsonProperty("cnt")]
        public long Cnt { get; set; }

        [JsonProperty("cod")]
        public long Cod { get; set; }
    }

    public partial class Temperatures
    {
        [JsonProperty("cod")]
        public long Cod { get; set; }

        [JsonProperty("message")]
        public long Message { get; set; }

        [JsonProperty("cnt")]
        public long Cnt { get; set; }

        [JsonProperty("list")]
        public List[] List { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }
    }

    public partial class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("timezone")]
        public long Timezone { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }

    public partial class List
    {
        [JsonProperty("dt", NullValueHandling = NullValueHandling.Ignore)]
        public long? Dt { get; set; }

        [JsonProperty("main", NullValueHandling = NullValueHandling.Ignore)]
        public MainClass Main { get; set; }

        [JsonProperty("weather", NullValueHandling = NullValueHandling.Ignore)]
        public Weather[] Weather { get; set; }

        [JsonProperty("clouds", NullValueHandling = NullValueHandling.Ignore)]
        public Clouds Clouds { get; set; }

        [JsonProperty("wind", NullValueHandling = NullValueHandling.Ignore)]
        public Wind Wind { get; set; }

        [JsonProperty("sys", NullValueHandling = NullValueHandling.Ignore)]
        public Sys Sys { get; set; }

        [JsonProperty("dt_txt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DtTxt { get; set; }
    }

    public partial class MainClass
    {
        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("sea_level")]
        public long SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public long GrndLevel { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }
    }


}
