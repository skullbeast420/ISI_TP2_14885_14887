using System;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

using System.Text.Json.Serialization;
using System.Text.Json;

namespace Cliente
{
    public partial class Form1 : Form
    {

        public static teste test = new teste();

        Cliente.Forms.Menu novoForm = new Cliente.Forms.Menu();

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

            Utilizadores listaAux = new Utilizadores();
            DataContractJsonSerializer jsonSerializer;

            StringBuilder uri = new StringBuilder();
            uri.Append("http://localhost:56385/Service.svc/rest/");
            //uri.Append("Login/" + textBox1.Text + "/" + textBox2.Text);
            uri.Append("Login/" + textBox1.Text + "/" + textBox2.Text);

            HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            //h2
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string json;

            using (var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }
            Aux aux = JsonSerializer.Deserialize<Aux>(json);

            if (aux.Json == null)
            {

                MessageBox.Show("Os dados introduzidos estão incorretos. Tente Novamente!");

            }

            else
            {

                test.currentUser = JsonSerializer.Deserialize<Utilizador>(aux.Json);
                MessageBox.Show("Login efetuado com sucesso!");
                this.Hide();
                novoForm.ShowDialog();
                this.Close();

            }

            response.Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
