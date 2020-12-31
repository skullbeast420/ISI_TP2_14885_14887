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

        public Utilizador currentUser = new Utilizador();
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
            MemoryStream copyStream;
            Utilizadores listaAux = new Utilizadores();
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

                copyStream = new MemoryStream();
                response.GetResponseStream().CopyTo(copyStream);
                jsonSerializer = new DataContractJsonSerializer(typeof(Utilizador));
                copyStream.Position = 0L;

                resposta = (Utilizador)jsonSerializer.ReadObject(copyStream);

                //currentUser = (Utilizador)jsonSerializer.ReadObject(response.GetResponseStream());
                //currentUser = (Utilizador)jsonSerializer.ReadObject(copyStream);

                Console.WriteLine("debug");

                //ou
                //Root myclass = (Root)jsonSerializer.ReadObject(response.GetResponseStream());

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
