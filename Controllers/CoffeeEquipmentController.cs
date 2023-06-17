using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace PixelBrewApi;
[ApiController]
[Route("[controller]")]
public class CoffeeEquipmentController
{
    [HttpGet]
    [Route("/GetEquipment")]
    public string GetEquipment(string equipment)
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();
        var sql = $"select * from Coffee;";
        string v = "";
        Coffee cofe = new Coffee();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                //cofe.CoffeeId = Convert.ToInt32(sqlDataReader["CoffeeId"]);
                v += sqlDataReader["Varietal"].ToString();
            }
        }

        return v;
    }
}