namespace IT13_FinalProject
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();
                MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"App initialization error: {ex}");
            }
        }
    }
}