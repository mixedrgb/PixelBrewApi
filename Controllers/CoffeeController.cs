using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace PixelBrewApi;

[ApiController]
[Route("[controller]")]
public class CoffeeController
{

    #region Insert Coffee
    [HttpGet]
    [Route("/InsertCoffee")]
    public int InsertCoffee(string coffeeName,
    string region = "",
    string processing = "",
    string varietal = "",
    string roastType = "",
    string weight = "",
    string roastDate = "")
    {

        //CoffeeId, CoffeeName, Region, Processing, Varietal, RoastType, Weight, RoastDate

        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        var sql = @$"insert into Coffee
(CoffeeName, Region, Processing, Varietal, RoastType, Weight, RoastDate)
values ('{coffeeName}','{region}','{processing}','{varietal}','{roastType}','{weight}','{roastDate}')
;";

        Coffee cofe = new Coffee();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            SqlParameter paramCoffeeName = new SqlParameter("@CoffeeName", coffeeName); // == null ? (object)DBNull.Value : employee.FirstName);
            SqlParameter paramRegion = new SqlParameter("@Region", region); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramProcessing = new SqlParameter("@Processing", processing); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramVarietal = new SqlParameter("@Varietal", varietal); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramRoastType = new SqlParameter("@RoastType", roastType); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramWeight = new SqlParameter("@Weight", weight); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramRoastDate = new SqlParameter("@RoastDate", roastDate); // == null ? (object)DBNull.Value : employee.Salary);

            paramCoffeeName.DbType = DbType.String;
            paramRegion.DbType = DbType.String;
            paramProcessing.DbType = DbType.String;
            paramVarietal.DbType = DbType.String;
            paramRoastType.DbType = DbType.String;
            paramWeight.DbType = DbType.String;
            paramRoastDate.DbType = DbType.String;

            sqlCommand.Parameters.Add(paramCoffeeName);
            sqlCommand.Parameters.Add(paramRegion);
            sqlCommand.Parameters.Add(paramProcessing);
            sqlCommand.Parameters.Add(paramVarietal);
            sqlCommand.Parameters.Add(paramRoastType);
            sqlCommand.Parameters.Add(paramWeight);
            sqlCommand.Parameters.Add(paramRoastDate);

            return sqlCommand.ExecuteNonQuery();
        }
    }
    #endregion

    #region Get Coffee
    [HttpGet]
    [Route("/GetCoffee")]
    public Coffee GetCoffee(string fixMe = "aaaaaaaaaaaaaaa")
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();
        var sql = $"select * from Coffee;";
        Coffee cofe = new Coffee();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                cofe.CoffeeName = sqlDataReader["CoffeeName"].ToString() ?? "";
                cofe.CoffeeId = Convert.ToInt32(sqlDataReader["CoffeeId"]);
                cofe.Region = sqlDataReader["Region"].ToString();
                cofe.Processing = sqlDataReader["Processing"].ToString();
                cofe.Varietal = sqlDataReader["Varietal"].ToString();
                cofe.RoastType = sqlDataReader["RoastType"].ToString();
                cofe.Weight = sqlDataReader["Weight"].ToString();
                cofe.RoastDate = sqlDataReader["RoastDate"].ToString();
            }
        }

        return cofe;
    }
    #endregion

    #region Update Coffee
    [HttpGet]
    [Route("/UpdateCoffee")]
    public int UpdateCoffee(int coffeeId,
    string coffeeName,
    string region = "",
    string processing = "",
    string varietal = "",
    string roastType = "",
    string weight = "",
    string roastDate = "")
    {
        //CoffeeId, CoffeeName, Region, Processing, Varietal, RoastType, Weight, RoastDate

        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        var sql = @$"update Coffee set
CoffeeName = @CoffeeName,
Region = @Region,
Processing = @Processing,
Varietal = @Varietal,
RoastType = @RoastType,
Weight = @Weight,
RoastDate = @RoastDate
where CoffeeId = @CoffeeId
;";

        Coffee cofe = new Coffee();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            SqlParameter paramCoffeeId = new SqlParameter("@CoffeeId", coffeeId); // == null ? (object)DBNull.Value : employee.FirstId);
            SqlParameter paramCoffeeName = new SqlParameter("@CoffeeName", coffeeName); // == null ? (object)DBNull.Value : employee.FirstName);
            SqlParameter paramRegion = new SqlParameter("@Region", region); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramProcessing = new SqlParameter("@Processing", processing); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramVarietal = new SqlParameter("@Varietal", varietal); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramRoastType = new SqlParameter("@RoastType", roastType); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramWeight = new SqlParameter("@Weight", weight); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramRoastDate = new SqlParameter("@RoastDate", roastDate); // == null ? (object)DBNull.Value : employee.Salary);

            paramCoffeeId.DbType = DbType.Int32;
            paramCoffeeName.DbType = DbType.String;
            paramRegion.DbType = DbType.String;
            paramProcessing.DbType = DbType.String;
            paramVarietal.DbType = DbType.String;
            paramRoastType.DbType = DbType.String;
            paramWeight.DbType = DbType.String;
            paramRoastDate.DbType = DbType.String;

            sqlCommand.Parameters.Add(paramCoffeeId);
            sqlCommand.Parameters.Add(paramCoffeeName);
            sqlCommand.Parameters.Add(paramRegion);
            sqlCommand.Parameters.Add(paramProcessing);
            sqlCommand.Parameters.Add(paramVarietal);
            sqlCommand.Parameters.Add(paramRoastType);
            sqlCommand.Parameters.Add(paramWeight);
            sqlCommand.Parameters.Add(paramRoastDate);

            return sqlCommand.ExecuteNonQuery();
        }
    }
    #endregion
}