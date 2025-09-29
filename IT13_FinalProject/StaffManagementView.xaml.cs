using Microsoft.Maui.Controls;
using IT13_FinalProject.ViewModels;
namespace IT13_FinalProject;

public partial class StaffManagementView : ContentView
{
    private StaffManagementViewModel _viewModel;

    public StaffManagementView()
    {
        InitializeComponent();
        _viewModel = new StaffManagementViewModel();
        BindingContext = _viewModel;
    }
}
