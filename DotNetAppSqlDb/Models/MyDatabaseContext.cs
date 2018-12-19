using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.Azure.Services.AppAuthentication;
using System.Web.Configuration;

namespace DotNetAppSqlDb.Models
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(SqlConnection conn) : base(conn, true)
        {
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            // DataSource != LocalDB means app is running in Azure with the SQLDB connection string you configured
            if (conn.DataSource != "(localdb)\\MSSQLLocalDB")
                conn.AccessToken = (new AzureServiceTokenProvider()).GetAccessTokenAsync("https://database.windows.net/").Result;

            Database.SetInitializer<MyDatabaseContext>(null);
        }
        
        public System.Data.Entity.DbSet<DotNetAppSqlDb.Models.Todo> Todoes { get; set; }
    }
}
