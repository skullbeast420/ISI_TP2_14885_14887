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
    public partial class SelectEvento : Form
    {
        Menu novoMenuForm = new Menu();
        List<Evento> listaEventos = new List<Evento>();
        int identifier;

        Evento evento = new Evento();
        
        public SelectEvento()
        {
            InitializeComponent();
        }

        public SelectEvento(List<Evento> eventList,int identificador)
        {

            listaEventos = eventList.ToList();
            identifier = identificador;
            
            InitializeComponent();

        }

        /// <summary>
        /// Botão para prosseguir depois de introduzir o ID do evento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Pesquisa na lista de eventos se existe o evento com o id introduzio
            foreach(Evento m in listaEventos)
            {

                //Caso exista faz a tribuição daas variáveis do evento para um novo evento criado em cima neste form
                if (textBox1.Text == m.id.ToString())
                {

                    evento.id = m.id;
                    evento.data = m.data;
                    evento.descricao = m.descricao;
                    evento.titulo = m.titulo;
                    evento.id_utilizador = m.id_utilizador;

                }

            }

            //Caso o evento não exista, mostra uma mensagem de erro
            if (evento.id == default(int)) { MessageBox.Show("O ID que introduziu ou não é correspondente a um evento criado por si, ou não existe ou não é viável. Tente novamente!"); }

            //Caso o evento exista
            else
            {

                //Se for para ALTERAR evento
                if (identifier == 1)
                {

                    //Cria um novo form 'AlterarEvento' com o evento desejado a ser enviado como parâmetro
                    AlterarEvento newAlterarEvento = new AlterarEvento(evento);
                    this.Hide();
                    newAlterarEvento.ShowDialog();
                    this.Close();

                }

                //Se for para APAGAR evento
                else if (identifier == 2)
                {

                    //Cria um novo form 'ApagarEvento' com o evento desejado a ser enviado como parâmetro
                    ApagarEvento newApagarEvento = new ApagarEvento(evento);
                    this.Hide();
                    newApagarEvento.ShowDialog();
                    this.Close();

                }

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Botão para sair e voltar ao menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            novoMenuForm.ShowDialog();
            this.Close();

        }
    }
}
