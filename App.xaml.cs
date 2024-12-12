using WeatherApp.Data;

namespace WeatherApp;

public partial class App : Application
{
	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();

		var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
		MainPage = new NavigationPage(new LandingPage(dbContext));
	}
}