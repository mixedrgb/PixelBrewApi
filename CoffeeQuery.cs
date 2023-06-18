using System.Text;

namespace PixelBrewApi;

public static class CoffeeQuery
{
    public static string BuildQuery(Coffee cofe)
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
            sql.Append(", Processing = @Processing,");
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
