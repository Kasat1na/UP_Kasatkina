using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MasterPol.ViewModels;

namespace MasterPol;

public partial class Sale_history : UserControl
{
    public Sale_history()
    {
        InitializeComponent();
    }
    public Sale_history(int Id)
    {
        InitializeComponent();
        DataContext = new Sale_historyViewModel(Id);
    }
}