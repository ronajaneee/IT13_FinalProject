using System;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class AddStaffPage : ContentPage
{
    public AddStaffPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var staff = new Staff
        {
            FirstName = FirstNameEntry.Text,
            LastName = LastNameEntry.Text,
            Position = RoleEntry.Text,
            Email = EmailEntry.Text,
            Phone = PhoneEntry.Text,
            Status = StatusEntry.Text,
            RecentActivities = new System.Collections.Generic.List<string> { "Added via AddStaffPage" }
        };
        // Add to staff list via MessagingCenter
        MessagingCenter.Send(this, "AddStaff", staff);
        await DisplayAlert("Success", "Staff added.", "OK");
        await Navigation.PopAsync();
    }

    // Add this method to handle the Cancel button click event
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // You can navigate back or clear fields as needed
        await Navigation.PopAsync();
    }
}
// Add partial class and ensure code-behind matches XAML.
