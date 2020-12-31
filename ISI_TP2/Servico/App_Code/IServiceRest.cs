﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IServiceRest
{

	/// <summary>
	/// Retornas os nomes e os idGlobal's das cidades da API do IPMA
	/// </summary>
	/// <returns></returns>
	[OperationContract]
	[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate ="RetornaCidades")]
	Dictionary<int, string> RetornaCidades();

	[OperationContract]
	[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWeatherTypes")]
	TiposTempo GetWeatherTypes();

	[OperationContract]
	[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Get5DayWeather/{idCidade}")]
	Previsao5dias Get5DayWeather(string idCidade);

	[OperationContract]
	[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate ="Login/{email}/{password}")]
	string Login(string email, string password);

	[OperationContract]
	[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Registo?jsonString={jsonString}")]
	Task<bool> Registo(string jsonString);

}