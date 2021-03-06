﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente.Forms
{
    public partial class Registo : Form
    {
        Locais listaLocais = new Locais();

        Form1 newForm = new Form1();

        public Registo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Botão de registo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void button1_ClickAsync(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)
                || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrWhiteSpace(comboBox1.Text)) MessageBox.Show("Um ou mais campos não foram preenchidos!");

            else
            {

                Form1.test.currentUser.cidade = comboBox1.Text;

                foreach (Local place in listaLocais.data)
                {

                    //if (kvp.Value == Form1.test.currentUser.cidade) Form1.test.currentUser.id_cidade = kvp.Key;

                    if (string.Equals(place.local, comboBox1.Text)) Form1.test.currentUser.id_cidade = place.globalIdLocal;

                }

                Form1.test.currentUser.nome = textBox1.Text + " " + textBox2.Text;
                Form1.test.currentUser.email = textBox3.Text;
                Form1.test.currentUser.password = textBox4.Text;

                string output = JsonConvert.SerializeObject(Form1.test.currentUser);

                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");
           
                string url = "https://isitp2-apim.azure-api.net/api/Utilizador/Registo/" + output;

                HttpClient client = new HttpClient();

                HttpResponseMessage resposta = await client.PostAsync(url, stringContent);
                
                //Falta tratar a resposta bool que é enviada do serviço (true ou false), o que está feito não funciona
                if (resposta.IsSuccessStatusCode == true)
                {

                    MessageBox.Show("O registo foi efetuado com sucesso! Faça o login com as suas credenciais para aceder à sua Agenda!");
                    this.Hide();
                    newForm.ShowDialog();
                    this.Close();

                }
                else if(resposta.IsSuccessStatusCode == false) { MessageBox.Show("Já existe um utilizador registado com o E-Mail introduzido. Introduza um E-Mail diferente!"); Form1.test.currentUser = new Utilizador(); }

            }

        }

        private void Registo_Load(object sender, EventArgs e)
        {

            object resposta;
            DataContractJsonSerializer jsonSerializer;
            StringBuilder uri = new StringBuilder();
            uri.Append("https://isitp2-apim.azure-api.net/api/IPMA/RetornaCidades");

            HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Verificar se não está disponível
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                //Serializa de JSON para Objecto
                jsonSerializer = new DataContractJsonSerializer(typeof(Locais));
                resposta = jsonSerializer.ReadObject(response.GetResponseStream());
                listaLocais = (Locais)resposta;
            }

            //listaLocais = WCFapi.RetornaCidades();
            
            foreach(Local place in listaLocais.data)
            {

                comboBox1.Items.Add(place.local);

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        /// <summary>
        /// Botão para retornar ao menu de login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            Form1.test.currentUser = new Utilizador();

            this.Hide();
            newForm.ShowDialog();
            this.Close();

        }
    }
}
