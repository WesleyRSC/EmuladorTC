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
            pReiniciarConfig.Visible = false;
            pReiniciar.Visible = false;
            ReiniciarEquipamentoG2();

            Conexao.Cliente = new Cliente
            {
                Ipserv = ipServidor.Text,
                Porta = porta.Text,
                NomeCli = txtNomeCliente.Text,
                IpCli = ipCliente.Text,
                MascaraCli = mascaraCliente.Text,
                GatewayCli = txtGatewayCliente.Text,
                Texto1 = txtTexto1.Text,
                Texto2 = txtTexto2.Text,
                Texto3 = txtTexto3.Text,
                Texto4 = txtTexto4.Text,
                TempoExibicao = txtTempoExibicao.Text,
                DHCP = false,
                Wifi = false,
                TempoExibicaoTemp = 0,
                Mac = txtMac.Text,
                ModeloTerminal = "#tc406|4.0\0"
            };
            Conexao.Mensagem = "Aguardando...";

            //combobox Modelo equipamento:
            cbModelo.SelectedIndex = 0;
            ReceberConfig();
        }

        bool isG2 = false;
        bool isGif = false;
        Connection Conexao = new Connection();
        const float tamanhofont = 10;        
        string nome = "";
        string preco = "";
        string msgAtual = " ";
        int tempoExibicao = 0;
        int troca = 0;
        int tempoExibicaoProduto = 0;



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
                    cbModelo.Enabled = false;
                    timer1.Start();
                }
                else
                {
                    Conexao.Desconectar();
                    CheckDebug.Abort();
                    botaoConectar.Text = "Conectar";
                    botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
                    cbModelo.Enabled = true;
                    timer1.Stop();
                }
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Faz a atualização dos campos a cada Tick do Timer1 ---------------------------------------------------------------------
            if (!Conexao.Conectado)
            {
                botaoConectar.Text = "Conectar";
                botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
                cbModelo.Enabled = true;
            }
            else
            {
                botaoConectar.Text = "Desconectar";
                botaoConectar.BackColor = Color.FromArgb(0, 97, 150);
                cbModelo.Enabled = false;
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
            //------------------------------------------------------------------------------------------------------------------------



            string produto = Conexao.Mensagem;
            byte[] imagem = null; 
            imagem  = Conexao.Cliente.Imagem;

            //Exibe GIF
            if (imagem != null)
            {
                ReproduzirGif(imagem);
            }            
            //Exibe a mensagem Instantânea
            if (Conexao.Cliente.TempoExibicaoTemp > 0)
            {
                VerificarDisp(isG2);
                txtResultadoConsulta.Text = Conexao.Cliente.Texto1Temp;
                MudarObj(txtResultadoConsulta);
                txtResultadoConsulta2.Text = Conexao.Cliente.Texto2Temp;
                MudarObj(txtResultadoConsulta2);
                Conexao.Cliente.TempoExibicaoTemp--;
            }
            else
            {
                if (Conexao.Cliente.RecebeConfig)
                {
                    ReceberConfig();
                    Conexao.Cliente.RecebeConfig = false;
                }
                //Exibe consulta de preço
                if (produto.IndexOf("|") >= 0 || tempoExibicaoProduto > 0 || produto == "#nfound")
                {
                    pbGifImagem.Visible = false;
                    ExibirProduto(produto);
                    if (tempoExibicaoProduto == 0 && isGif)
                    {
                        pbGifImagem.Visible = true;
                    }
                }
                else
                {
                    //Exibe mensagem das linhas
                    troca++;
                    if (troca == tempoExibicao)
                    {
                        VerificarDisp(isG2);
                        txtResultadoConsulta.Text = Conexao.Cliente.Texto1;
                        MudarObj(txtResultadoConsulta);
                        txtResultadoConsulta2.Text = Conexao.Cliente.Texto2;
                        MudarObj(txtResultadoConsulta2);

                    }
                    if (troca == tempoExibicao * 2)
                    {
                        VerificarDisp(isG2);
                        txtResultadoConsulta.Text = Conexao.Cliente.Texto3;
                        MudarObj(txtResultadoConsulta);
                        txtResultadoConsulta2.Text = Conexao.Cliente.Texto4;
                        MudarObj(txtResultadoConsulta2);
                        troca = 0;
                    }
                }
            }            
        }

        //Envia o texto dos campos para as variaveis na Client -----------------------------------------------------------------
        private void button1_Click_2(object sender, EventArgs e)
        {
            Conexao.EnviarProduto(txtBuscarProduto.Text);
        }
        private void txtTempoExibicao_TextChanged(object sender, EventArgs e)
        {
            
        }
        //------------------------------------------------------------------------------------------------------------------------


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
                    VerificarDisp(isG2);
                    Conexao.Cliente.ModeloTerminal = "#tc406|4.0\0";
                }
                else
                {
                    isG2 = false;
                    CarregarImagem("buscapreco.jpg");
                    VerificarDisp(isG2);
                    Conexao.Cliente.ModeloTerminal = "#tc502|4.0\0";
                }
            }
        }

        //Seleciona o GIF ---------------------------------------------------------------------------------------------------------
        private void CarregarImagem(string nomeImagem)
        {
            string diretorioAtual = Directory.GetCurrentDirectory();

            string pastaRaiz = diretorioAtual + @"\Imagens\";

            var caminhoImagem = Path.Combine(pastaRaiz, nomeImagem);
            pbImagemG2.Image = Image.FromFile(caminhoImagem);
        }
        //------------------------------------------------------------------------------------------------------------------------

     
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
                VerificarDisp(isG2);
            }
            else if (CaixaTexto.TextLength > 10 && CaixaTexto.TextLength <= 15)
            {
                CaixaTexto.Font = new Font(CaixaTexto.Font.FontFamily, tamanhofont + 2);
                VerificarDisp(isG2);
            }
            else
            {
                CaixaTexto.Font = new Font(CaixaTexto.Font.FontFamily, tamanhofont);
                VerificarDisp(isG2);
            }
        }
        private void MudarObj(TextBox CaixaDesc, TextBox CaixaPreco)
        {
            if (CaixaDesc.Text == "Produto" && CaixaPreco.Text == "não encontrado")
            {
                CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont + 8);
                CaixaPreco.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont + 2);
                VerificarDisp(isG2);
            }
            else
            {
                CaixaPreco.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont + 8);
                if (CaixaDesc.TextLength <= 10)
                {
                    CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont + 8);
                    VerificarDisp(isG2);
                }
                else if (CaixaDesc.TextLength > 10 && CaixaDesc.TextLength <= 15)
                {
                    CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont + 2);
                    VerificarDisp(isG2);
                }
                else if (CaixaDesc.TextLength <= 20)
                {
                    CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont);
                    VerificarDisp(isG2);
                }
                else
                {
                    if (CaixaDesc.TextLength > 80)
                    {
                        CaixaDesc.Text = CaixaDesc.Text.Substring(0, 80);
                    }
                    CaixaDesc.Font = new Font(CaixaDesc.Font.FontFamily, tamanhofont);
                    CaixaDesc.Size = new Size(146, 80);
                    CaixaPreco.Size = new Size(146, 30);
                    CaixaPreco.Location = new Point(120, 305);

                }
            }
        }


        private void ConfigurarLayoutG2()
        {
            CarregarImagem("buscaprecoG2.jpg");
            txtBuscarProduto.Location = new Point(122, 385);
            txtBuscarProduto.Size = new Size(138, 26);

            //TEXTO LINHA 1
            txtResultadoConsulta.Location = new Point(119, 240);
            txtResultadoConsulta.Size = new Size(146, 40);
            txtResultadoConsulta.BackColor = Color.FromArgb(247, 242, 242);
            txtResultadoConsulta.MaxLength = 32767;
            txtResultadoConsulta.ForeColor = Color.FromArgb(0, 97, 150);
            txtResultadoConsulta.Multiline = true;

            //TEXTO LINHA 2
            txtResultadoConsulta2.Location = new Point(119, 285);
            txtResultadoConsulta2.Size = new Size(146, 40);
            txtResultadoConsulta2.BackColor = Color.FromArgb(247, 242, 242);
            txtResultadoConsulta.MaxLength = 32767;
            txtResultadoConsulta2.ForeColor = Color.FromArgb(0, 97, 150);
            txtResultadoConsulta2.Multiline = true;
        }
        private void ConfigurarLayoutG1()
        {
            //TEXTO LINHA 1
            txtResultadoConsulta.Location = new Point(82, 199);
            txtResultadoConsulta.Size = new Size(227, 23);
            txtResultadoConsulta.BackColor = Color.FromArgb(61, 79, 25);
            txtResultadoConsulta.ForeColor = Color.FromArgb(247, 242, 242);
            txtResultadoConsulta.MaxLength = 20;
            txtResultadoConsulta.Multiline = false;
            txtResultadoConsulta.Font = new Font(txtResultadoConsulta.Font.FontFamily, 15);

            //TEXTO LINHA 2
            txtResultadoConsulta2.Location = new Point(82, 223);
            txtResultadoConsulta2.Size = new Size(227, 23);
            txtResultadoConsulta2.BackColor = Color.FromArgb(61, 79, 25);
            txtResultadoConsulta2.ForeColor = Color.FromArgb(247, 242, 242);
            txtResultadoConsulta2.MaxLength = 20;
            txtResultadoConsulta2.Multiline = false;
            txtResultadoConsulta2.Font = new Font(txtResultadoConsulta2.Font.FontFamily, 15);

            txtBuscarProduto.Location = new Point(132, 340);
            txtBuscarProduto.Size = new Size(126, 26);
        }
        private void VerificarDisp(bool isG2)
        {
            if (isG2)
                ConfigurarLayoutG2();
            else
                ConfigurarLayoutG1();
        }

        private void ExibirProduto(string produto)
        {
            if (tempoExibicaoProduto <= 0)
            {
                tempoExibicaoProduto = tempoExibicao;
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
            //Setar para tempo da mensagem
            Conexao.Mensagem = "";
            tempoExibicaoProduto--;
            if (isG2)
            {
                txtResultadoConsulta.Text = nome;
                txtResultadoConsulta2.Text = preco;
                MudarObj(txtResultadoConsulta, txtResultadoConsulta2);
            }
            else
            {
                ConfigurarLayoutG1();
                txtResultadoConsulta.Text = nome;
                txtResultadoConsulta2.Text = preco;
            }
        }
        public void ReproduzirGif(byte[] imagem)
        {
            if (!isGif)
            {
                MemoryStream imgConvertida = new MemoryStream(imagem);
                Image im = Image.FromStream(imgConvertida);
                pbGifImagem.Image = im;
                pbGifImagem.Visible = true;
            }
            //Apresenta a imagem imediatamente
            if (Conexao.Cliente.IndiceGif == 0 && Conexao.Cliente.TempoGif > 0 && isG2)
            {
                isGif = true;
                Conexao.Cliente.TempoGif--;
            }
            //Apresenta a imagem da pesquisa de preço
            else if (Conexao.Cliente.IndiceGif == 254 && Conexao.Cliente.TempoGif > 0 && isG2)
            {
                isGif = true;
                Conexao.Cliente.TempoGif--;
            }
            //Apresenta a imagem imediatamente
            else if (Conexao.Cliente.IndiceGif > 0 && Conexao.Cliente.IndiceGif < 254 && Conexao.Cliente.TempoGif > 0 && isG2)
            {
                isGif = true;
            }
            //Reseta a apresentação da imagem
            else
            {
                Conexao.Cliente.Imagem = null;
                pbGifImagem.Visible = false;
                isGif = false;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Conexao.Conectado)
            {
                Conexao.Desconectar();
                CheckDebug.Abort();
            }
        }

        private void ReiniciarEquipamentoG2()
        {

            btnSalvarTab1.Enabled = false;
            btnSalvarTab2.Enabled = false;
            botaoConectar.Enabled = false;
            pReiniciar.Visible = true;
            tReiniciar.Start();
            if (Conexao.Conectado)
            {
                Conexao.Desconectar();
                CheckDebug.Abort();
            }

        }
        int trocaCarregar = 0;
        private void TReiniciar_Tick(object sender, EventArgs e)
        {

            trocaCarregar++;
            lblIpServidor.Text = "IP DO SERVIDOR: " + Conexao.Cliente.Ipserv;
            lblIpLocal.Text = "IP LOCAL: " + Conexao.Cliente.IpCli;
            trocar(trocaCarregar);
        }

        private void trocar(int troca)
        {
            if (troca == 1)
            {
               
                pCarregar1.BackColor = Color.FromArgb(18, 29, 91);
                pCarregar2.BackColor = Color.DarkGray;
                pCarregar3.BackColor = Color.LightGray;
            }
            if (troca == 2)
            {
                pCarregar1.BackColor = Color.DarkGray;
                pCarregar2.BackColor = Color.FromArgb(18, 29, 91);
                pCarregar3.BackColor = Color.LightGray;
            }
            if (troca == 3)
            {
                pCarregar1.BackColor = Color.LightGray;
                pCarregar2.BackColor = Color.DarkGray;
                pCarregar3.BackColor = Color.FromArgb(18, 29, 91);
               
               
            }
            if (troca == 4)
            {
                pReiniciar.Visible = false;
                pReiniciarConfig.Visible = true;
                pCarregar4.BackColor = Color.FromArgb(18, 29, 91);
                pCarregar5.BackColor = Color.DarkGray;
                pCarregar6.BackColor = Color.LightGray;

            }
            if (troca == 5)
            {
                pCarregar4.BackColor = Color.DarkGray;
                pCarregar5.BackColor = Color.FromArgb(18, 29, 91);
                pCarregar6.BackColor = Color.LightGray;
            }
            if(troca == 6)
            {
                pCarregar4.BackColor = Color.LightGray;
                pCarregar5.BackColor = Color.DarkGray;
                pCarregar6.BackColor = Color.FromArgb(18, 29, 91);
            }
            if(troca == 7)
            {
                pReiniciarConfig.Visible = false;
                trocaCarregar = 0;
                tReiniciar.Stop();
                btnSalvarTab1.Enabled = true;
                btnSalvarTab2.Enabled = true;
                botaoConectar.Enabled = true;
            }

        }

        private void PReiniciarConfig_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSalvarTab1_Click(object sender, EventArgs e)
        {

            ReiniciarEquipamentoG2();
            EnviarConfig();
            ReceberConfig();

            
        }

        private void EnviarConfig()
        {
            //FAZ AS CONFIGURAÇÕES DO EMULADOR-----------------------------------------------------------------------------------------
            Conexao.Cliente.Texto1 = txtTexto1.Text;
            Conexao.Cliente.Texto2 = txtTexto2.Text;
            Conexao.Cliente.Texto3 = txtTexto3.Text;
            Conexao.Cliente.Texto4 = txtTexto4.Text;
            Conexao.Cliente.GatewayCli = txtGatewayCliente.Text;
            Conexao.Cliente.Mac = txtMac.Text;
            Conexao.Cliente.NomeCli = txtNomeCliente.Text;
            Conexao.Cliente.Porta = porta.Text;
            Conexao.Cliente.IpCli = ipCliente.Text;
            Conexao.Cliente.MascaraCli = mascaraCliente.Text;
            troca = 0;
            Conexao.Cliente.TempoExibicao = txtTempoExibicao.Text;
            Conexao.Cliente.Ipserv = ipServidor.Text;



            if (checkBoxWifi.Checked)
                Conexao.Cliente.Wifi = true;
            else
                Conexao.Cliente.Wifi = false;

            if (rbIpFixo.Checked)
                Conexao.Cliente.DHCP = false;
            else
                Conexao.Cliente.DHCP = true;

            //------------------------------------------------------------------------------------------------------------------------

        }

        private void ReceberConfig()
        {
            //RECEBE AS INFORMAÇÕES DO SERVIDOR-----------------------------------------------------------------------------------------
            if (Conexao.Cliente.DHCP)
            {
                rbDhcp.Checked = true;
                rbIpFixo.Checked = false;
            }
            else
            {
                rbDhcp.Checked = false;
                rbIpFixo.Checked = true;
            }
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
            //------------------------------------------------------------------------------------------------------------------------

        }
    }
}
