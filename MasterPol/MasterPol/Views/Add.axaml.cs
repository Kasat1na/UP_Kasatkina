using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MasterPol.ViewModels;

namespace MasterPol;

public partial class Add : UserControl
{
    public Add()
    {
        InitializeComponent();
        DataContext = new AddViewModel();
    }
    public Add(int Id)
    {
        InitializeComponent();
        DataContext = new AddViewModel(Id);
    }
}