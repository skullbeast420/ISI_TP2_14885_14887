using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            listaLocais = WCFapi.RetornaCidades();
            
            foreach(KeyValuePair<int, string> kvp in listaLocais)
            {

                comboBox1.Items.Add(kvp.Value);

            }

        }
    }
}
