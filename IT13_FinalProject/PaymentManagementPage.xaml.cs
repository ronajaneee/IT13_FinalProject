using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class PaymentManagementPage : ContentPage
{
    public PaymentManagementPage()
    {
        InitializeComponent();
    }

    // Example: Restrict edit/delete for Staff
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (UserSession.Role == "Staff")
        {
            // Optionally hide or disable edit/delete UI elements here
        }
    }
}
