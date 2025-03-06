using Avalonia.Controls;
using MasterPol.Models;
using ReactiveUI;

namespace MasterPol.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance;
        public static _43pKasatkinaUpContext myConnection = new _43pKasatkinaUpContext();
        public MainWindowViewModel()
        {
            Instance = this;

        }
        UserControl _pageContent = new Show();

        public UserControl PageContent
        {
            get => _pageContent;
            set => this.RaiseAndSetIfChanged(ref _pageContent, value);
        }
    }
}
