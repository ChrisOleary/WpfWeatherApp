using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WpfWeatherApp.Model;

namespace WpfWeatherApp.ViewModels.Helpers
{
    class AccuWeatherHelpers
    {
        public const string BASE_URL = "http://dataservice.accuweather.com";
        public const string API_KEY = "fGGTSauprGrTZD2SxGoVvO65OSJKu8v6";
        public const string AUTOCOMPLETE_ENDPOINT = "/locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CONDITIONS_ENDPOINT = "/currentconditions/v1/{0}?apikey={1}";

        public static async Task<List<City>> GetCities(string userSearch)
        {
            var cities = new List<City>();
            var url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, userSearch);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                // convert httpresponse to json
                var json = await response.Content.ReadAsStringAsync();
                // deserialize into list of cities
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }
            return cities;
        }

        public static async Task<CurrentConditions> GetCondition(string cityKey)
        {
            var condition = new List<CurrentConditions>();
            // build url
            var url = BASE_URL + string.Format(CONDITIONS_ENDPOINT,cityKey,API_KEY);

            // do api call
            using (HttpClient client = new HttpClient())
            {
                // send out httprequest
                HttpResponseMessage respone = await client.GetAsync(url);
                // convert to json string
                var json = await respone.Content.ReadAsStringAsync();
                // deseriazle to object
                condition = JsonConvert.DeserializeObject<List<CurrentConditions>>(json);
            }

            return condition.First();
        }
    }
}
