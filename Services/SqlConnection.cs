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
        string envVar = File.ReadAllText("./sql-environment");
        envVar = envVar.Split("=")[1].Trim();
        string? serverName = Environment.GetEnvironmentVariable(envVar);
        string databaseName = "SQLEXPRESS";
        string connectionString = $"data source={serverName}; database={databaseName}; Integrated Security=true;";
        return connectionString;
    }
}