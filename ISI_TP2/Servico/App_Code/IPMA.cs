using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

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