using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public partial class EditStaffPage : ContentPage
{
    private Staff _staff;
    public EditStaffPage(Staff staff)
    {
        InitializeComponent();
        _staff = staff;
        FirstNameEntry.Text = staff.FirstName;
        LastNameEntry.Text = staff.LastName;
        EmailEntry.Text = staff.Email;
        PhoneEntry.Text = staff.Phone;
        RoleEntry.Text = staff.Position;
        StatusEntry.Text = staff.Status;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _staff.FirstName = FirstNameEntry.Text;
        _staff.LastName = LastNameEntry.Text;
        _staff.Email = EmailEntry.Text;
        _staff.Phone = PhoneEntry.Text;
        _staff.Position = RoleEntry.Text;
        _staff.Status = StatusEntry.Text;
        await DisplayAlert("Success", "Staff updated.", "OK");
        await Navigation.PopAsync();
    }
}
