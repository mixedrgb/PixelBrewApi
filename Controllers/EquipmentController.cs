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
    public EquipmentResponse InsertEquipment(string grinderManufacturer,
    string? grinderModel,
    string? grinderType,
    string? grinderSetting)
    {

        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();

        var sql = @$"INSERT INTO Equipment
(GrinderManufacturer, GrinderModel, GrinderType, GrinderSetting)
VALUES
(@GrinderManufacturer, @GrinderModel, @GrinderType, @GrinderSetting)
;";
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            SqlParameter paramGrinderManfacturer = new SqlParameter("GrinderManufacturer",
                    grinderManufacturer == null ? (object)DBNull.Value : grinderManufacturer);

            SqlParameter paramGrinderModel = new SqlParameter("@GrinderModel",
                    grinderModel == null ? (object)DBNull.Value : grinderModel);

            SqlParameter paramGrinderType = new SqlParameter("@GrinderType",
                    grinderType == null ? (object)DBNull.Value : grinderType);

            SqlParameter paramGrinderSetting = new SqlParameter("@GrinderSetting",
                    grinderSetting == null ? (object)DBNull.Value : grinderSetting);

            paramGrinderManfacturer.DbType = DbType.String;
            paramGrinderModel.DbType = DbType.String;
            paramGrinderType.DbType = DbType.String;
            paramGrinderSetting.DbType = DbType.String;

            sqlCommand.Parameters.Add(paramGrinderManfacturer);
            sqlCommand.Parameters.Add(paramGrinderModel);
            sqlCommand.Parameters.Add(paramGrinderType);
            sqlCommand.Parameters.Add(paramGrinderSetting);

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
    [HttpPut]
    [Route("/UpdateEquipment")]
    public EquipmentResponse UpdateEquipment(int grinderId,
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

        string sql = PBQuery.BuildGrinderQuery(equip);

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql.ToString(), sqlConnection);
            sqlCommand.CommandType = CommandType.Text;

            SqlParameter paramGrinderId = new SqlParameter("@GrinderId", grinderId);
            SqlParameter paramGrinderName = new SqlParameter("@GrinderManufacturer", manufacturer);
            SqlParameter paramModel = new SqlParameter("@GrinderModel", model);
            SqlParameter paramType = new SqlParameter("@GrinderType", type);
            SqlParameter paramSetting = new SqlParameter("@GrinderSetting", setting);

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

            sqlCommand.ExecuteNonQuery();
            var equipResponse = new EquipmentResponse();
            equipResponse.Message = "Equipment updated successfully";
            equipResponse.Status = "Success";
            equipResponse.Equipments = GetEquipment();
            return equipResponse;
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

        var sql = @$"DELETE FROM Equipment WHERE GrinderId = @GrinderId;";
        Equipment cofe = new Equipment();
        var equipResponse = new EquipmentResponse();
        try
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;

                SqlParameter paramEquipmentId = new SqlParameter("@GrinderId", grinderId);

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
