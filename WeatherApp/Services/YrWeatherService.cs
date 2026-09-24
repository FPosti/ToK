using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace WeatherApp.Services;

// Den här klassen sparar inställningarna för platsen som vädret ska visas för.
public class YrWeatherOptions
{
    // Namnet som visas på sidan.
    public string PlaceName { get; set; } = "Stockholm";

    // Yr använder latitud och longitud för att veta vilken plats prognosen gäller.
    public double Latitude { get; set; } = 59.3293;
    public double Longitude { get; set; } = 18.0686;

    // Yr vill att appnamn och kontakt skickas med i User-Agent.
    public string UserAgent { get; set; } = "ToKWeatherApp/1.0 (contact: local-dev@localhost)";
}

// Den här klassen hämtar väderdata från Yr.
public class YrWeatherService
{
    private readonly HttpClient httpClient;
    private readonly YrWeatherOptions options;

    // Servicen får HttpClient och inställningar från Program.cs.
    public YrWeatherService(HttpClient httpClient, IOptions<YrWeatherOptions> options)
    {
        this.httpClient = httpClient;
        this.options = options.Value;
    }

    // Den här metoden hämtar prognosen och gör om den till ett enklare format.
    public async Task<WeatherReport> GetForecastAsync()
    {
        // Bygger API-adressen med koordinaterna från appsettings.json.
        string url = "https://api.met.no/weatherapi/locationforecast/2.0/compact?lat="
            + options.Latitude.ToString(CultureInfo.InvariantCulture)
            + "&lon="
            + options.Longitude.ToString(CultureInfo.InvariantCulture);

        // Skapar requesten som skickas till Yr.
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        // Yr kräver User-Agent så att de vet vilken app som använder API:t.
        request.Headers.TryAddWithoutValidation("User-Agent", options.UserAgent);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Skickar anropet till Yr.
        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        // Yr skickar tillbaka JSON, så svaret läses som text och öppnas som JSON.
        string json = await response.Content.ReadAsStringAsync();
        JsonDocument document = JsonDocument.Parse(json);

        // Går ner i JSON-strukturen där prognosen finns.
        JsonElement properties = document.RootElement.GetProperty("properties");
        JsonElement timeseries = properties.GetProperty("timeseries");
        DateTimeOffset updatedAt = properties.GetProperty("meta").GetProperty("updated_at").GetDateTimeOffset();

        // Den här listan innehåller dagarna som visas i tabellen.
        List<DayForecast> days = new List<DayForecast>();
        DayForecast? current = null;
        DateOnly lastDate = DateOnly.MinValue;

        // Yr skickar många tidpunkter, så här går vi igenom dem en och en.
        foreach (JsonElement item in timeseries.EnumerateArray())
        {
            // Gör om tiden från Yr till lokal tid.
            DateTimeOffset time = item.GetProperty("time").GetDateTimeOffset().ToLocalTime();

            // Hoppar över gamla tider.
            if (time < DateTimeOffset.Now.AddHours(-1))
            {
                continue;
            }

            // Läser temperatur och vädertext från en prognospunkt.
            DayForecast forecast = ReadForecast(item, time);

            // Den första framtida prognosen används som aktuell prognos.
            if (current == null)
            {
                current = forecast;
            }

            // Sparar bara en prognos per dag så listan inte blir för lång.
            if (forecast.Date != lastDate)
            {
                days.Add(forecast);
                lastDate = forecast.Date;
            }

            // Appen visar bara fem dagar.
            if (days.Count == 5)
            {
                break;
            }
        }

        // Om Yr inte gav någon användbar prognos kastas ett fel.
        if (current == null)
        {
            throw new Exception("No forecast found.");
        }

        // Skickar den färdiga rapporten tillbaka till Weather.razor.
        return new WeatherReport(options.PlaceName, updatedAt, current, days);
    }

    // Den här metoden läser en prognos från JSON.
    private static DayForecast ReadForecast(JsonElement item, DateTimeOffset time)
    {
        JsonElement data = item.GetProperty("data");
        JsonElement details = data.GetProperty("instant").GetProperty("details");

        // Temperaturen finns under instant/details i Yr:s JSON.
        double temperature = details.GetProperty("air_temperature").GetDouble();

        // Om ingen vädertext hittas används unknown som standard.
        string summary = "unknown";
        JsonElement nextHours;

        // next_6_hours innehåller en enkel vädersymbol, till exempel cloudy.
        if (data.TryGetProperty("next_6_hours", out nextHours))
        {
            summary = nextHours.GetProperty("summary").GetProperty("symbol_code").GetString() ?? "unknown";
        }

        // Gör Yr:s symbol lite lättare att läsa i tabellen.
        summary = summary.Replace("_day", "").Replace("_night", "").Replace("_", " ");

        // Skapar objektet som sidan sedan visar i tabellen.
        return new DayForecast(DateOnly.FromDateTime(time.DateTime), temperature, summary);
    }
}

// Den här klassen är en enkel väderrapport för sidan.
public class WeatherReport
{
    public WeatherReport(string placeName, DateTimeOffset updatedAt, DayForecast current, List<DayForecast> days)
    {
        PlaceName = placeName;
        UpdatedAt = updatedAt;
        Current = current;
        Days = days;
    }

    public string PlaceName { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DayForecast Current { get; set; }
    public List<DayForecast> Days { get; set; }
}

// Den här klassen representerar en rad i prognostabellen.
public class DayForecast
{
    public DayForecast(DateOnly date, double temperatureC, string summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }

    public DateOnly Date { get; set; }
    public double TemperatureC { get; set; }
    public string Summary { get; set; }
}
