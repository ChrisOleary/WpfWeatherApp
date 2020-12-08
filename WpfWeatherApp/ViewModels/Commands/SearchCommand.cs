using System;
using System.Windows.Input;

namespace WpfWeatherApp.ViewModels.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM VM { get; set; }
        public SearchCommand searchCommand { get; set; }

        public event EventHandler CanExecuteChanged 
        {
            // these 2 lines both needed for from end binding of UpdateSourceTrigger=PropertyChanged to work
            add { CommandManager.RequerySuggested += value; } 
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherVM vM)
        {
            VM = vM;
        }

        public bool CanExecute(object parameter)
        {
            var query = parameter as string;
            if (string.IsNullOrEmpty(query))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
