using Microsoft.Maui.Controls;
using IT13_FinalProject.ViewModels;

namespace IT13_FinalProject;

public partial class ServiceManagementView : ContentView
{
    private ServiceManagementViewModel _viewModel;

    public ServiceManagementView()
    {
        InitializeComponent();
        _viewModel = new ServiceManagementViewModel();
        BindingContext = _viewModel;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.FilterServices(e.NewTextValue);
    }
}
