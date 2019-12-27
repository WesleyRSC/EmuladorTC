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
using System.Drawing.Imaging;

namespace EmuladorTC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Conexao.Cliente = new Cliente();
            Conexao.Cliente.Ipserv = ipServidor.Text;
            Conexao.Cliente.Porta = porta.Text;
            Conexao.Cliente.NomeCli = txtNomeCliente.Text;
            Conexao.Cliente.IpCli = ipCliente.Text;
            Conexao.Cliente.MascaraCli = mascaraCliente.Text;
            Conexao.Cliente.GatewayCli = txtGatewayCliente.Text;
            Conexao.Cliente.Texto1 = txtTexto1.Text;
            Conexao.Cliente.Texto2 = txtTexto2.Text;
            Conexao.Cliente.Texto3 = txtTexto3.Text;
            Conexao.Cliente.Texto4 = txtTexto4.Text;
            Conexao.Cliente.TempoExibicao = txtTempoExibicao.Text;
            Conexao.Cliente.DHCP = false;
            Conexao.Cliente.Wifi = false;
            Conexao.Cliente.TempoExibicaoTemp = 0;
            Conexao.Cliente.Mac = txtMac.Text;
            Conexao.Cliente.ModeloTerminal = "#tc406|4.0\0";
            Conexao.Mensagem = "Aguardando...";
            //combobox Modelo equipamento:
            cbModelo.SelectedIndex = 0;

            
        }

        bool isG2 = false;
        Connection Conexao = new Connection();
        const float tamanhofont = 10;

        private void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (Conexao.Conectado == false)
                {
                    //timer1.Start();
                    Conexao.Conectar(ipServidor.Text, int.Parse(porta.Text));
                    botaoConectar.Text = "Desconectar";
                    botaoConectar.BackColor = Color.FromArgb(0, 97, 150);
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
            txtNomeCliente.Text = Conexao.Cliente.NomeCli;
            ipCliente.Text = Conexao.Cliente.IpCli;
            mascaraCliente.Text = Conexao.Cliente.MascaraCli;
            txtGatewayCliente.Text = Conexao.Cliente.GatewayCli;
            txtTempoExibicao.Text = Conexao.Cliente.TempoExibicao;
            txtTexto1.Text = Conexao.Cliente.Texto1;
            txtTexto2.Text = Conexao.Cliente.Texto2;
            txtTexto3.Text = Conexao.Cliente.Texto3;
            txtTexto4.Text = Conexao.Cliente.Texto4;
            txtMac.Text = Conexao.Cliente.Mac;

            if (Conexao.Cliente.Wifi)
            {
                checkBoxWifi.Checked = true;
            }
            else
            {
                checkBoxWifi.Checked = false;
            }

            if (Conexao.Cliente.DHCP)
            {
                rbDhcp.Checked = true;
            }
            else
            {
                rbIpFixo.Checked = true;
            }

            string produto = null;
            string nome = "";
            string preco = "";
            produto = Conexao.Mensagem;
            

            //Exibe a mensagem
            if (Conexao.Cliente.TempoExibicaoTemp > 0)
            {
                txtResultadoConsulta.Text = Conexao.Cliente.Texto1Temp + Environment.NewLine + Conexao.Cliente.Texto2Temp;
                Conexao.Cliente.TempoExibicaoTemp -= 1;

            }
            else
            {

                // if(produto != "#live?" && produto != "" && produto!= null && produto != "aguardando...")
                if (produto.IndexOf("|") >= 0)
                {
                    nome = produto.Substring(1, produto.IndexOf("|") - 1);
                    preco = produto.Substring(produto.IndexOf('|') + 1);
                    if (isG2)
                    {
                        if (nome.Length <= 10)
                        {
                            txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, tamanhofont + 8);
                            txtResultadoConsulta.Text = nome + Environment.NewLine + preco;
                        }
                        else
                        {
                            if (nome.Length>10 && nome.Length<20)
                            {
                                txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, tamanhofont + 2);
                                txtResultadoConsulta.Text = nome + Environment.NewLine + preco;
                            }
                            else
                            {
                                if (nome.Length>60)
                                {
                                    nome=nome.Substring(0, 60);
                                }
                                txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, tamanhofont - 1);
                                txtResultadoConsulta.Text = nome + Environment.NewLine + preco;
                            }

                        }
                    }
                    else
                    {
                        txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, tamanhofont);
                        txtResultadoConsulta.Text = nome + Environment.NewLine + preco;
                    }
                }
                else
                {
                    troca++;
                    if (troca == Convert.ToInt32(Conexao.Cliente.TempoExibicao))
                    {
                        txtResultadoConsulta.Text = Conexao.Cliente.Texto1 + Environment.NewLine + Conexao.Cliente.Texto2;
                        txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, 16);
                    }
                    if (troca == Convert.ToInt32(Conexao.Cliente.TempoExibicao) * 2)
                    {
                        txtResultadoConsulta.Text = Conexao.Cliente.Texto3 + Environment.NewLine + Conexao.Cliente.Texto4;
                        txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, 16);
                        troca = 0;
                    }
                }
            }
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            Conexao.EnviarProduto(txtBuscarProduto.Text);

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
            Conexao.Cliente.GatewayCli = txtGatewayCliente.Text;
        }

        private void rbIpFixo_CheckedChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.DHCP = false;
        }

        private void rbDhcp_CheckedChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.DHCP = true;
        }

        private void txtMac_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.Mac = txtMac.Text;
        }

        private void CbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Conexao.Conectado)
            {
                MessageBox.Show("Desconecte antes de alterar o Modelo");
            }
            else
            {
                if (cbModelo.SelectedIndex == 0)
                {
                    CarregarImagem("buscaprecoG2.jpg");
                    txtBuscarProduto.Location = new Point(122, 385);
                    txtBuscarProduto.Size = new Size(138, 26);
                    txtResultadoConsulta.Location = new Point(123, 260);
                    txtResultadoConsulta.Size = new Size(138, 56);
                    txtResultadoConsulta.BackColor = Color.FromArgb(255, 186, 20);
                    Conexao.Cliente.ModeloTerminal = "#tc406|4.0\0";
                }
                else
                {
                    CarregarImagem("buscapreco.jpg");
                    txtResultadoConsulta.Location = new Point(82, 199);
                    txtResultadoConsulta.Size = new Size(227, 45);
                    txtResultadoConsulta.BackColor = Color.FromArgb(61, 79, 25);
                    txtBuscarProduto.Location = new Point(132, 340);
                    txtBuscarProduto.Size = new Size(126, 26);
                    Conexao.Cliente.ModeloTerminal = "#tc502|4.0\0";
                }
            }        

            if (cbModelo.SelectedIndex == 0)
            {
                isG2 = true;
                CarregarImagem("buscaprecoG2.jpg");
                txtBuscarProduto.Location = new Point(122, 385);
                txtBuscarProduto.Size = new Size(138, 26);
                txtResultadoConsulta.Location = new Point(123, 260);
                txtResultadoConsulta.Size = new Size(138, 56);
                txtResultadoConsulta.BackColor = Color.FromArgb(255, 186, 20);

            }
            else
            {
                isG2 = false;
                CarregarImagem("buscapreco.jpg");
                txtResultadoConsulta.Location = new Point(82, 199);
                txtResultadoConsulta.Size = new Size(227, 45);
                txtResultadoConsulta.BackColor = Color.FromArgb(61, 79, 25);
                txtBuscarProduto.Location = new Point(132, 340);
                txtBuscarProduto.Size = new Size(126, 26);

            }
        }
        private void CarregarImagem(string nomeImagem)
        {
            string diretorioAtual = Directory.GetCurrentDirectory();

            string pastaRaiz = diretorioAtual + @"\Imagens\";

            var caminhoImagem = Path.Combine(pastaRaiz, nomeImagem);
            pbImagemG2.Image = Image.FromFile(caminhoImagem);
        }


        private void checkBoxWifi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWifi.Checked)
            {
                Conexao.Cliente.Wifi = true;
            }
            else
            {
                Conexao.Cliente.Wifi = false;
            }
		}
        private void txtResultadoConsulta_TextChanged(object sender, EventArgs e)
        {


        }

        private void nomeCliente_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.NomeCli = txtNomeCliente.Text;
        }

        private void txtDebug_TextChanged(object sender, EventArgs e)
        {
            txtDebug.Text = Conexao.Mensagem;
        }
    }
}
