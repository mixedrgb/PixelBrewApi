using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PixelBrewApi;
public class SqlConnectionString
{
    public string GetConnectionString()
    {
        // read the value of COMPUTERNAME from the environment
        string serverName = Environment.GetEnvironmentVariable("COMPUTERNAME");
        //string serverName = File.ReadAllText(@".\.sql-environment");
        //serverName = serverName.Split("=")[1].Trim();
        serverName += @"\SQLEXPRESS";
        string databaseName = "PixelBrew";
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;TrustServerCertificate=True;";
        return connectionString;
    }
}