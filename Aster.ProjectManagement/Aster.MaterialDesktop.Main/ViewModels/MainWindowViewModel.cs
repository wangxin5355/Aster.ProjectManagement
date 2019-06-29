using Aster.MaterialDesktop.Main.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace Aster.MaterialDesktop.Main.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";

        public DelegateCommand  SettingsCommand { get; private set; }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            SettingsCommand = new DelegateCommand(DoSettingsCommand);
        }

        private void DoSettingsCommand()
        {
            Settings settings = new Settings();
            settings.Show();

        }
    }
}
