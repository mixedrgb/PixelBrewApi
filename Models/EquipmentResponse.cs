using System.Collections.Generic;

namespace PixelBrewApi;
public class EquipmentResponse
{
    public string Status { get; set; } = "";
    public string Message { get; set; } = "Unknown";
    public List<Equipment>? Equipments { get; set; } = null;
}
