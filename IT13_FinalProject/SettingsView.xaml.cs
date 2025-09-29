using System.Collections.ObjectModel;
using System.Linq;

namespace IT13_FinalProject;

public partial class SettingsView : ContentView
{
    public string AdminName { get; set; } = "Sarah Johnson";
    public string AdminEmail { get; set; } = "admin@luxespa.com";
    public string AdminContact { get; set; } = "555-1234";
    public string AdminPassword { get; set; } = "password123";

    public ObservableCollection<Account> Accounts { get; set; } = new();
    public bool IsAdmin => UserSession.Role == "Admin";
    public bool IsStaff => UserSession.Role == "Staff";

    public SettingsView()
    {
        InitializeComponent();
        // Hardcoded accounts
        Accounts.Add(new Account { Name = "Sarah Johnson", Email = "admin@luxespa.com", Role = "Admin" });
        Accounts.Add(new Account { Name = "Jane Doe", Email = "jane@luxespa.com", Role = "Staff" });
        Accounts.Add(new Account { Name = "John Smith", Email = "john@luxespa.com", Role = "Staff" });
        BindingContext = this;
        ApplyRoleBasedVisibility();
    }

    private void ApplyRoleBasedVisibility()
    {
        if (IsStaff)
        {
            // Only show the logged-in staff's own account
            var current = Accounts.FirstOrDefault(a => a.Email == UserSession.Username || a.Name == UserSession.Username);
            Accounts.Clear();
            if (current != null)
                Accounts.Add(current);
            // Set profile fields to current staff info
            if (current != null)
            {
                AdminName = current.Name;
                AdminEmail = current.Email;
                AdminContact = ""; // Set if you have contact info
                AdminPassword = ""; // Set if you have password info
            }
        }
        else if (IsAdmin)
        {
            // Admin sees all accounts
        }
    }

    private async void OnUpdateProfileClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Profile Updated", $"Name: {AdminName}\nEmail: {AdminEmail}\nContact: {AdminContact}", "OK");
    }

    private async void OnAddAccountClicked(object sender, EventArgs e)
    {
        if (!IsAdmin)
        {
            await Application.Current.MainPage.DisplayAlert("Access Denied", "Only Admin can add accounts.", "OK");
            return;
        }
        string name = await Application.Current.MainPage.DisplayPromptAsync("Add Account", "Enter name:");
        string email = await Application.Current.MainPage.DisplayPromptAsync("Add Account", "Enter email:");
        string role = await Application.Current.MainPage.DisplayActionSheet("Select Role", "Cancel", null, "Admin", "Staff");
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && role != "Cancel")
        {
            Accounts.Add(new Account { Name = name, Email = email, Role = role });
        }
    }

    private async void OnSetRoleClicked(object sender, EventArgs e)
    {
        if (!IsAdmin)
        {
            await Application.Current.MainPage.DisplayAlert("Access Denied", "Only Admin can set roles.", "OK");
            return;
        }
        if (sender is Button btn && btn.BindingContext is Account acc)
        {
            string role = await Application.Current.MainPage.DisplayActionSheet($"Set Role for {acc.Name}", "Cancel", null, "Admin", "Staff");
            if (role != "Cancel")
            {
                acc.Role = role;
            }
        }
    }

    private void OnRemoveAccountClicked(object sender, EventArgs e)
    {
        if (!IsAdmin)
        {
            Application.Current.MainPage.DisplayAlert("Access Denied", "Only Admin can remove accounts.", "OK");
            return;
        }
        if (sender is Button btn && btn.BindingContext is Account acc)
        {
            Accounts.Remove(acc);
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        UserSession.Clear();
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}

public class Account
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
