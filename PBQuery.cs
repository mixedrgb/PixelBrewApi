using System.Text;

namespace PixelBrewApi;

public static class PBQuery
{
    public static string BuildGrinderQuery(Equipment equip)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE Equipment SET");
        if (equip.Manufacturer != null && equip.Manufacturer.Length > 0)
        {
            sql.Append(" GrinderManufacturer = @GrinderManufacturer");
        }

        if (equip.Type != null && equip.Type.Length > 0)
        {
            sql.Append(", GrinderType = @GrinderType");
        }

        if (equip.Model != null && equip.Model.Length > 0)
        {
            sql.Append(", GrinderModel = @GrinderModel");
        }

        if (equip.Setting != null && equip.Setting.Length > 0)
        {
            sql.Append(", GrinderSetting = @GrinderSetting");
        }

        sql.Append(" WHERE GrinderId = @GrinderId");

        return sql.ToString();
    }

    public static string BuildCoffeeQuery(Coffee cofe)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE Coffee SET");
        if (cofe.CoffeeName != null && cofe.CoffeeName.Length > 0)
        {
            sql.Append(" CoffeeName = @CoffeeName");
        }

        if (cofe.Region != null && cofe.Region.Length > 0)
        {
            sql.Append(", Region = @Region");
        }

        if (cofe.Processing != null && cofe.Processing.Length > 0)
        {
            sql.Append(", Processing = @Processing");
        }

        if (cofe.Varietal != null && cofe.Varietal.Length > 0)
        {
            sql.Append(", Varietal = @Varietal");
        }

        if (cofe.RoastType != null && cofe.RoastType.Length > 0)
        {
            sql.Append(", RoastType = @RoastType");
        }

        if (cofe.Weight != null && cofe.Weight.Length > 0)
        {
            sql.Append(", Weight = @Weight");
        }

        if (cofe.RoastDate != null && cofe.RoastDate.Length > 0)
        {
            sql.Append(", RoastDate = @RoastDate");
        }

        sql.Append(" WHERE CoffeeId = @CoffeeId");

        return sql.ToString();
    }
}
