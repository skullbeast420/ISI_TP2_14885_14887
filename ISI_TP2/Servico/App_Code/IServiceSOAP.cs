using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data;
using System.ServiceModel.Web;
using System.Text;

/// <summary>
/// Summary description for IServiceSOAP
/// </summary>
public class IServiceSOAP
{
    
    public IServiceSOAP()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

[ServiceContract]
public interface IServiceSoap
{

    [OperationContract]
    DataTable Login(string email, string password);

    [OperationContract]
    bool Registo(string email);

}