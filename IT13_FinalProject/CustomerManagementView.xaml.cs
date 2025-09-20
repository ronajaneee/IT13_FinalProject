using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IT13_FinalProject;

public partial class CustomerManagementView : ContentView
{
    public CustomerProfile SelectedCustomer { get; set; }

    public CustomerManagementView()
    {
        InitializeComponent();
        SelectedCustomer = new CustomerProfile
        {
            Name = "Alice Smith",
            AgeGender = "Age: 32, Female",
            Phone = "555-1234",
            Email = "alice@example.com",
            Address = "123 Main St, Luxe City",
            LoyaltyPoints = 120,
            MembershipStatus = "VIP",
            MembershipColor = "#FF4B91",
            LoyaltyProgress = 0.8,
            VisitTrend = "3 visits this month",
            AdminNotes = "Prefers lavender scent. Allergic to peanuts.",
            BookingHistory = new()
            {
                new Booking { Date = "2024-06-01", Service = "Massage", Staff = "Anna", Status = "Completed" },
                new Booking { Date = "2024-06-10", Service = "Facial", Staff = "John", Status = "Upcoming" }
            },
            RedeemedRewards = new() { "Free Massage (2024-05-01)", "Facial Discount (2024-04-10)" },
            PaymentHistory = new()
            {
                new CustomerPayment { Date = "2024-06-01", Method = "Credit Card", Amount = "$120" },
                new CustomerPayment { Date = "2024-05-01", Method = "Cash", Amount = "$80" }
            },
            AvgSpend = "$80",
            LifetimeValue = "$1,200",
            RetentionScore = "Active"
        };
        BindingContext = this;
    }
}

public class CustomerProfile : INotifyPropertyChanged
{
    public string Name { get; set; }
    public string AgeGender { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int LoyaltyPoints { get; set; }
    public string MembershipStatus { get; set; }
    public string MembershipColor { get; set; }
    public double LoyaltyProgress { get; set; }
    public string VisitTrend { get; set; }
    public string AdminNotes { get; set; }
    public ObservableCollection<Booking> BookingHistory { get; set; } = new();
    public ObservableCollection<string> RedeemedRewards { get; set; } = new();
    public ObservableCollection<CustomerPayment> PaymentHistory { get; set; } = new();
    public string AvgSpend { get; set; }
    public string LifetimeValue { get; set; }
    public string RetentionScore { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class Booking
{
    public string Date { get; set; }
    public string Service { get; set; }
    public string Staff { get; set; }
    public string Status { get; set; }
}

public class CustomerPayment
{
    public string Date { get; set; }
    public string Method { get; set; }
    public string Amount { get; set; }
}
