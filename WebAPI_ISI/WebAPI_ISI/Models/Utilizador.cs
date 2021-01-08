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

		public bool Registo(string jsonString)
		{

			int numeroLinhas; string query;

			Utilizador userDeserialized = JsonConvert.DeserializeObject<Utilizador>(jsonString);

			query = "select id, nome, cidade, id_cidade, email, password from Utilizador where email='" + userDeserialized.email + "';";

			dt = db.ExecuteReturnQuery(query);

			if (dt.Rows.Count > 0)
			{ return false; }

			else
			{

				query = "select id, nome, cidade, id_cidade, email, password from Utilizador";

				dt = db.ExecuteReturnQuery(query);

				int numeroID = dt.Rows.Count + 1;

				userDeserialized.id = numeroID;

				numeroLinhas = db.ExecuteInsertUser(userDeserialized);

				return true;
			}

		}

		public Aux Login(string email, string password)
		{
			string jsonString;

			if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
			{

				string query = "select id, nome, cidade, id_cidade, email, password from Utilizador where email='" + email + "' and password='" + password + "'";

				dt = db.ExecuteReturnQuery(query);

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
			}

			else { jsonString = null; }

			Aux aux = new Aux() { Json = jsonString };

			return aux;

		}

	}

	/// <summary>
	/// Summary description for Utilizador
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
