using AkshaySDbDiscoverySQLServer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace AkshaySDbDiscoverySQLServer
{
    public class DbTypeQueries
    {
        public string ConnectionString { get; set; }

        public List<Table> GetAllTablesInfo()
        {
            List<Table> tables = new List<Table>();
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            string queryText = "select table_name from information_schema.tables";
            SqlCommand sqlCommand = new SqlCommand(queryText, sqlConnection);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while(dr.Read())
            {
                Table table = new Table();
                table.TableName = dr["table_name"].ToString();
                tables.Add(table);
            }
            dr.Close();
            sqlConnection.Close();
            return tables;
        }
    }
}
