using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IServiceRest
{

	/// <summary>
	/// Retornas os nomes e os idGlobal's das cidades da API do IPMA
	/// </summary>
	/// <returns></returns>
	[OperationContract]
	[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
	Dictionary<int, string> RetornaCidades();

}