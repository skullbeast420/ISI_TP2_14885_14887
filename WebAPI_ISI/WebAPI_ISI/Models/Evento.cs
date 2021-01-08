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

		public bool AddEvento(string jsonString)
		{

			Random rdm = new Random();
			
			int numeroLinhas;

			Evento eventoDeserialized = JsonConvert.DeserializeObject<Evento>(jsonString);

			string query = "select id, data, titulo, descricao, id_utilizador from evento where data= TO_DATE('" + eventoDeserialized.data.ToShortDateString() +
				"', 'DD/MM/YYYY') and titulo='" + eventoDeserialized.titulo + "' and descricao='" + eventoDeserialized.descricao + "' and id_utilizador=" + eventoDeserialized.id_utilizador + ";";

			dt = db.ExecuteReturnQuery(query);

			if (dt.Rows.Count > 0)
			{ return false; }

			else
			{

				query = "select * from evento;";

				dt = db.ExecuteReturnQuery(query);

				int numeroID = dt.Rows.Count + 1;
				
				eventoDeserialized.id = numeroID;

				numeroLinhas = db.ExecuteInsertEvento(eventoDeserialized);

				return true;

			}

		}

		public Aux GetEventos(string id_utilizador)
		{
			string jsonString;

			string query = "select * from Evento where id_utilizador='" + id_utilizador + "'";

			dt = db.ExecuteReturnQuery(query);

			//Caso encontre os eventos do utilizador inserido
			if (dt.Rows.Count != 0)
			{

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

				jsonString = JsonConvert.SerializeObject(listaEventos);

			}

			else { jsonString = null; }


			Aux aux = new Aux() { Json = jsonString };

			return aux;
		}

		public bool DeleteEvento(string jsonString)
		{

			int numeroLinhas;

			Evento eventoDeserialized = JsonConvert.DeserializeObject<Evento>(jsonString);

			numeroLinhas = db.ExecuteDeleteEvento(eventoDeserialized);

			return true;

		}

		public bool UpdateEvento(string jsonString)
		{

			int numeroLinhas;

			Evento eventoDeserialized = JsonConvert.DeserializeObject<Evento>(jsonString);

			numeroLinhas = db.ExecuteUpdateEvento(eventoDeserialized);

			return true;

		}

	}
	
	public class Evento
	{
		[DataMember]
		public int id { get; set; }

		[JsonProperty(ItemConverterType = typeof(IsoDateTimeConverter))]
		[DataMember]
		public DateTime data { get; set; }

		[DataMember(Name = "dataEvento")]
		private string HiredForSerialization { get; set; }

		[OnSerializing]
		void OnSerializing(StreamingContext ctx)
		{
			this.HiredForSerialization = JsonConvert.SerializeObject(this.data).Replace('"', ' ').Trim();
		}

		/*[OnDeserialized]
		void OnDeserialized(StreamingContext ctx)
		{
			this.data = DateTime.Parse(this.HiredForSerialization);
		}*/

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
