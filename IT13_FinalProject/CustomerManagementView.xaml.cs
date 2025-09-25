using IT13_FinalProject.ViewModels;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace IT13_FinalProject;

public partial class CustomerManagementView : ContentView
{
    private CustomerManagementViewModel _viewModel;

    public CustomerManagementView()
    {
        try
        {
            InitializeComponent();
            _viewModel = new CustomerManagementViewModel();
            BindingContext = _viewModel;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in CustomerManagementView constructor: {ex}");
            Application.Current?.MainPage?.DisplayAlert("Error", "Failed to initialize Customer Management", "OK");
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (_viewModel == null) return;

            var searchText = e.NewTextValue?.ToLower() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                _viewModel.Customers.Clear();
                foreach (var c in new IT13_FinalProject.ViewModels.CustomerManagementViewModel().Customers)
                {
                    _viewModel.Customers.Add(c);
                }
            }
            else
            {
                var filtered = _viewModel.Customers.Where(c =>
                    c.FirstName?.ToLower().Contains(searchText) == true ||
                    c.LastName?.ToLower().Contains(searchText) == true ||
                    c.Email?.ToLower().Contains(searchText) == true).ToList();
                _viewModel.Customers.Clear();
                foreach (var c in filtered)
                {
                    _viewModel.Customers.Add(c);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in search: {ex}");
        }
    }
}
