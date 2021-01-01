using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for Evento
/// </summary>
[DataContract]
[KnownType(typeof(Evento))]
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

	[OnDeserialized]
	void OnDeserialized(StreamingContext ctx)
	{
		this.data = DateTime.Parse(this.HiredForSerialization);
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

[DataContract]
[KnownType(typeof(Evento))]
[KnownType(typeof(Eventos))]
public class Eventos
{

	[DataMember]
	public List<Evento> listaEventos { get; set; }

}