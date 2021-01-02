using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente.Forms
{
    public partial class AlterarEvento : Form
    {
        Evento evento = new Evento();
        Menu newMenuForm = new Menu();


        public AlterarEvento()
        {
            InitializeComponent();
        }

        public AlterarEvento(Evento m)
        {
            evento.id = m.id;
            evento.data = m.data;
            evento.descricao = m.descricao;
            evento.titulo = m.titulo;
            evento.id_utilizador = m.id_utilizador;

            InitializeComponent();
        }

        private void AlterarEvento_Load(object sender, EventArgs e)
        {
            textBox1.Text = evento.titulo;
            textBox2.Text = evento.descricao;
            textBox3.Text = evento.data.Hour.ToString();
            textBox4.Text = evento.data.Minute.ToString();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DateTime newData = DateTime.Parse(monthCalendar1.SelectionRange.Start.ToString());

            if (textBox1.Text == evento.titulo && newData == evento.data && evento.descricao == textBox3.Text)
            {

                MessageBox.Show("Nada foi alterado!");


            }

            else
            {

                evento.data = DateTime.Parse(monthCalendar1.SelectionRange.Start.ToString());
                evento.descricao = textBox2.Text;
                evento.titulo = textBox1.Text;
                evento.id = evento.id;
                evento.id_utilizador = evento.id_utilizador;

                string output = JsonConvert.SerializeObject(evento);

                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                string url = "http://localhost:56385/Service.svc/rest/UpdateEvento/?jsonString=" + output;
                HttpClient client = new HttpClient();

                HttpResponseMessage resposta = await client.PutAsync(url, stringContent);

                //Falta tratar a resposta bool que é enviada do serviço (true ou false), o que está feito não funciona
                if (resposta.IsSuccessStatusCode == true)
                {

                    MessageBox.Show("O evento foi alterado com sucesso!");
                    this.Hide();

                    this.Close();

                    newMenuForm.ShowDialog();

                }
                else if (resposta.IsSuccessStatusCode == false) MessageBox.Show("Ocorreu um erro ao alterar o ");

            }
        }
    }
}
