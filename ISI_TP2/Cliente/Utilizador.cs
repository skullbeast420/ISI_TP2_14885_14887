using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class Utilizador
    {

		public int id { get; set; }

		public string nome { get; set; }

		public string cidade { get; set; }

		public int id_cidade { get; set; }

		public string email { get; set; }

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
}
