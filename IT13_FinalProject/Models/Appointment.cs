using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IT13_FinalProject;

public class Appointment : INotifyPropertyChanged
{
    private string _customerName = string.Empty;
    private DateTime _appointmentDateTime = System.DateTime.Now;
    private string _status = string.Empty;
    private string _service = string.Empty;

    public string CustomerName 
    { 
        get => _customerName;
        set 
        {
            if (_customerName != value)
            {
                _customerName = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime AppointmentDateTime 
    { 
        get => _appointmentDateTime;
        set 
        {
            if (_appointmentDateTime != value)
            {
                _appointmentDateTime = value;
                OnPropertyChanged();
            }
        }
    }

    public string Status 
    { 
        get => _status;
        set 
        {
            if (_status != value)
            {
                _status = value;
                OnPropertyChanged();
            }
        }
    }

    public string Service 
    { 
        get => ServiceName;
        set 
        {
            if (ServiceName != value)
            {
                ServiceName = value;
                OnPropertyChanged();
            }
        }
    }

    public string AppointmentId { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string AssignedStaff { get; set; } = string.Empty;

    // Rename string property to AppointmentDateTimeString to avoid conflict
    public string AppointmentDateTimeString
    {
        get => AppointmentDateTime.ToString("yyyy-MM-dd HH:mm");
        set
        {
            if (System.DateTime.TryParse(value, out var dt))
            {
                AppointmentDateTime = dt;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}