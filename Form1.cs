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
            Conexao.Cliente = new Cliente();
            Conexao.Cliente.Ipserv = ipServidor.Text;
            Conexao.Cliente.Texto1 = txtTexto1.Text;
            Conexao.Cliente.Porta = porta.Text;
            Conexao.Cliente.NomeCli = nomeCliente.Text;
            Conexao.Cliente.IpCli = ipCliente.Text;
            Conexao.Cliente.MascaraCli = mascaraCliente.Text;
            Conexao.Cliente.Texto2 = txtTexto2.Text;
            Conexao.Cliente.Texto3 = txtTexto3.Text;
            Conexao.Cliente.Texto4 = txtTexto4.Text;
            Conexao.Cliente.TempoExibicao = txtTempoExibicao.Text;
            Conexao.Cliente.DHCP = false;
        }

        Connection Conexao = new Connection();

        private void Button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (Conexao.Conectado == false)
                {
                    //timer1.Start();
                    Conexao.Conectar(ipServidor.Text, int.Parse(porta.Text));
                    botaoConectar.Text = "Desconectar";
                    botaoConectar.BackColor = Color.FromArgb(0,  97,  150);
                }
                else
                {
                    //timer1.Stop();
                    Conexao.Desconectar();
                    botaoConectar.Text = "Conectar"; 
                    botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
                }

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
        }
        int troca = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            ipServidor.Text = Conexao.Cliente.Ipserv;
            porta.Text = Conexao.Cliente.Porta;
            nomeCliente.Text = Conexao.Cliente.NomeCli;
            ipCliente.Text = Conexao.Cliente.IpCli;
            mascaraCliente.Text = Conexao.Cliente.MascaraCli;
            gatewayCliente.Text = Conexao.Cliente.GatewayCli;
            txtTexto1.Text = Conexao.Cliente.Texto1;
            txtTexto2.Text = Conexao.Cliente.Texto2;
            txtTexto3.Text = Conexao.Cliente.Texto3;
            txtTexto4.Text = Conexao.Cliente.Texto4;
            txtTempoExibicao.Text = Conexao.Cliente.TempoExibicao;

            if (Conexao.Cliente.DHCP)
            {
                rbDhcp.Checked = true;
            }
            else
            {
                rbIpFixo.Checked = true;
            }
            
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
            else
            {
                troca++;
                if (troca == Convert.ToInt32(Conexao.Cliente.TempoExibicao))
                {
                    txtResultadoConsulta.Text = Conexao.Cliente.Texto1 + Environment.NewLine + Conexao.Cliente.Texto2;
                }
                if (troca == Convert.ToInt32(Conexao.Cliente.TempoExibicao)*2)
                {
                    txtResultadoConsulta.Text = Conexao.Cliente.Texto3 + Environment.NewLine + Conexao.Cliente.Texto4;
                    troca = 0;
                }
            } 
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            Conexao.EnviarProduto(entradaProduto.Text);

        }
        private void txtTempoExibicao_TextChanged(object sender, EventArgs e)
        {
            troca = 0;
            Conexao.Cliente.TempoExibicao = txtTempoExibicao.Text;
        }
        private void ipServidor_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.Ipserv = ipServidor.Text;
        }
        private void txtTexto1_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.Texto1 = txtTexto1.Text;
        }
        private void porta_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.Porta = porta.Text;
        }
        private void ipCliente_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.IpCli = ipCliente.Text;
        }
        private void mascaraCliente_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.MascaraCli = mascaraCliente.Text;
        }
        private void txtTexto2_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.Texto2 = txtTexto2.Text;
        }
        private void txtTexto3_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.Texto3 = txtTexto3.Text;
        }
        private void txtTexto4_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.Texto4 = txtTexto4.Text;
        }
        private void gatewayCliente_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.GatewayCli = gatewayCliente.Text;
        }

        private void rbIpFixo_CheckedChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.DHCP = false;
        }

        private void rbDhcp_CheckedChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.DHCP = true;
        }
    }
}
