using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_ISI.Models
{
    public class Database
    {

        public Database()
        {

        }

        private static string connectionString = "Server=isitp2server.postgres.database.azure.com;Database=isi_tp2;Port=5432;User Id=Bernas@isitp2server;Password=Guardasol23;Ssl Mode=Require;";

        #region Evento

        public int ExecuteInsertEvento(Evento evento)
        {

            string query = "INSERT INTO Evento(ID, Data, Titulo, Descricao, Id_Utilizador) VALUES (@id, @data, @titulo, @descricao, @id_utilizador);";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id", evento.id);
                    command.Parameters.AddWithValue("@data", evento.data);
                    command.Parameters.AddWithValue("@titulo", evento.titulo);
                    command.Parameters.AddWithValue("@descricao", evento.descricao);
                    command.Parameters.AddWithValue("@id_utilizador", evento.id_utilizador);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ReturnSpecificEvent(Evento eventoDeserialized)
        {

            string query = "select * from evento where data= TO_DATE(@data, 'DD/MM/YYYY') and titulo= @titulo and descricao= @descricao and id_utilizador= @id_utilizador;";

            DataTable dt = new DataTable();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@data", eventoDeserialized.data.ToShortDateString());
                cmd.Parameters.AddWithValue("@titulo", eventoDeserialized.titulo);
                cmd.Parameters.AddWithValue("@descricao", eventoDeserialized.descricao);
                cmd.Parameters.AddWithValue("@id_utilizador", eventoDeserialized.id_utilizador);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                da.Fill(dt);
            }

            return dt;

        }

        public DataTable ReturnEventosUser(string id_utilizador)
        {

            string query = "select * from Evento where id_utilizador= @id_utilizador;";

            DataTable dt = new DataTable();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id_utilizador", Convert.ToInt32(id_utilizador));

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                da.Fill(dt);
            }

            return dt;

        }

        public int ExecuteDeleteEvento(Evento eventoDeserialized)
        {
            string query = "DELETE FROM Evento WHERE ID = @id;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", eventoDeserialized.id);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteUpdateEvento(Evento evento)
        {

            string query = "UPDATE Evento SET Data = @data, Titulo = @titulo, Descricao = @descricao, Id_Utilizador = @id_utilizador WHERE ID = @id;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id", evento.id);
                    command.Parameters.AddWithValue("@data", evento.data);
                    command.Parameters.AddWithValue("@titulo", evento.titulo);
                    command.Parameters.AddWithValue("@descricao", evento.descricao);
                    command.Parameters.AddWithValue("@id_utilizador", evento.id_utilizador);

                    return command.ExecuteNonQuery();
                }
            }

        }

        #endregion

        /// <summary>
        /// Método que executa uma query não parametrizada para retornar informação da base de dados em formato DataTable
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable ExecuteReturnQuery(string query)
        {
            DataTable dt = new DataTable();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connectionString);

                da.Fill(dt);
            }
            return dt;
        }

        #region Utilizador

        public DataTable ReturnUsersEmail(string email)
        {

            string query = "select * from utilizador where email= @email;";

            DataTable dt = new DataTable();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@email", email);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                da.Fill(dt);
            }

            return dt;

        }

        public DataTable ReturnUsersEmailPassword(string email, string password)
        {

            string query = "select * from utilizador where email= @email and password= @password;";

            DataTable dt = new DataTable();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                da.Fill(dt);
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

        #endregion

    }
}
