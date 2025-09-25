using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;
using IT13_FinalProject;

namespace IT13_FinalProject.ViewModels;

public class CustomerManagementViewModel : BindableObject
{
    private Page? _parentPage;
    public ObservableCollection<Customer> Customers { get; private set; }

    public ICommand AddCustomerCommand { get; private set; }
    public ICommand EditCustomerCommand { get; private set; }
    public ICommand DeleteCustomerCommand { get; private set; }
    public ICommand ViewCustomerCommand { get; private set; }

    public CustomerManagementViewModel()
    {
        Customers = new ObservableCollection<Customer>(GetSampleCustomers());
        AddCustomerCommand = new Command(OnAddCustomerPopup);
        EditCustomerCommand = new Command<Customer>(OnEditCustomerPopup);
        DeleteCustomerCommand = new Command<Customer>(OnDeleteCustomerPopup);
        ViewCustomerCommand = new Command<Customer>(OnViewCustomerPopup);
    }

    private List<Customer> GetSampleCustomers()
    {
        return new List<Customer>
        {
            new Customer { FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Phone = "555-1234", Address = "123 Main St", LoyaltyPoints = 120 },
            new Customer { FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com", Phone = "555-5678", Address = "456 Oak St", LoyaltyPoints = 85 },
            new Customer { FirstName = "Carol", LastName = "Lee", Email = "carol@example.com", Phone = "555-8765", Address = "789 Pine Rd", LoyaltyPoints = 200 },
            new Customer { FirstName = "David", LastName = "Kim", Email = "david@example.com", Phone = "555-4321", Address = "321 Maple Ave", LoyaltyPoints = 50 },
            new Customer { FirstName = "Eva", LastName = "Martinez", Email = "eva@example.com", Phone = "555-2468", Address = "654 Cedar Blvd", LoyaltyPoints = 0 }
        };
    }

    private void OnAddCustomerPopup()
    {
        var page = Application.Current?.MainPage;
        if (page == null) return;
        AddEditCustomerPopupV2? popup = null;
        popup = new AddEditCustomerPopupV2(null, false, customer =>
        {
            Customers.Add(customer);
            OnPropertyChanged(nameof(Customers));
            popup.Close();
        });
        page.ShowPopup(popup);
    }

    private void OnEditCustomerPopup(Customer? customer)
    {
        if (customer == null) return;
        var page = Application.Current?.MainPage;
        if (page == null) return;
        AddEditCustomerPopupV2? popup = null;
        popup = new AddEditCustomerPopupV2(customer, true, editedCustomer =>
        {
            var index = Customers.IndexOf(customer);
            if (index >= 0)
            {
                Customers[index] = editedCustomer;
                OnPropertyChanged(nameof(Customers));
            }
            popup.Close();
        });
        page.ShowPopup(popup);
    }

    private void OnDeleteCustomerPopup(Customer? customer)
    {
        if (customer == null) return;
        var page = Application.Current?.MainPage;
        if (page == null) return;
        page.DisplayAlert("Confirm Delete", $"Are you sure you want to delete {customer.FirstName} {customer.LastName}?", "Yes", "No")
            .ContinueWith(t =>
            {
                if (t.Result)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Customers.Remove(customer);
                        OnPropertyChanged(nameof(Customers));
                    });
                } 
            });
    }

    private void OnViewCustomerPopup(Customer? customer)
    {
        if (customer == null) return;
        var page = Application.Current?.MainPage;
        if (page == null) return;
        var popup = new ViewCustomerDetailsPopupV2(customer);
        page.ShowPopup(popup);
    }
}
