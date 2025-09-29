using IT13_FinalProject.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;
using System.Linq;

namespace IT13_FinalProject.ViewModels
{
    public class ServiceManagementViewModel : BindableObject
    {
        public ObservableCollection<Service> Services { get; private set; }
        public ObservableCollection<Service> FilteredServices { get; private set; }

        public ICommand AddServiceCommand { get; private set; }
        public ICommand EditServiceCommand { get; private set; }
        public ICommand DeleteServiceCommand { get; private set; }
        public ICommand ViewServiceCommand { get; private set; }

        public ServiceManagementViewModel()
        {
            Services = new ObservableCollection<Service>
            {
                new Service { Name = "Massage", Price = 50.00m, Duration = "60 mins", Description = "Relaxing full-body massage" },
                new Service { Name = "Facial", Price = 35.00m, Duration = "45 mins", Description = "Rejuvenating facial treatment" },
                new Service { Name = "Hair Treatment", Price = 40.00m, Duration = "50 mins", Description = "Deep conditioning hair treatment" }
            };
            FilteredServices = new ObservableCollection<Service>(Services);

            AddServiceCommand = new Command(() =>
            {
                if (UserSession.Role == "Staff")
                {
                    Application.Current.MainPage.DisplayAlert("Access Denied", "Staff cannot add services.", "OK");
                    return;
                }
                var page = Application.Current?.MainPage;
                if (page == null) return;
                var modal = new AddServiceModal(Services, FilteredServices);
                page.ShowPopup(modal);
            });

            EditServiceCommand = new Command<Service>(service =>
            {
                if (UserSession.Role == "Staff")
                {
                    Application.Current.MainPage.DisplayAlert("Access Denied", "Staff cannot edit services.", "OK");
                    return;
                }
                if (service == null) return;
                var page = Application.Current?.MainPage;
                if (page == null) return;
                var modal = new EditServiceModal(service);
                page.ShowPopup(modal);
            });

            DeleteServiceCommand = new Command<Service>(async service =>
            {
                if (UserSession.Role == "Staff")
                {
                    await Application.Current.MainPage.DisplayAlert("Access Denied", "Staff cannot delete services.", "OK");
                    return;
                }
                if (service == null) return;
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Confirm Delete",
                    $"Are you sure you want to delete {service.Name}?",
                    "Yes", "No");
                if (confirm)
                {
                    Services.Remove(service);
                    FilteredServices.Remove(service);
                    OnPropertyChanged(nameof(FilteredServices));
                }
            });

            ViewServiceCommand = new Command<Service>(service =>
            {
                if (service == null) return;
                var page = Application.Current?.MainPage;
                if (page == null) return;
                var modal = new ViewServiceModal(service);
                page.ShowPopup(modal);
            });
        }

        public void FilterServices(string searchText)
        {
            FilteredServices.Clear();
            var filtered = string.IsNullOrWhiteSpace(searchText) 
                ? Services 
                : Services.Where(s => s.Name.ToLower().Contains(searchText.ToLower()));
            foreach (var service in filtered)
                FilteredServices.Add(service);
            OnPropertyChanged(nameof(FilteredServices));
        }
    }
}