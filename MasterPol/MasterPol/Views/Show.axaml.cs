using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MasterPol.ViewModels;

namespace MasterPol;

public partial class Show : UserControl
{
    public Show()
    {
        InitializeComponent();
        DataContext = new ShowViewModel();
    }
}