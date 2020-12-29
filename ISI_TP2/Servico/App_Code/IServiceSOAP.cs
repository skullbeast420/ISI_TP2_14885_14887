using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;

/// <summary>
/// Summary description for IServiceSOAP
/// </summary>

[ServiceContract]
public interface IServiceSoap
{

    [OperationContract]
    bool Registo(string email);

}