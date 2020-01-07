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
        public Thread CheckDebug;
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
        int tempoProduto = 0;
        string nome = "";
        string preco = "";
        int tempoExibicao = 0;

        [Obsolete]
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Conexao.Conectado)
                {
                    CheckDebug = new Thread(ImprimirDebug);
                    CheckDebug.Start();
                    Conexao.Conectar(ipServidor.Text, int.Parse(porta.Text));
                    botaoConectar.Text = "Desconectar";
                    botaoConectar.BackColor = Color.FromArgb(0, 97, 150);
                    timer1.Start();
                }
                else
                {
                    Conexao.Desconectar();
                    CheckDebug.Abort();
                    botaoConectar.Text = "Conectar";
                    botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
                    timer1.Stop();
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
            Int32.TryParse(Conexao.Cliente.TempoExibicao, out tempoExibicao);

            if (!Conexao.Conectado)
            {
                botaoConectar.Text = "Conectar";
                botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
            }
            else
            {
                botaoConectar.Text = "Desconectar";
                botaoConectar.BackColor = Color.FromArgb(0, 97, 150);
            }

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
            produto = Conexao.Mensagem;

            //Exibe a mensagem
            if (Conexao.Cliente.TempoExibicaoTemp > 0)
            {
                txtResultadoConsulta.Text = Conexao.Cliente.Texto1Temp;
                MudarObj(txtResultadoConsulta);
                txtResultadoConsulta2.Text = Conexao.Cliente.Texto2Temp;
                MudarObj(txtResultadoConsulta2);
                Conexao.Cliente.TempoExibicaoTemp--;

            }
            else
            {

                if (produto.IndexOf("|") >= 0 || tempoProduto > 0 || produto == "#nfound")
                {
                    if (tempoProduto <= 0)
                    {
                        tempoProduto = tempoExibicao;
                        if (produto.IndexOf("|") >= 0)
                        {
                            nome = produto.Substring(1, produto.IndexOf("|") - 1);
                            preco = produto.Substring(produto.IndexOf("|") + 1);
                        }
                        if (produto == "#nfound")
                        {
                            nome = "Produto";
                            preco = "não encontrado";
                        }
                    }
                    tempoProduto--;
                    if (isG2)
                    {
                        txtResultadoConsulta.Text = nome;
                        txtResultadoConsulta2.Text = preco;
                        MudarObj(txtResultadoConsulta,txtResultadoConsulta2);

                    }
                    else
                    {
                        ConfigurarLayoutG1();
                        txtResultadoConsulta.Text = nome;
                        txtResultadoConsulta2.Text = preco;
                    }
                }
                else
                {

                    troca++;
                    if (troca == tempoExibicao)
                    {
                        if (isG2 == true)
                            ConfigurarLayoutG2();
                        else
                            ConfigurarLayoutG1();

                        txtResultadoConsulta.Text = Conexao.Cliente.Texto1;
                        MudarObj(txtResultadoConsulta);
                        txtResultadoConsulta2.Text = Conexao.Cliente.Texto2; 
                        MudarObj(txtResultadoConsulta2);

                    }
                    if (troca == tempoExibicao * 2)
                    {
                        if (isG2 == true)
                            ConfigurarLayoutG2();
                        else
                            ConfigurarLayoutG1();
                        txtResultadoConsulta.Text = Conexao.Cliente.Texto3;
                        MudarObj(txtResultadoConsulta);
                        txtResultadoConsulta2.Text = Conexao.Cliente.Texto4;
                        MudarObj(txtResultadoConsulta2);
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
                    isG2 = true;
                    ConfigurarLayoutG2();
                    Conexao.Cliente.ModeloTerminal = "#tc406|4.0\0";

                }
                else
                {
                    isG2 = false;
                    CarregarImagem("buscapreco.jpg");
                    ConfigurarLayoutG1();
                    Conexao.Cliente.ModeloTerminal = "#tc502|4.0\0";

                }
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

        private void nomeCliente_TextChanged(object sender, EventArgs e)
        {
            Conexao.Cliente.NomeCli = txtNomeCliente.Text;
        }
        string msgAtual = " ";
        public void ImprimirDebug()
        {
            do
            {
                if (Conexao.Cliente.Debug != msgAtual)
                {
                    Console.WriteLine(Conexao.Cliente.Debug);
                    msgAtual = Conexao.Cliente.Debug;
                    Invoke(new Action(() => EscreveDebug(msgAtual)));
                }
            } while (true);
        }

        public void EscreveDebug(string Texto)
        {
            txtDebug.Text += Environment.NewLine + Texto;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TxtDebug_TextChanged(object sender, EventArgs e)
        {
            txtDebug.SelectionStart = txtDebug.Text.Length;
            txtDebug.ScrollToCaret();
        }

        private void MudarObj(TextBox CaixaTexto)
        {
            if (CaixaTexto.TextLength <= 10)
            {
                CaixaTexto.Font = new Font(CaixaTexto.Font.FontFamily, tamanhofont + 8);
                ConfigurarLayoutG2();
            }
            else if (CaixaTexto.TextLength > 10 && CaixaTexto.TextLength <= 15)
            {
                CaixaTexto.Font = new Font(CaixaTexto.Font.FontFamily, tamanhofont + 2);
                ConfigurarLayoutG2();
            }
            else
            {
                CaixaTexto.Font = new Font(CaixaTexto.Font.FontFamily, tamanhofont);
                ConfigurarLayoutG2();
            }
        }


        private void MudarObj(TextBox CaixaDesc, TextBox CaixaPreco)
        {
            if (CaixaDesc.TextLength <= 10)
            {
                CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont + 8);
                ConfigurarLayoutG2();
            }
            else if (CaixaDesc.TextLength > 10 && CaixaDesc.TextLength <= 15)
            {
                CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont + 2);
                ConfigurarLayoutG2();
            }
            else if (CaixaDesc.TextLength <= 20)
            {
                CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont);
                ConfigurarLayoutG2();
            }
            else
            { 
                if (CaixaDesc.TextLength>80)
                {
                    CaixaDesc.Text = CaixaDesc.Text.Substring(0, 80);
                }
                CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont);
                CaixaDesc.Size = new Size(144, 80);
                CaixaPreco.Size = new Size(144, 30);
                CaixaPreco.Location = new Point(120, 305);
            }
        }

        private void ConfigurarLayoutG2()
        {
            CarregarImagem("buscaprecoG2.jpg");
            txtBuscarProduto.Location = new Point(122, 385);
            txtBuscarProduto.Size = new Size(138, 26);

            //TEXTO LINHA 1
            txtResultadoConsulta.Location = new Point(120, 235);
            txtResultadoConsulta.Size = new Size(144, 40);
            txtResultadoConsulta.BackColor = Color.FromArgb(247, 242, 242);
            txtResultadoConsulta.ForeColor = Color.FromArgb(0, 97, 150);
            txtResultadoConsulta.Multiline = true;

            //TEXTO LINHA 2
            txtResultadoConsulta2.Location = new Point(120, 285);
            txtResultadoConsulta2.Size = new Size(144, 40);
            txtResultadoConsulta2.BackColor = Color.FromArgb(247, 242, 242);
            txtResultadoConsulta2.ForeColor = Color.FromArgb(0, 97, 150);
            txtResultadoConsulta2.Multiline = true;
        }

        private void ConfigurarLayoutG1()
        {
            //TEXTO LINHA 1
            txtResultadoConsulta.Location = new Point(82, 199);
            txtResultadoConsulta.Size = new Size(227, 23);
            txtResultadoConsulta.BackColor = Color.FromArgb(61, 79, 25);
            txtResultadoConsulta.Multiline = false;
            txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, 15);

            //TEXTO LINHA 2
            txtResultadoConsulta2.Location = new Point(82, 223);
            txtResultadoConsulta2.Size = new Size(227, 23);
            txtResultadoConsulta2.BackColor = Color.FromArgb(61, 79, 25);
            txtResultadoConsulta2.Multiline = false;
            txtResultadoConsulta2.Font = new Font(txtResultadoConsulta2.Font.FontFamily, 15);

            txtBuscarProduto.Location = new Point(132, 340);
            txtBuscarProduto.Size = new Size(126, 26);
        }
    }
}
