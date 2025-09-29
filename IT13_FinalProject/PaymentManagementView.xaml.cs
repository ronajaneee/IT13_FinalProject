using Microsoft.Maui.Controls;
using IT13_FinalProject.ViewModels;

namespace IT13_FinalProject;

public partial class PaymentManagementView : ContentView
{
    private PaymentManagementViewModel _viewModel;

    public PaymentManagementView()
    {
        InitializeComponent();
        _viewModel = (PaymentManagementViewModel)BindingContext;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel?.FilterPayments(e.NewTextValue);
    }
}
