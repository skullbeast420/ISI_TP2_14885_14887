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
    public partial class AdicionarEvento : Form
    {
        Menu newMenuForm = new Menu();
        Evento evento = new Evento();
        
        public AdicionarEvento()
        {
            InitializeComponent();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void AdicionarEvento_Load(object sender, EventArgs e)
        {
            monthCalendar1.MinDate = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            newMenuForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Botão para adicionar um novo evento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text)) MessageBox.Show("Um ou mais campos não foram preenchidos!");

            else
            {

                evento.titulo = textBox1.Text;
                evento.descricao = textBox2.Text;
                evento.data = DateTime.Parse(monthCalendar1.SelectionRange.Start.ToString());
                evento.data = new DateTime(evento.data.Year, evento.data.Month, evento.data.Day, 0, 0, 0);
                evento.data.Date.AddHours(evento.data.TimeOfDay.Hours);
                evento.data.Date.AddMinutes(evento.data.TimeOfDay.Minutes);
                
                evento.id_utilizador = Form1.test.currentUser.id;

                string output = JsonConvert.SerializeObject(evento);

                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                string url = "http://localhost:56385/Service.svc/rest/AddEvento/?jsonString=" + output;
                HttpClient client = new HttpClient();

                HttpResponseMessage resposta = await client.PostAsync(url, stringContent);

                //Falta tratar a resposta bool que é enviada do serviço (true ou false), o que está feito não funciona
                if (resposta.IsSuccessStatusCode == true)
                {

                    MessageBox.Show("O evento foi adicionado à sua agenda com sucesso!");

                    this.Hide();
                    newMenuForm.ShowDialog();
                    this.Close();

                }
                else if (resposta.IsSuccessStatusCode == false) MessageBox.Show("Já existe um evento registado por si com os parâmetros introduzidos!");

            }

        }
    }
}
