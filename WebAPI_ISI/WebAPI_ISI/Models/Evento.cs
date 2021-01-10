using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebAPI_ISI.Models
{
	public class Eventos
    {

		Database db = new Database();
		DataTable dt = new DataTable();
		Evento evento = new Evento();
		List<Evento> listaEventos = new List<Evento>();

		/// <summary>
		/// Método que adiciona um novo Evento à Base de Dados
		/// </summary>
		/// <param name="jsonString">String em formato JSON com a informação do novo Evento a ser adicionado</param>
		/// <returns>
		/// true - Sucesso na criação do Evento
		/// false - O utilizador já criou um evento com os mesmos dados
		/// </returns>
		public bool AddEvento(string jsonString)
		{
			
			int numeroLinhas;

			Evento eventoDeserialized = JsonConvert.DeserializeObject<Evento>(jsonString);

			//Procura na base de dados por um evento com os mesmos dados do que aqueles que o utilizador introduziu
			dt = db.ReturnSpecificEvent(eventoDeserialized);

			//Caso já exista um evento com os mesmos dados, retorna false
			if (dt.Rows.Count > 0)
			{ return false; }

			//Caso contrário
			else
			{

				string query = "select * from evento;";

				//Retorna todos os eventos para fazer a atribuição do id
				dt = db.ExecuteReturnQuery(query);

				int numeroID = dt.Rows.Count + 1;
				
				eventoDeserialized.id = numeroID;

				//Insere o evento na base de dados e retorna true
				numeroLinhas = db.ExecuteInsertEvento(eventoDeserialized);

				return true;

			}

		}

		/// <summary>
		/// Método que retorna todos os eventos do utilizador
		/// </summary>
		/// <param name="id_utilizador">Id do utilizador</param>
		/// <returns>
		/// Objeto do tipo "Aux" cuja propriedade "json" irá conter uma string em formato JSON com os eventos do utilizador
		/// </returns>
		public Aux GetEventos(string id_utilizador)
		{
			string jsonString;

			//Retorna todos os eventos do utilizador
			dt = db.ReturnEventosUser(id_utilizador);

			//Caso o utilizador tenha eventos previamente criados
			if (dt.Rows.Count != 0)
			{

				//Percorre todas as linhas da DataTable "dt" para fazer a correta atribuição dos seus (linha) valores a um novo evento e adicioná-lo a uma lista de eventos
				foreach (DataRow linha in dt.Rows)
				{
					//O new Evento tem como objetivo eliminar o overwrite que acontecia com a variavel evento
					evento = new Evento(
					Convert.ToInt32(linha["id"]),
					Convert.ToDateTime(linha["data"]),
					linha["titulo"].ToString(),
					linha["descricao"].ToString(),
					Convert.ToInt32(linha["id_utilizador"])
					);

					listaEventos.Add(evento);
				}

				//Serializa o conteúdo da lista de eventos para uma string em formato JSON
				jsonString = JsonConvert.SerializeObject(listaEventos);

			}

			//Caso o utilizador não tenha eventos previamente criados, a variável "jsonString" torna-se null
			else { jsonString = null; }

			//Atribui-se o conteúdo da string "jsonString" à propriedade "Json" do objeto "aux"
			Aux aux = new Aux() { Json = jsonString };

			//Retorna o objeto "aux" do tipo "Aux"
			return aux;
		}

		/// <summary>
		/// Método que irá eliminar um Evento da Base de Dados
		/// </summary>
		/// <param name="jsonString">String em formato JSON que irá conter a informação do Evento a ser eliminado</param>
		/// <returns></returns>
		public bool DeleteEvento(string jsonString)
		{

			int numeroLinhas;

			Evento eventoDeserialized = JsonConvert.DeserializeObject<Evento>(jsonString);

			//Remove o evento desejado da Base de Dados e retorna true
			numeroLinhas = db.ExecuteDeleteEvento(eventoDeserialized);

			return true;

		}

		/// <summary>
		/// Método que irá alterar a informação de um Evento
		/// </summary>
		/// <param name="jsonString">String em formato JSON que irá conter a informação do Evento a ser alterado</param>
		/// <returns></returns>
		public bool UpdateEvento(string jsonString)
		{

			int numeroLinhas;

			Evento eventoDeserialized = JsonConvert.DeserializeObject<Evento>(jsonString);

			//Altera o evento desejado na Base de Dados
			numeroLinhas = db.ExecuteUpdateEvento(eventoDeserialized);

			return true;

		}

	}
	
	/// <summary>
	/// Classe que contém a informação de um Evento
	/// </summary>
	public class Evento
	{
		[DataMember]
		public int id { get; set; }

		[JsonProperty(ItemConverterType = typeof(IsoDateTimeConverter))]
		[DataMember]
		public DateTime data { get; set; }

		[DataMember(Name = "dataEvento")]
		private string HiredForSerialization { get; set; }

		//Maneira encontrada no StackOverflow para fazer a correta serialização de um objeto do tipo DateTime
		[OnSerializing]
		void OnSerializing(StreamingContext ctx)
		{
			this.HiredForSerialization = JsonConvert.SerializeObject(this.data).Replace('"', ' ').Trim();
		}

		[DataMember]
		public string titulo { get; set; }

		[DataMember]
		public string descricao { get; set; }

		[DataMember]
		public int id_utilizador { get; set; }

		public Evento()
		{

			id = default(int);
			data = default(DateTime);
			titulo = default(string);
			descricao = default(string);
			id_utilizador = default(int);

		}

		public Evento(int Id, DateTime Data, string Titulo, string Descricao, int Id_utilizador)
		{

			id = Id;
			data = Data;
			titulo = Titulo;
			descricao = Descricao;
			id_utilizador = Id_utilizador;
		}

	}
}
