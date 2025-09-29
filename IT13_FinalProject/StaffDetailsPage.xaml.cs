using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace IT13_FinalProject;

public partial class StaffDetailsPage : ContentPage
{
    public StaffDetailsPage(Staff staff)
    {
        InitializeComponent();
        Title = "Staff Details";
        if (staff == null)
        {
            NameLabel.Text = "No staff selected.";
            RoleLabel.Text = "";
            EmailLabel.Text = "";
            PhoneLabel.Text = "";
            StatusLabel.Text = "";
            ActivitiesStack.Children.Clear();
            ActivitiesStack.Children.Add(new Label { Text = "No activities available.", FontSize = 14, TextColor = Colors.WhiteSmoke });
            return;
        }
        NameLabel.Text = $"{staff.FirstName} {staff.LastName}";
        RoleLabel.Text = $"Role/Position: {staff.Position}";
        EmailLabel.Text = $"Email: {staff.Email}";
        PhoneLabel.Text = $"Phone: {staff.Phone}";
        StatusLabel.Text = $"Status: {staff.Status}";
        ActivitiesStack.Children.Clear();
        if (staff.RecentActivities != null && staff.RecentActivities.Count > 0)
        {
            foreach (var activity in staff.RecentActivities)
            {
                var activityLabel = new Label 
                { 
                    Text = "• " + activity,
                    FontSize = 14,
                    TextColor = Colors.WhiteSmoke,
                    Margin = new Thickness(8, 0, 0, 0)
                };
                ActivitiesStack.Children.Add(activityLabel);
            }
        }
        else
        {
            ActivitiesStack.Children.Add(new Label { Text = "No activities available.", FontSize = 14, TextColor = Colors.WhiteSmoke });
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
