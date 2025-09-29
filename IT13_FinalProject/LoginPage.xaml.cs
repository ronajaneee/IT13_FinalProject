using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace IT13_FinalProject;

public static class UserSession
{
    public static string Username { get; set; }
    public static string Role { get; set; }
    public static void Clear()
    {
        Username = null;
        Role = null;
    }
}

public class AppUser
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

public static class UserStore
{
    // In-memory user store for demo
    public static List<AppUser> Users = new List<AppUser>
    {
        new AppUser { Username = "admin", Password = "1234", Role = "Admin" },
        new AppUser { Username = "staff", Password = "1234", Role = "Staff" }
        // Add more admin accounts as needed
    };

    public static AppUser ValidateUser(string username, string password)
    {
        return Users.Find(u => u.Username == username && u.Password == password);
    }
}

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var user = UserStore.ValidateUser(usernameEntry.Text, passwordEntry.Text);
        if (user != null)
        {
            UserSession.Username = user.Username;
            UserSession.Role = user.Role;
            await DisplayAlert("Success", $"Welcome, {user.Username}!", "OK");
            if (user.Role == "Admin")
                Application.Current.MainPage = new NavigationPage(new AdminPage());
            else if (user.Role == "Staff")
                Application.Current.MainPage = new NavigationPage(new StaffPage());
        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password.", "OK");
        }
    }
}