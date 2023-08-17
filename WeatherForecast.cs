<<<<<<< HEAD
namespace KalumManagement;
=======
namespace KalumAuthManagement;
>>>>>>> 8c782d89149ff6cca6be42a8f6889018b2794a80

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
