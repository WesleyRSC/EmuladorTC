using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace EmuladorTC
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Connection Conexao = new Connection();      

        private void Button1_Click(object sender, EventArgs e)
        {            
            try
            {
                if (Conexao.Conectado==false)
                {
                    timer1.Start();
                    Conexao.Connect(ipServidor.Text, int.Parse(porta.Text));
                    botaoConectar.Text = "Desconectar";
                }
                else
                {
                    timer1.Stop();
                    Conexao.Disconnect();
                    botaoConectar.Text = "Conectar";
                }

            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Conexao.DadosCliente(ipServidor.Text, porta.Text, nomeCliente.Text, ipCliente.Text, mascaraCliente.Text, 
                gatewayCliente.Text, txtTexto1.Text, txtTexto2.Text,txtTexto3.Text,txtTexto4.Text,txtTempoExibicao.Text);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {            
            string produto;
            produto = Conexao.EnviarProduto(entradaProduto.Text);
            txtResultadoConsulta.Text = produto;
        }
    }
}
