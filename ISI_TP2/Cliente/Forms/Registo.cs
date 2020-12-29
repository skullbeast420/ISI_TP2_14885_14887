using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente.Forms
{
    public partial class Registo : Form
    {
        Dictionary<int, string> listaLocais = new Dictionary<int, string>();
        WCF.ServiceRestClient WCFapi = new WCF.ServiceRestClient();

        public Registo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Botão de registo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Registo_Load(object sender, EventArgs e)
        {

            object resposta;
            DataContractJsonSerializer jsonSerializer;
            StringBuilder uri = new StringBuilder();
            uri.Append("http://localhost:56385/Service.svc/rest/RetornaCidades");

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
                jsonSerializer = new DataContractJsonSerializer(typeof(Dictionary<int, string>));
                resposta = jsonSerializer.ReadObject(response.GetResponseStream());
                listaLocais = (Dictionary<int, string>)resposta;
            }

            //listaLocais = WCFapi.RetornaCidades();
            
            foreach(KeyValuePair<int, string> kvp in listaLocais)
            {

                comboBox1.Items.Add(kvp.Value);

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
