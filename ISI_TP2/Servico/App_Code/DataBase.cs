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

    public int ExecuteInsertUser(Utilizador currentUser)
    {

        string query = "INSERT INTO Utilizador(ID, Nome, Cidade, Id_Cidade, Email, Password) VALUES (@id, @nome, @cidade, @idCidade, @email, @password);";
        
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new NpgsqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@id", currentUser.id);
                command.Parameters.AddWithValue("@nome", currentUser.nome);
                command.Parameters.AddWithValue("@cidade", currentUser.cidade);
                command.Parameters.AddWithValue("@idCidade", currentUser.id_cidade);
                command.Parameters.AddWithValue("@email", currentUser.email);
                command.Parameters.AddWithValue("@password", currentUser.password);

                return command.ExecuteNonQuery();
            }
        }

    }

}