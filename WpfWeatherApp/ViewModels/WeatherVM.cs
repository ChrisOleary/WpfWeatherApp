using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfWeatherApp.Model;
using WpfWeatherApp.ViewModels.Commands;
using WpfWeatherApp.ViewModels.Helpers;

namespace WpfWeatherApp.ViewModels
{
    // : INotifyPropertyChanged deals with the event handling
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;
        public string Query
        {
            get { return query; }
            set 
            { 
                query = value;
                // call the below method on setting Query
                OnPropertyChanged("Query");
            }
        }

        private CurrentConditions currentConditions;
        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set 
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;
        public City SelectedCity 
        {
            get { return selectedCity; }
            set 
            { 
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions(); // fire this method everytime a city is selected from the Listview
            }
        }

        public SearchCommand SearchCommand { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        public WeatherVM()
        {
            // if the project is not running, eg in design mode, run this
            // excellent for testing fixed values
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "Plymouth"
                };

                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "10"
                        }
                    }
                };
            }

            // passess an instance of the current class into the constructor
            // used for binding
            // this variable is bound to the search button. pressing it triggers an instance of this class (object) 
            // to be passed into SearchCommand(this) which then has access to the entire objectt, 
            // which then can be passed to the execute method, which runs MakeQuery() which is back in this class
            // xaml Button > VM Constuctor > Command Constructor > Command Execute Method > VM MakeQuey Method > AccuWeatherHelpers.GetCities using the Query property thats holding the city name
            SearchCommand = new SearchCommand(this); //// initialize the Commands

            Cities = new ObservableCollection<City>(); // must initialise the observablCollection in the constructor
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelpers.GetCities(Query);
            
            Cities.Clear(); // make sure the ObservableCollection has no other things in from before
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
            
        }

        public async void GetCurrentConditions()
        {
            Query = string.Empty; // clears search bar for user
            Cities.Clear(); // clears List for user
            CurrentConditions = await AccuWeatherHelpers.GetCondition(SelectedCity.Key);
        }

        // inherited from INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // very common method for event changes
        private void OnPropertyChanged(string propertyName)
        {
            // this triggers the event property above
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
