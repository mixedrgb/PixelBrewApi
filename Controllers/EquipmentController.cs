using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace PixelBrewApi;

[ApiController]
[Route("[controller]")]
public class EquipmentController
{

    #region Insert Equipment
    [HttpPost]
    [Route("/InsertEquipment")]
    public EquipmentResponse InsertEquipment(string coffeeName = "Barista's Choice",
    string? region = "Vietnam",
    string? processing = "Natural",
    string? varietal = "Orange Bourbon",
    string? roastType = "Medium-light",
    string? weight = "340g",
    string? roastDate = "1970/01/01")
    {

        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        var sql = @$"INSERT INTO Equipment
(EquipmentName, Region, Processing, Varietal, RoastType, Weight, RoastDate)
VALUES
(@EquipmentName, @Region, @Processing, @Varietal, @RoastType, @Weight, @RoastDate)
;";
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            SqlParameter paramEquipmentName = new SqlParameter("EquipmentName",
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

            paramEquipmentName.DbType = DbType.String;
            paramRegion.DbType = DbType.String;
            paramProcessing.DbType = DbType.String;
            paramVarietal.DbType = DbType.String;
            paramRoastType.DbType = DbType.String;
            paramWeight.DbType = DbType.String;
            paramRoastDate.DbType = DbType.String;

            sqlCommand.Parameters.Add(paramEquipmentName);
            sqlCommand.Parameters.Add(paramRegion);
            sqlCommand.Parameters.Add(paramProcessing);
            sqlCommand.Parameters.Add(paramVarietal);
            sqlCommand.Parameters.Add(paramRoastType);
            sqlCommand.Parameters.Add(paramWeight);
            sqlCommand.Parameters.Add(paramRoastDate);

            // return a 200 OK status method
            sqlCommand.ExecuteNonQuery();

            EquipmentResponse equipResponse = new EquipmentResponse();
            equipResponse.Message = "Equipment inserted successfully";
            equipResponse.Status = "Success";
            Equipment cofe = new Equipment();
            equipResponse.Equipments = GetEquipment();

            return equipResponse;
        }
    }
    #endregion

    #region Get Equipment
    [HttpGet]
    [Route("/GetEquipment")]
    public List<Equipment> GetEquipment()
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();
        var sql = "SELECT * FROM Equipment;";
        List<Equipment> listOfEquipment = new List<Equipment>();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Equipment equip = new Equipment();
                equip.GrinderId = Convert.ToInt32(sqlDataReader["GrinderId"]);
                equip.Manufacturer = sqlDataReader["GrinderManufacturer"].ToString() ?? "";
                equip.Type = sqlDataReader["GrinderType"].ToString();
                equip.Model = sqlDataReader["GrinderModel"].ToString();
                equip.Setting = sqlDataReader["GrinderSetting"].ToString();
                listOfEquipment.Add(equip);
            }
        }

        return listOfEquipment;
    }
    #endregion

    #region Update Equipment
    [HttpPatch]
    [Route("/UpdateEquipment")]
    public int UpdateEquipment(int grinderId,
    string manufacturer,
    string model = "",
    string type = "",
    string setting = "")
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        Equipment equip = new Equipment();
        equip.GrinderId = grinderId;
        equip.Manufacturer = manufacturer;
        equip.Model = model;
        equip.Type = type;
        equip.Setting = setting;

        string sql = EquipmentQuery.BuildQuery(equip);

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql.ToString(), sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            SqlParameter paramGrinderId = new SqlParameter("@EquipmentId", grinderId); // == null ? (object)DBNull.Value : employee.FirstId);
            SqlParameter paramGrinderName = new SqlParameter("@EquipmentName", manufacturer); // == null ? (object)DBNull.Value : employee.FirstName);
            SqlParameter paramModel = new SqlParameter("@Region", model); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramType = new SqlParameter("@Processing", type); // == null ? (object)DBNull.Value : employee.Salary);
            SqlParameter paramSetting = new SqlParameter("@Varietal", setting); // == null ? (object)DBNull.Value : employee.Salary);

            paramGrinderId.DbType = DbType.Int32;
            paramGrinderName.DbType = DbType.String;
            paramModel.DbType = DbType.String;
            paramType.DbType = DbType.String;
            paramSetting.DbType = DbType.String;

            sqlCommand.Parameters.Add(paramGrinderId);
            sqlCommand.Parameters.Add(paramGrinderName);
            sqlCommand.Parameters.Add(paramModel);
            sqlCommand.Parameters.Add(paramType);
            sqlCommand.Parameters.Add(paramSetting);

            return sqlCommand.ExecuteNonQuery();
        }
    }
    #endregion

    #region Delete Equipment
    [HttpDelete]
    [Route("/DeleteEquipment")]
    public EquipmentResponse DeleteEquipment(int grinderId)
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        var sql = @$"DELETE FROM Equipment WHERE EquipmentId = @EquipmentId;";
        Equipment cofe = new Equipment();
        var equipResponse = new EquipmentResponse();
        try
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;

                SqlParameter paramEquipmentId = new SqlParameter("@EquipmentId", grinderId); // == null ? (object)DBNull.Value : employee.FirstId);

                paramEquipmentId.DbType = DbType.Int32;

                sqlCommand.Parameters.Add(paramEquipmentId);

                int rowsAffected = sqlCommand.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    equipResponse.Message = $"Equipment with id {grinderId} deleted successfully";
                }
                else
                {
                    equipResponse.Message = $"Equipment with id {grinderId} not found";
                    equipResponse.Status = "Failure";
                }
            }

            equipResponse.Equipments = GetEquipment();

        }
        catch (Exception e)
        {
            equipResponse.Message = e.Message;
            equipResponse.Status = "Failure";
        }

        return equipResponse;
    }
    #endregion
}
