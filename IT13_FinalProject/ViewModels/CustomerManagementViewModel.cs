using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;

namespace IT13_FinalProject.ViewModels;

public class CustomerManagementViewModel : BindableObject
{
    public ObservableCollection<Customer> Customers { get; private set; }

    public ICommand AddCustomerCommand { get; private set; }
    public ICommand EditCustomerCommand { get; private set; }
    public ICommand DeleteCustomerCommand { get; private set; }
    public ICommand ViewCustomerCommand { get; private set; }

    public CustomerManagementViewModel()
    {
        Customers = new ObservableCollection<Customer>(GetSampleCustomers());
        
        AddCustomerCommand = new Command(() => {
            var page = Application.Current?.MainPage;
            if (page == null) return;
            var modal = new AddCustomerModal(customer =>
            {
                Customers.Add(customer);
                OnPropertyChanged(nameof(Customers));
            });
            page.ShowPopup(modal);
        });

        EditCustomerCommand = new Command<Customer>(customer => {
            if (customer == null) return;
            var page = Application.Current?.MainPage;
            if (page == null) return;
            var modal = new EditCustomerModal(customer, editedCustomer =>
            {
                var index = Customers.IndexOf(customer);
                if (index >= 0)
                {
                    Customers[index] = editedCustomer;
                    OnPropertyChanged(nameof(Customers));
                }
            });
            page.ShowPopup(modal);
        });

        DeleteCustomerCommand = new Command<Customer>(async customer => {
            if (customer == null) return;
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete", 
                $"Are you sure you want to delete {customer.FirstName} {customer.LastName}?",
                "Yes", "No");
            
            if (confirm)
            {
                Customers.Remove(customer);
                OnPropertyChanged(nameof(Customers));
            }
        });

        ViewCustomerCommand = new Command<Customer>(customer => {
            if (customer == null) return;
            var page = Application.Current?.MainPage;
            if (page == null) return;
            var modal = new ViewCustomerModal(customer);
            page.ShowPopup(modal);
        });
    }

    private List<Customer> GetSampleCustomers()
    {
        return new List<Customer>
        {
            new Customer { FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Phone = "555-0101", Address = "123 Main St", LoyaltyPoints = 100 },
            new Customer { FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com", Phone = "555-0102", Address = "456 Oak Ave", LoyaltyPoints = 75 },
            new Customer { FirstName = "Carol", LastName = "Williams", Email = "carol@example.com", Phone = "555-0103", Address = "789 Pine Rd", LoyaltyPoints = 150 }
        };
    }
}
