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

namespace Cliente
{
    public partial class Form1 : Form
    {

        Utilizador currentUser = new Utilizador();
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

            dt = WCFapi.Login(textBox1.Text, textBox2.Text);

            if (dt.Rows.Count == 0)
            {

                MessageBox.Show("Os dados estão incorretos. O login foi mal sucedido!");

            }

            else
            {

                DataRow row = dt.Rows[0];

                currentUser.id = row.Field<int>(dt.Columns[0]);
                currentUser.nome = row.Field<string>(dt.Columns[1]);
                currentUser.cidade = row.Field<string>(dt.Columns[2]);
                currentUser.idCidade = row.Field<int>(dt.Columns[3]);
                currentUser.email = row.Field<string>(dt.Columns[4]);
                currentUser.password = row.Field<string>(dt.Columns[5]);

                MessageBox.Show("Login efetuado com sucesso!");

            }

        }
    }
}
