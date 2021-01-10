using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebAPI_ISI.Models
{
	
	public class Utilizadores
	{

		Database db = new Database();
		DataTable dt = new DataTable();
		Utilizador newUser = new Utilizador();

		/// <summary>
		/// Método que irá adicionar um novo Utilizador à Base de Dados
		/// </summary>
		/// <param name="jsonString">String em formato JSON que contém a informação do Utilizador a ser adicionado</param>
		/// <returns>
		/// true - Registo efetuado com sucesso
		/// false - Já existe um Utilizador com o E-Mail introduzido
		/// </returns>
		public bool Registo(string jsonString)
		{

			int numeroLinhas; string query;

			Utilizador userDeserialized = JsonConvert.DeserializeObject<Utilizador>(jsonString);

			//Retorna todos os utilizadores que contenham o E-Mail introduzido
			dt = db.ReturnUsersEmail(userDeserialized.email);

			//Caso já exista alguém com o E-Mail introduzido, retorna false
			if (dt.Rows.Count > 0)
			{ return false; }

			//Caso contrário
			else
			{

				query = "select * from Utilizador;";

				//Retorna todos os utilizadores para fazer a atribuição do ID
				dt = db.ExecuteReturnQuery(query);

				int numeroID = dt.Rows.Count + 1;

				userDeserialized.id = numeroID;

				//Executa a inserção do novo utilizador na Base de Dados e retorna true
				numeroLinhas = db.ExecuteInsertUser(userDeserialized);

				return true;
			}

		}

		/// <summary>
		/// Método que executa o Login da aplicação
		/// </summary>
		/// <param name="email">E-Mail introduzido na aplicação</param>
		/// <param name="password">Password introduzida na aplicação</param>
		/// <returns>
		/// Objeto do tipo "Aux" cuja propriedade "json" irá conter uma string em formato JSON com os dados do Utilizador
		/// </returns>
		public Aux Login(string email, string password)
		{
			string jsonString;

			dt = db.ReturnUsersEmailPassword(email, password);

			//Se encontrar algum utilizador com o mail e a password introduzidos
			if (dt.Rows.Count != 0)
			{

				newUser.id = Convert.ToInt32(dt.Rows[0]["id"]);
				newUser.nome = dt.Rows[0]["nome"].ToString();
				newUser.cidade = dt.Rows[0]["cidade"].ToString();
				newUser.id_cidade = Convert.ToInt32(dt.Rows[0]["id_cidade"]);
				newUser.email = dt.Rows[0]["email"].ToString();
				newUser.password = dt.Rows[0]["password"].ToString();

				jsonString = JsonConvert.SerializeObject(newUser);

			}

			else { jsonString = null; }

			Aux aux = new Aux() { Json = jsonString };

			return aux;

		}

	}

	/// <summary>
	/// Classe que contém a informação de um utilizador
	/// </summary>
	public class Utilizador
	{

		public int id { get; set; }

		public string nome { get; set; }

		public string cidade { get; set; }

		public int id_cidade { get; set; }

		public string email { get; set; }

		public string password { get; set; }

		public Utilizador()
		{

			id = default(int);
			nome = default(string);
			cidade = default(string);
			id_cidade = default(int);
			email = default(string);
			password = default(string);

		}

		public Utilizador(int Id, string Nome, string Cidade, int IDCidade, string Email, string Password)
		{

			id = Id;
			nome = Nome;
			cidade = Cidade;
			id_cidade = IDCidade;
			email = Email;
			password = Password;

		}

	}

	/// <summary>
	/// Classe auxiliar disponibilizada pelo professor Luis Ferreira para resolver os problemas nos métodos do tipo GET
	/// </summary>
	[DataContract]
	public class Aux
	{
		[DataMember]
		public string Json { get; set; }

		public Aux() { }

		public Aux(string s)
		{
			Json = s;
		}

	}
}
