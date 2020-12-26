using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

/// <summary>
/// Summary description for DataBase
/// </summary>
public class DataBase
{

    public DataBase()
    {


    }
    
    private static string host = "localhost";
    private static string username = "postgres";
    private static string password = "guardasol23";
    private static string database = "ISI_TP2_Agenda";

    private static string connectionString = "Host=" + host + ";Username=" + username + ";Password=" + password + ";Database=" + database;

    public DataTable ExecuteReturnQuery(string query)
    {
        DataTable dt = new DataTable();

        using (var connection = new NpgsqlConnection(connectionString))
        {

            connection.Open();

            using (var cmd = new NpgsqlCommand(query, connection))
            {

                
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connectionString);


                da.Fill(dt);


            }

        }

        return dt;

    }

}