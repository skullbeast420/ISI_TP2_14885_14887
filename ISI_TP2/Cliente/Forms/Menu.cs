using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Cliente.Forms
{
    public partial class Menu : Form
    {
        Previsao5dias previsao5dias = new Previsao5dias();
        TiposTempo weatherTypes = new TiposTempo();
        public List<Evento> listaEventos = new List<Evento>();
        static AdicionarEvento newAdicionarEvento = new AdicionarEvento();
        static Form1 newForm = new Form1();

        Evento evento = new Evento();

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {

            label2.Text = "Previsão Meteorológica dos próximos 5 dias para a cidade " + Form1.test.currentUser.cidade + ":";

            #region Pedidos aos serviços

            object resposta;
            DataContractJsonSerializer jsonSerializer;
            HttpWebRequest request;
            HttpWebResponse response;
            StringBuilder uri = new StringBuilder();
            uri.Append("http://localhost:56385/Service.svc/rest/GetWeatherTypes");

            request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            using (response = request.GetResponse() as HttpWebResponse)
            {
                // Verificar se não está disponível
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                //Serializa de JSON para Objecto
                jsonSerializer = new DataContractJsonSerializer(typeof(TiposTempo));
                resposta = jsonSerializer.ReadObject(response.GetResponseStream());
                weatherTypes = (TiposTempo)resposta;
            }

            //Nova uri para aceder ao serviço da previsão para os próximos 5 dias
            uri = new StringBuilder();
            uri.Append("http://localhost:56385/Service.svc/rest/Get5DayWeather/");
            uri.Append(Form1.test.currentUser.id_cidade.ToString());

            request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            using (response = request.GetResponse() as HttpWebResponse)
            {
                // Verificar se não está disponível
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                //Serializa de JSON para Objecto
                jsonSerializer = new DataContractJsonSerializer(typeof(Previsao5dias));
                resposta = jsonSerializer.ReadObject(response.GetResponseStream());
                previsao5dias = (Previsao5dias)resposta;
            }


            //Nova uri para aceder aos eventos do utilizador
            uri = new StringBuilder();
            uri.Append("http://localhost:56385/Service.svc/rest/GetEventos/");
            uri.Append(Form1.test.currentUser.id.ToString());

            request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

            response = (HttpWebResponse)request.GetResponse();
            string json;

            using (var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }
            Aux aux = JsonSerializer.Deserialize<Aux>(json);

            if (aux.Json != null)
            {

                listaEventos = JsonSerializer.Deserialize<List<Evento>>(aux.Json);
                foreach (Evento evento in listaEventos)
                {
                    if(evento.data >= DateTime.Now)
                    {

                        string[] row = new string[] { evento.id.ToString() ,evento.data.ToString(), evento.titulo.ToString(), evento.descricao.ToString() };
                        dataGridView1.Rows.Add(row);

                    }

                }
                
            }

            #endregion

            PrimeiroDia();
            SegundoDia();
            TerceiroDia();
            QuartoDia();
            QuintoDia();

        }

        #region Tratamento dos dados para a previsão meteorológica

        void PrimeiroDia()
        {

            label3.Text = previsao5dias.data[0].forecastDate;

            foreach (Tempo m in weatherTypes.data)
            {

                if (previsao5dias.data[0].idWeatherType == m.idWeatherType) label5.Text = m.descIdWeatherTypePT;

            }

            textBox1.Text = previsao5dias.data[0].tMin + "º";
            textBox2.Text = previsao5dias.data[0].tMax + "º";

            label4.Text = "Vento:" + previsao5dias.data[0].predWindDir;
            label6.Text = "% de Chuva:" + previsao5dias.data[0].precipitaProb + "%";

        }

        void SegundoDia()
        {

            label10.Text = previsao5dias.data[1].forecastDate;

            foreach (Tempo m in weatherTypes.data)
            {

                if (previsao5dias.data[1].idWeatherType == m.idWeatherType) label8.Text = m.descIdWeatherTypePT;

            }

            textBox4.Text = previsao5dias.data[1].tMin + "º";
            textBox3.Text = previsao5dias.data[1].tMax + "º";

            label9.Text = "Vento:" + previsao5dias.data[1].predWindDir;
            label7.Text = "% de Chuva:" + previsao5dias.data[1].precipitaProb + "%";

        }

        void TerceiroDia()
        {

            label14.Text = previsao5dias.data[2].forecastDate;

            foreach (Tempo m in weatherTypes.data)
            {

                if (previsao5dias.data[2].idWeatherType == m.idWeatherType) label12.Text = m.descIdWeatherTypePT;

            }

            textBox6.Text = previsao5dias.data[2].tMin + "º";
            textBox5.Text = previsao5dias.data[2].tMax + "º";

            label13.Text = "Vento:" + previsao5dias.data[2].predWindDir;
            label11.Text = "% de Chuva:" + previsao5dias.data[2].precipitaProb + "%";

        }

        void QuartoDia()
        {

            label18.Text = previsao5dias.data[3].forecastDate;

            foreach (Tempo m in weatherTypes.data)
            {

                if (previsao5dias.data[3].idWeatherType == m.idWeatherType) label16.Text = m.descIdWeatherTypePT;

            }

            textBox8.Text = previsao5dias.data[3].tMin + "º";
            textBox7.Text = previsao5dias.data[3].tMax + "º";

            label17.Text = "Vento:" + previsao5dias.data[3].predWindDir;
            label15.Text = "% de Chuva:" + previsao5dias.data[3].precipitaProb + "%";

        }

        void QuintoDia()
        {

            label22.Text = previsao5dias.data[4].forecastDate;

            foreach (Tempo m in weatherTypes.data)
            {

                if (previsao5dias.data[4].idWeatherType == m.idWeatherType) label20.Text = m.descIdWeatherTypePT;

            }

            textBox10.Text = previsao5dias.data[4].tMin + "º";
            textBox9.Text = previsao5dias.data[4].tMax + "º";

            label21.Text = "Vento:" + previsao5dias.data[4].predWindDir;
            label19.Text = "% de Chuva:" + previsao5dias.data[4].precipitaProb + "%";

        }

        #endregion

        /// <summary>
        /// Botão para adicionar evento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            newAdicionarEvento.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Botão para alterar evento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (listaEventos.Count == 0) { MessageBox.Show("Não foram encontrados eventos criados por si! Crie um evento primeiro!"); }

            else
            {

                //Envia 1 como parâmetro para o SelectForm saber que tem de abrir o form para ALTERAR o evento
                SelectEvento newSelectEvento = new SelectEvento(listaEventos, 1);

                this.Hide();
                newSelectEvento.ShowDialog();
                this.Close();

            }
        }

        /// <summary>
        /// Botão para remover evento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            if (listaEventos.Count == 0) { MessageBox.Show("Não foram encontrados eventos criados por si! Crie um evento primeiro!"); }

            else
            {

                //Envia 2 como parâmetro para o SelectForm saber que tem de abrir o form para APAGAR o evento
                SelectEvento newSelectEvento = new SelectEvento(listaEventos, 2);

                this.Hide();
                newSelectEvento.ShowDialog();
                this.Close();

            }

        }

        /// <summary>
        /// Botão para sair
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Deseja sair da aplicação?", "Logout", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Form1.test.currentUser = new Utilizador();
                
                this.Hide();
                newForm.ShowDialog();
                this.Close();

            }

        }
    }
}
