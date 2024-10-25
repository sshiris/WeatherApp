using System.Net.Http.Json;
using System.Text.Json.Serialization;

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
	private async Task<WeatherResponse?> GetWeatherData(string city, string apiKey)
	{
		string url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?key={apiKey}";

		try
		{
			var response = await httpClient.GetStringAsync(url);
			Console.WriteLine(response); // Print raw JSON to check field names

			WeatherResponse? weatherResponse = await httpClient.GetFromJsonAsync<WeatherResponse>(url);

			// Return the weatherResponse, which can be null
			return weatherResponse;
		}
		catch (HttpRequestException e)
		{
			Console.WriteLine($"Request error: {e.Message}");
			return null; // Return null in case of an error
		}
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
			if (weatherData != null && weatherData.Days != null && weatherData.Days.Count > 0)
			{
				var today = weatherData.Days[0];

				double minTemp = today.TempMin;
				double maxTemp = today.TempMax;
				bool willRain = today.Precip > 0;
				double windSpeed = today.Windspeed;

				WeatherLabel.Text = $"Min Temp: {minTemp}°C\nMax Temp: {maxTemp}°C\nRain Forecast: {(willRain ? "Yes" : "No")}";
				WindAlertLabel.Text = windSpeed > 4 ? "Alert: High wind speed!" : "Wind speed is nornal.";
			}
		}
		catch (Exception ex)
		{
			WeatherLabel.Text = "Error retrieving weather data.";
			Console.WriteLine($"Exception:{ex.Message}");
		}
	}


}
//structure of weather response from the API
public class WeatherResponse
{
	[JsonPropertyName("days")]
	public List<WeatherDay>? Days { get; set; }
}

public class WeatherDay
{
	[JsonPropertyName("tempmin")]
	public double TempMin { get; set; }
	[JsonPropertyName("tempmax")]
	public double TempMax { get; set; }
	[JsonPropertyName("precip")]
	public double Precip { get; set; }
	[JsonPropertyName("windspeed")]
	public double Windspeed { get; set; }
}