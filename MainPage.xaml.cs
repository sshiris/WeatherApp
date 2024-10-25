using System.Net.Http.Json;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
	//create HttpClient instance for making API requests
	private static readonly HttpClient httpClient = new HttpClient();

	//constructor for MainPage, initialize the components defined in the XAML file
	public MainPage()
	{
		InitializeComponent();
	}


	// fetch weather data from the API
	private async Task<WeatherResponse> GetWeatherData(string city, string apiKey)
	{
		string url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?key={apiKey}";

		return await httpClient.GetFromJsonAsync<WeatherResponse>(url);
	}
	//Event Handler when 'get weather' button is clicked
	private async void OnGetWeatherClicked(object sender, EventArgs e)
	{
		//get the city name
		string city = CityEntry.Text ?? "London,UK";
		//my api key
		string apiKey = "MWHYDE3VUPWHXPERKXR2NE99L";

		try
		{
			var weatherData = await GetWeatherData(city, apiKey);
			if (weatherData != null)
			{
				double minTemp = weatherData.days[0].tempMin;
				double maxTemp = weatherData.days[0].temMax;

				bool willRain = weatherData.days[0].precip > 0;
				double windSpeed = weatherData.days[0].windspeed;

				WeatherLabel.Text = $"Min temp: {minTemp}\nMax Temp: {maxTemp}\nRain Forecast: {(willRain? "Yes":"No")}";
				WindAlertLabel.Text = windSpeed > 4 ? "Alert: High wind speed!":"Wind speed is nornal.";
			}
		}
		catch (Exception ex)
		{

			throw;
		}
	}
}

//structure of weather response from the API
public class WeatherResponse
{
	public List<WeatherDay> days { get; set; }
}

public class WeatherDay
{
	public double tempMin { get; set; }
	public double temMax { get; set; }
	public double precip { get; set; }
	public double windspeed { get; set; }
}