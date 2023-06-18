using System.Collections.Generic;

namespace PixelBrewApi;
public class Response
{
    public string Status { get; set; } = "";
    public string Message { get; set; } = "Unknown";
    public List<Coffee>? Coffees { get; set; } = null;
}