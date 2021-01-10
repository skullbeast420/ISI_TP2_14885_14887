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

        //String que contém a informação para fazer a ligação à Base de Dados
        private static string connectionString = "Server=isitp2server.postgres.database.azure.com;Database=isi_tp2;Port=5432;User Id=Bernas@isitp2server;Password=Guardasol23;Ssl Mode=Require;";

        #region Evento

        /// <summary>
        /// Insere um novo evento na base de dados
        /// </summary>
        /// <param name="evento">Evento a ser inserido</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna um evento em específico
        /// </summary>
        /// <param name="eventoDeserialized">Evento a ser retornado</param>
        /// <returns></returns>
        public DataTable ReturnSpecificEvent(Evento eventoDeserialized)
        {

            string query = "select * from evento where data= TO_DATE(@data, 'DD/MM/YYYY') and titulo= @titulo and descricao= @descricao and id_utilizador= @id_utilizador;";

            DataTable dt = new DataTable();

            try
            {

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

            }
            catch(Exception e)
            { }
            
            return dt;
        }

        /// <summary>
        /// Retorna um conjunto de eventos
        /// </summary>
        /// <param name="id_utilizador">Id do utilizador cujos eventos querem ser retornados</param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove um evento da base de dados
        /// </summary>
        /// <param name="eventoDeserialized">Evento a ser removido</param>
        /// <returns></returns>
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

        /// <summary>
        /// Altera um evento na base de dados
        /// </summary>
        /// <param name="evento">Evento a ser alterado</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna os utilizadores com o E-Mail especificado
        /// </summary>
        /// <param name="email">E-Mail a procurar nos utilizadores</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna os utilizadores com o E-Mail e Password especificados
        /// </summary>
        /// <param name="email">E-Mail a procurar nos utilizadores</param>
        /// <param name="password">Password a procurar nos utilizadores</param>
        /// <returns></returns>
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

        /// <summary>
        /// Insere um novo utilizador na base de dados
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
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
