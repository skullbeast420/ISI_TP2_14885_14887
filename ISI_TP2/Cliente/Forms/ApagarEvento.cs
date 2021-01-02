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
    public partial class ApagarEvento : Form
    {
        Evento evento = new Evento();
        Menu newMenuForm = new Menu();
        
        public ApagarEvento()
        {
            InitializeComponent();
        }

        public ApagarEvento(Evento m)
        {
            evento.id = m.id;
            evento.data = m.data;
            evento.descricao = m.descricao;
            evento.titulo = m.titulo;
            evento.id_utilizador = m.id_utilizador;

            InitializeComponent();
        }

        private void ApagarEvento_Load(object sender, EventArgs e)
        {
            label1.Text = "Título do evento: " + evento.titulo;
            label2.Text = "Data do evento: " + evento.data.ToString();
            label3.Text = "Descrição do evento: " + evento.descricao;
        }

        /// <summary>
        /// Botão para sair
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            newMenuForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Botão para apagar o evento requerido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            string output = JsonConvert.SerializeObject(evento);

            string url = "http://localhost:56385/Service.svc/rest/DeleteEvento/?jsonString=" + output;
            HttpClient client = new HttpClient();

            HttpResponseMessage resposta = await client.DeleteAsync(url);

            //Falta tratar a resposta bool que é enviada do serviço (true ou false), o que está feito não funciona
            if (resposta.IsSuccessStatusCode == true)
            {

                MessageBox.Show("O evento foi eliminado com sucesso!");
                this.Hide();
                newMenuForm.ShowDialog();
                this.Close();

            }
            else if (resposta.IsSuccessStatusCode == false) MessageBox.Show("Ocorreu um erro ao eliminar o evento!");
        }
    }
}
