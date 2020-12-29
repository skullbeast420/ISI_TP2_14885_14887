using System;
using System.Data;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IO;

namespace Cliente
{
    public partial class Form1 : Form
    {

        Utilizador currentUser;
        WCF.ServiceSoapClient WCFapi = new WCF.ServiceSoapClient("SOAPEndpoint");
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cliente.Forms.Registo novoRegisto = new Cliente.Forms.Registo();
            this.Hide();
            novoRegisto.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Executa o login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            object resposta;
            string info;
            Utilizador userAux = new Utilizador();
            DataContractJsonSerializer jsonSerializer;
            StringBuilder uri = new StringBuilder();
            uri.Append("http://localhost:56385/Service.svc/rest/");
            uri.Append("Login/" + textBox1.Text + "/" + textBox2.Text);


            HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Verificar se não está disponível
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                jsonSerializer = new DataContractJsonSerializer(typeof(Utilizador));
                resposta = jsonSerializer.ReadObject(response.GetResponseStream());
                currentUser = (Utilizador)resposta;

            }

            /*if (dt.Rows.Count == 0)
            {

                MessageBox.Show("Os dados estão incorretos. O login foi mal sucedido!");

            }

            else
            {

                DataRow row = dt.Rows[0];

                currentUser.id = row.Field<int>(dt.Columns[0]);
                currentUser.nome = row.Field<string>(dt.Columns[1]);
                currentUser.cidade = row.Field<string>(dt.Columns[2]);
                currentUser.id_cidade = row.Field<int>(dt.Columns[3]);
                currentUser.email = row.Field<string>(dt.Columns[4]);
                currentUser.password = row.Field<string>(dt.Columns[5]);

                MessageBox.Show("Login efetuado com sucesso!");

            }*/

        }
    }
}
