using System.Text;

namespace PixelBrewApi;

public static class EquipmentQuery
{
    public static string BuildQuery(Equipment equip)
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("UPDATE Coffee SET");
        if (equip.Manufacturer != null && equip.Manufacturer.Length > 0)
        {
            sql.Append(" Manufacturer = @Manufacturer");
        }

        if (equip.Model != null && equip.Model.Length > 0)
        {
            sql.Append(", Model = @Model");
        }

        if (equip.Type != null && equip.Type.Length > 0)
        {
            sql.Append(", Type = @Type,");
        }

        if (equip.Setting != null && equip.Setting.Length > 0)
        {
            sql.Append(", Setting = @Setting");
        }


        sql.Append(" WHERE GrinderId = @GrinderId");

        return sql.ToString();
    }
}
