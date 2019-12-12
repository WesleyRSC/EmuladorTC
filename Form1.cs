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
        Cliente cliente = new Cliente();

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Conexao.Conectado == false)
                {
                    timer1.Start();
                    Conexao.Conectar(ipServidor.Text, int.Parse(porta.Text));
                    botaoConectar.Text = "Desconectar";
                }
                else
                {
                    timer1.Stop();
                    Conexao.Desconectar();
                    botaoConectar.Text = "Conectar";
                }

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cliente.Ipserv = ipServidor.Text;
            cliente.Porta = porta.Text;
            cliente.NomeCli = nomeCliente.Text;
            cliente.IpCli = ipCliente.Text;
            cliente.MascaraCli = mascaraCliente.Text;
            cliente.GatewayCli = gatewayCliente.Text;
            cliente.Texto1 = txtTexto1.Text;
            cliente.Texto2 = txtTexto2.Text;
            cliente.Texto3 = txtTexto3.Text;
            cliente.Texto4 = txtTexto4.Text;
            cliente.TempoExibicao = txtTempoExibicao.Text;

            string produto = null;
            string nome="";
            string preco="";
            produto = Conexao.RetornarProduto();

            // if(produto != "#live?" && produto != "" && produto!= null && produto != "aguardando...")
            if (produto.IndexOf("|") >= 0)
            {
                nome=produto.Substring(1,produto.IndexOf("|")-1);
                preco = produto.Substring(produto.IndexOf('|')+1);
                txtResultadoConsulta.Text = nome+Environment.NewLine+preco;
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Conexao.EnviarProduto(entradaProduto.Text);

        }
    }
}
