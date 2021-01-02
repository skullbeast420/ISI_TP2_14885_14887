using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using Npgsql;
using System.ServiceModel.Activation;
using System.Threading.Tasks;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.

public class Service : IServiceRest
{

	DataBase db = new DataBase();
	DataTable dt = new DataTable();
	Utilizador newUser = new Utilizador();
	Evento evento = new Evento();
	List<Evento> listaEventos = new List<Evento>();

	/// <summary>
	/// Acede à API do IPMA para obter os nomes das cidades e os seus idglobal's
	/// </summary>
	/// <returns>
	/// locais- dictionary com os id's e nomes das cidades
	/// </returns>
	public Dictionary<int, string> RetornaCidades()
	{

		Dictionary<int, string> locais = new Dictionary<int, string>();
		StringBuilder uri = new StringBuilder();
		Locais listaLocais = new Locais();

		uri.Append("https://api.ipma.pt/open-data/distrits-islands.json");

		HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

		using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
		{

			if (response.StatusCode != HttpStatusCode.OK)
			{

				string message = string.Format("GET falhou. HTTP recebido {0}", response.StatusCode);
				throw new ApplicationException(message);

			}

			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Locais));
			object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
			listaLocais = (Locais)objResponse;

		}

		foreach (Local m in listaLocais.data)
		{

			locais.Add(m.globalIdLocal, m.local);

		}

		return locais;
	}

	/// <summary>
	/// Acede à API do IPMA para obter os diferentes tipos de tempo e as suas descrições em PT e em EN
	/// </summary>
	/// <returns></returns>
	public TiposTempo GetWeatherTypes()
    {

		StringBuilder uri = new StringBuilder();

		TiposTempo weatherTypes = new TiposTempo();

		uri.Append("https://api.ipma.pt/open-data/weather-type-classe.json");

		HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

		using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
		{

			if (response.StatusCode != HttpStatusCode.OK)
			{

				string message = string.Format("GET falhou. HTTP recebido {0}", response.StatusCode);
				throw new ApplicationException(message);

			}

			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TiposTempo));
			object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
			weatherTypes = (TiposTempo)objResponse;

		}

		return weatherTypes;

	}

	public Previsao5dias Get5DayWeather(string idCidade)
    {

		StringBuilder uri = new StringBuilder();

		Previsao5dias previsao = new Previsao5dias();

		uri.Append("https://api.ipma.pt/open-data/forecast/meteorology/cities/daily/");
		uri.Append(idCidade + ".json");

		HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

		using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
		{

			if (response.StatusCode != HttpStatusCode.OK)
			{

				string message = string.Format("GET falhou. HTTP recebido {0}", response.StatusCode);
				throw new ApplicationException(message);

			}

			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Previsao5dias));
			object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
			previsao = (Previsao5dias)objResponse;

		}

		return previsao;

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

	public Aux GetEventos(string id_utilizador)
	{
		string jsonString;

		string query = "select * from Evento where id_utilizador='" + id_utilizador + "'";

		dt = db.ExecuteReturnQuery(query);

		//Caso encontre os eventos do utilizador inserido
		if (dt.Rows.Count != 0)
		{

			foreach(DataRow linha in dt.Rows)
            {
				//O new Evento tem como objetivo eliminar o overwrite que acontecia com a variavel evento
				evento = new Evento(
				Convert.ToInt32(linha["id"].ToString()),
				Convert.ToDateTime(linha["data"]),
				linha["titulo"].ToString(),
				linha["descricao"].ToString(),
				Convert.ToInt32(linha["id_utilizador"])
				);

				listaEventos.Add(evento);
            }
			
			jsonString = JsonConvert.SerializeObject(listaEventos);

		}

		else { jsonString = null; }
		

		Aux aux = new Aux() { Json = jsonString };

		return aux;
	}

	public async Task<bool> Registo(string jsonString)
    {

		int numeroLinhas;
		
		Utilizador userDeserialized = JsonConvert.DeserializeObject<Utilizador>(jsonString);

		string query = "select id, nome, cidade, id_cidade, email, password from Utilizador where email='" + userDeserialized.email + "'";

		dt = db.ExecuteReturnQuery(query);

		if (dt.Rows.Count > 0) 
		{ await Task.Delay(3000); return false; }

		else
		{

			query = "select id, nome, cidade, id_cidade, email, password from Utilizador";

			dt = db.ExecuteReturnQuery(query);

			int numeroID = dt.Rows.Count + 1;

			numeroLinhas = db.ExecuteInsertUser(userDeserialized);

			return true;
		}

	}
}


