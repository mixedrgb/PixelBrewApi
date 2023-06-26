using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace PixelBrewApi;

[ApiController]
[Route("[controller]")]
public class CoffeeController
{

    #region Insert Coffee
    [HttpPost]
    [Route("/InsertCoffee")]
    public CoffeeResponse InsertCoffee(string coffeeName,
    string? region,
    string? roastDate,
    string? roastType,
    string? processing,
    string? varietal,
    string? weight
    )
    {

        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        var sql = @$"INSERT INTO Coffee
(CoffeeName, Region, Processing, Varietal, RoastType, Weight, RoastDate)
VALUES
(@CoffeeName, @Region, @Processing, @Varietal, @RoastType, @Weight, @RoastDate)
;";
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            SqlParameter paramCoffeeName = new SqlParameter("CoffeeName",
                    coffeeName == null ? (object)DBNull.Value : coffeeName);

            SqlParameter paramRegion = new SqlParameter("@Region",
                    region == null ? (object)DBNull.Value : region);

            SqlParameter paramProcessing = new SqlParameter("@Processing",
                    processing == null ? (object)DBNull.Value : processing);

            SqlParameter paramVarietal = new SqlParameter("@Varietal",
                    varietal == null ? (object)DBNull.Value : varietal);

            SqlParameter paramRoastType = new SqlParameter("@RoastType",
                    roastType == null ? (object)DBNull.Value : roastType);

            SqlParameter paramWeight = new SqlParameter("@Weight",
                    weight == null ? (object)DBNull.Value : weight);

            SqlParameter paramRoastDate = new SqlParameter("@RoastDate",
                    roastDate == null ? (object)DBNull.Value : roastDate);

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

            sqlCommand.ExecuteNonQuery();

            CoffeeResponse response = new CoffeeResponse();
            response.Message = "Coffee inserted successfully";
            response.Status = "Success";
            Coffee cofe = new Coffee();
            response.Coffees = GetCoffee();

            return response;
        }
    }
    #endregion

    #region Get Coffee
    [HttpGet]
    [Route("/GetCoffee")]
    public List<Coffee> GetCoffee()
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();
        var sql = "SELECT * FROM Coffee;";
        List<Coffee> listOfCoffee = new List<Coffee>();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Coffee cofe = new Coffee();
                cofe.CoffeeId = Convert.ToInt32(sqlDataReader["CoffeeId"]);
                cofe.CoffeeName = sqlDataReader["CoffeeName"].ToString() ?? "";
                cofe.Region = sqlDataReader["Region"].ToString();
                cofe.RoastDate = sqlDataReader["RoastDate"].ToString();
                cofe.RoastType = sqlDataReader["RoastType"].ToString();
                cofe.Processing = sqlDataReader["Processing"].ToString();
                cofe.Varietal = sqlDataReader["Varietal"].ToString();
                cofe.Weight = sqlDataReader["Weight"].ToString();
                listOfCoffee.Add(cofe);
            }
        }

        return listOfCoffee;
    }
    #endregion

    #region Update Coffee
    [HttpPut]
    [Route("/UpdateCoffee")]
    public CoffeeResponse UpdateCoffee(int coffeeId,
    string? coffeeName,
    string? region,
    string? roastDate,
    string? roastType,
    string? processing,
    string? varietal,
    string? weight)
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();
        var cofeResp = new CoffeeResponse();

        Coffee cofe = new Coffee();
        cofe.CoffeeName = coffeeName;
        cofe.Region = region;
        cofe.RoastDate = roastDate;
        cofe.RoastType = roastType;
        cofe.Processing = processing;
        cofe.Varietal = varietal;
        cofe.Weight = weight;

        string sql = PBQuery.BuildCoffeeQuery(cofe);
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql.ToString(), sqlConnection);
                sqlCommand.CommandType = CommandType.Text;

                SqlParameter paramCoffeeId = new SqlParameter("@CoffeeId", coffeeId); // == null ? (object)DBNull.Value : employee.FirstId);
                sqlCommand.Parameters.Add(paramCoffeeId);
                paramCoffeeId.DbType = DbType.Int32;
                SqlParameter paramCoffeeName = new SqlParameter("", "");
                SqlParameter paramRegion = new SqlParameter("", "");
                SqlParameter paramProcessing = new SqlParameter("", "");
                SqlParameter paramVarietal = new SqlParameter("", "");
                SqlParameter paramRoastType = new SqlParameter("", "");
                SqlParameter paramWeight = new SqlParameter("", "");
                SqlParameter paramRoastDate = new SqlParameter("", "");

                if (coffeeName != null && coffeeName != "")
                {
                    paramCoffeeName = new SqlParameter("@CoffeeName", coffeeName); // == null ? (object)DBNull.Value : employee.FirstName);
                    sqlCommand.Parameters.Add(paramCoffeeName);
                    paramCoffeeName.DbType = DbType.String;
                }
                if (region != null && region != "")
                {
                    System.Console.WriteLine(region);
                    paramRegion = new SqlParameter("@Region", region); // == null ? (object)DBNull.Value : employee.Salary);
                    sqlCommand.Parameters.Add(paramRegion);
                    paramRegion.DbType = DbType.String;
                }
                if (processing != null && processing != "")
                {
                    paramProcessing = new SqlParameter("@Processing", processing); // == null ? (object)DBNull.Value : employee.Salary);
                    sqlCommand.Parameters.Add(paramProcessing);
                    paramProcessing.DbType = DbType.String;
                }
                if (varietal != null && varietal != "")
                {
                    paramVarietal = new SqlParameter("@Varietal", varietal); // == null ? (object)DBNull.Value : employee.Salary);
                    sqlCommand.Parameters.Add(paramVarietal);
                    paramVarietal.DbType = DbType.String;
                }
                if (roastType != null && roastType != "")
                {
                    paramRoastType = new SqlParameter("@RoastType", roastType); // == null ? (object)DBNull.Value : employee.Salary);
                    sqlCommand.Parameters.Add(paramRoastType);
                    paramRoastType.DbType = DbType.String;
                }
                if (weight != null && weight != "")
                {
                    paramWeight = new SqlParameter("@Weight", weight); // == null ? (object)DBNull.Value : employee.Salary);
                    sqlCommand.Parameters.Add(paramWeight);
                    paramWeight.DbType = DbType.String;
                }
                if (roastDate != null && roastDate != "")
                {
                    paramRoastDate = new SqlParameter("@RoastDate", roastDate); // == null ? (object)DBNull.Value : employee.Salary);
                    sqlCommand.Parameters.Add(paramRoastDate);
                    paramRoastDate.DbType = DbType.String;
                }

                sqlCommand.ExecuteNonQuery();
                cofeResp.Message = "Coffee updated successfully";
                cofeResp.Status = "Success";
                cofeResp.Coffees = GetCoffee();
                return cofeResp;
            }
        }
        catch (Exception e)
        {
            cofeResp.Message = e.Message;
            cofeResp.Status = "Failure";
            return cofeResp;
        }
    }
    #endregion

    #region Delete Coffee
    [HttpDelete]
    [Route("/DeleteCoffee")]
    public CoffeeResponse DeleteCoffee(int coffeeId)
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        var sql = @$"DELETE FROM Coffee WHERE CoffeeId = @CoffeeId;";
        Coffee cofe = new Coffee();
        var response = new CoffeeResponse();
        try
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;

                SqlParameter paramCoffeeId = new SqlParameter("@CoffeeId", coffeeId); // == null ? (object)DBNull.Value : employee.FirstId);

                paramCoffeeId.DbType = DbType.Int32;

                sqlCommand.Parameters.Add(paramCoffeeId);

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    response.Message = $"Coffee with id {coffeeId} deleted successfully";
                }
                else
                {
                    response.Message = $"Coffee with id {coffeeId} not found";
                    response.Status = "Failure";
                }
            }

            response.Coffees = GetCoffee();

        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = "Failure";
        }

        return response;
    }
    #endregion
}