using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    [DataContract]
	[KnownType(typeof(Utilizador))]
	public class Utilizador
    {

		[DataMember]
		public int id { get; set; }

		[DataMember]
		public string nome { get; set; }

		[DataMember]
		public string cidade { get; set; }

		[DataMember]
		public int id_cidade { get; set; }

		[DataMember]
		public string email { get; set; }

		[DataMember]
		public string password { get; set; }

		public Utilizador()
		{

			id = default(int);
			nome = default(string);
			cidade = default(string);
			id_cidade = default(int);
			email = default(string);
			password = default(string);

		}

		public Utilizador(int Id, string Nome, string Cidade, int IDCidade, string Email, string Password)
		{

			id = Id;
			nome = Nome;
			cidade = Cidade;
			id_cidade = IDCidade;
			email = Email;
			password = Password;

		}

	}

	[DataContract]
	[KnownType(typeof(Utilizador))]
	[KnownType(typeof(Utilizadores))]
	public class Utilizadores
    {

		[DataMember]
		public List<Utilizador> utilizadores { get; set; }

    }

	public class Aux
	{
		public string Json { get; set; }

	}

}
