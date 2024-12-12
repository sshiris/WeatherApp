using WeatherApp.Data;

namespace WeatherApp;

public partial class LandingPage : ContentPage
{
    private readonly AppDbContext _dbContext;
    public LandingPage(AppDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
        LoadDashboardData();
    }
    private async void OnViewTodoClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Navigating to TodoPage...");
        var todoPage = new TodoPage(_dbContext);
        await Navigation.PushAsync(todoPage);
    }

    private async void LoadDashboardData()
    {
        try
        {
            string city = "Vaasa, Finland";
            string apiKey = "MWHYDE3VUPWHXPERKXR2NE99L";

            MainPage mainPage = new MainPage();
            var weatherData = await mainPage.GetWeatherData(city, apiKey);

            if (weatherData != null && weatherData.Days != null && weatherData.Days.Count > 0)
            {
                var today = weatherData.Days[0];
                WeatherSummaryLabel.Text = $"Today's weather:{today.TempMin}C - {today.TempMax}C";

            }
            else
            {
                WeatherSummaryLabel.Text = $"unable to load weather data.";
            }

            int todoCount = TodoStorage.GetAllTodos().Count;
            TodoSummaryLabel.Text = $"Today's Todos:{todoCount}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            WeatherSummaryLabel.Text = "Error loading dashboard data.";
            TodoSummaryLabel.Text = "";
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadDashboardData();
    }

    private async void OnViewWeatherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

}