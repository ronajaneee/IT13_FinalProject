using System.Collections.ObjectModel;

namespace IT13_FinalProject;

public partial class CustomerManagementView : ContentView
{
    public ObservableCollection<Customer> Customers { get; set; } = new();

    public CustomerManagementView()
    {
        InitializeComponent();
        // Hardcoded example customers
        Customers.Add(new Customer {
            Name = "Alice Smith",
            ContactDetails = "alice@example.com, 555-1234",
            LoyaltyPoints = 120,
            BookingHistory = "Facial (Jan 10), Massage (Feb 5)"
        });
        Customers.Add(new Customer {
            Name = "Bob Johnson",
            ContactDetails = "bob@example.com, 555-5678",
            LoyaltyPoints = 80,
            BookingHistory = "Manicure (Jan 15), Pedicure (Feb 12)"
        });
        Customers.Add(new Customer {
            Name = "Carol Lee",
            ContactDetails = "carol@example.com, 555-8765",
            LoyaltyPoints = 200,
            BookingHistory = "Haircut (Jan 20), Spa (Feb 20)"
        });
        BindingContext = this;
    }

    private void OnAddCustomerClicked(object sender, EventArgs e)
    {
        Customers.Add(new Customer {
            Name = "New Customer",
            ContactDetails = "new@example.com, 555-0000",
            LoyaltyPoints = 0,
            BookingHistory = "None yet"
        });
    }

    private void OnEditCustomerClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Customer customer)
        {
            customer.Name += " (Edited)";
        }
    }

    private void OnDeleteCustomerClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Customer customer)
        {
            Customers.Remove(customer);
        }
    }

    private async void OnViewHistoryClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Customer customer)
        {
            await Application.Current.MainPage.DisplayAlert(
                $"Booking History for {customer.Name}",
                customer.BookingHistory,
                "OK");
        }
    }
}

public class Customer
{
    public string Name { get; set; }
    public string ContactDetails { get; set; }
    public int LoyaltyPoints { get; set; }
    public string BookingHistory { get; set; }
}
