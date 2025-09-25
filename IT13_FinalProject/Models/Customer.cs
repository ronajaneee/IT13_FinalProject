using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IT13_FinalProject;

public class Customer : INotifyPropertyChanged
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;
    private string _phone = string.Empty;
    private string _address = string.Empty;
    private int _loyaltyPoints;
    private ObservableCollection<string> _bookingHistory = new();

    public string FirstName 
    { 
        get => _firstName;
        set 
        {
            if (_firstName != value)
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
    }

    public string LastName 
    { 
        get => _lastName;
        set 
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
    }

    public string Email 
    { 
        get => _email;
        set 
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }

    public string Phone 
    { 
        get => _phone;
        set 
        {
            if (_phone != value)
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
    }

    public string Address 
    { 
        get => _address;
        set 
        {
            if (_address != value)
            {
                _address = value;
                OnPropertyChanged();
            }
        }
    }

    public int LoyaltyPoints 
    { 
        get => _loyaltyPoints;
        set 
        {
            if (_loyaltyPoints != value)
            {
                _loyaltyPoints = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<string> BookingHistory { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}