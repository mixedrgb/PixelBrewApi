using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace PixelBrewApi;
public class CoffeeEquipmentController
{

    [HttpGet]
    [Route("/GetEquipment")]
    public string GetEquipment(string model, string manufacturer)
    {
        var sqlConn = new SqlConnectionString();
        string connectionString = sqlConn.GetConnectionString();
        var sql = "select * from Coffee;";
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Employee employee = new Employee();

                employee.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"].ToString());
                employee.LastName = sqlDataReader["LastName"].ToString();
                employee.FirstName = sqlDataReader["FirstName"].ToString();
                employee.Salary = Convert.ToDecimal(sqlDataReader["Salary"].ToString() == "" ? "0.00" : sqlDataReader["Salary"].ToString());

                employees.Add(employee);
            }
        }

        string message = "";
        return message;
    }
}