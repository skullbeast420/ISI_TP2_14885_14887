using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_ISI.Models
{
	public class IpmaMetodos
	{

		/// <summary>
		/// Acede à API do IPMA para obter os nomes das cidades e os seus idglobal's
		/// </summary>
		/// <returns>
		/// locais- dictionary com os id's e nomes das cidades
		/// </returns>
		public Locais RetornaCidades()
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

			return listaLocais;
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

	}


	/// <summary>
	/// Summary description for IPMA
	/// </summary>
	public class Locais
	{

		public string owner { get; set; }
		public string country { get; set; }
		public List<Local> data { get; set; }

	}

	public class Local
	{
		public int idRegiao { get; set; }
		public string idAreaAviso { get; set; }
		public int idConcelho { get; set; }
		public int globalIdLocal { get; set; }
		public string latitude { get; set; }
		public int idDistrito { get; set; }
		public string local { get; set; }
		public string longitude { get; set; }
	}

	public class Tempo
	{
		public string descIdWeatherTypeEN { get; set; }
		public string descIdWeatherTypePT { get; set; }
		public int idWeatherType { get; set; }
	}

	public class TiposTempo
	{
		public string owner { get; set; }
		public string country { get; set; }
		public List<Tempo> data { get; set; }
	}

	public class PrevisaoDia
	{
		public string precipitaProb { get; set; }
		public string tMin { get; set; }
		public string tMax { get; set; }
		public string predWindDir { get; set; }
		public int idWeatherType { get; set; }
		public int classWindSpeed { get; set; }
		public string longitude { get; set; }
		public string forecastDate { get; set; }
		public int classPrecInt { get; set; }
		public string latitude { get; set; }
	}

	[DataContract]
	public class Previsao5dias
	{

		[DataMember]
		public string owner { get; set; }

		[DataMember]
		public string country { get; set; }

		[DataMember]
		public List<PrevisaoDia> data { get; set; }

		[DataMember]
		public int globalIdLocal { get; set; }

		[JsonProperty(ItemConverterType = typeof(IsoDateTimeConverter))]
		public DateTime dataUpdate { get; set; }

		[DataMember(Name = "dataUpdate")]
		private string HiredForSerialization { get; set; }

		[OnSerializing]
		void OnSerializing(StreamingContext ctx)
		{
			this.HiredForSerialization = JsonConvert.SerializeObject(this.dataUpdate).Replace('"', ' ').Trim();
		}

		[OnDeserialized]
		void OnDeserialized(StreamingContext ctx)
		{
			this.dataUpdate = DateTime.Parse(this.HiredForSerialization);
		}
	}
}
