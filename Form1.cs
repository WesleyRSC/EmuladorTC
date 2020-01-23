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
            ReiniciarEquipamento();

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
                ModeloTerminal = "#tc406|4.0\0",
                Reconectar = false
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
        int trocaCarregar = 0;



        [Obsolete]
        private void Button1_Click(object sender, EventArgs e)
        {
            Conectar();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Faz a atualização dos campos a cada Tick do Timer1 ---------------------------------------------------------------------
            if (!Conexao.Conectado)
            {
                botaoConectar.Text = "Conectar";
                botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
                botaoConsulta.Enabled = false;
            }
            else
            {
                botaoConectar.Text = "Desconectar";
                botaoConectar.BackColor = Color.FromArgb(0, 97, 150);
                botaoConsulta.Enabled = true;
                cbModelo.Enabled = false;
            }

            //------------------------------------------------------------------------------------------------------------------------

            

            string produto = Conexao.Mensagem;
            byte[] imagem = null; 
            imagem  = Conexao.Cliente.Imagem;

            if (Conexao.Conectado)
            {
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
                    //Reinicia o equipamento e recebe as informações do servidor ----------------------------------------------------------                
                    if (Conexao.Cliente.RecebeConfig)
                    {
                        if(Conexao.Cliente.RecebeConfig == true)
                        {
                            Conexao.Cliente.RecebeConfig = false;
                            ReceberConfig();
                        }
                        if (Conexao.Conectado)
                        {
                            Conexao.Cliente.Reconectar = true;
                        }
                        ReiniciarEquipamento();
                    }
                    //----------------------------------------------------------------------------------------------------------------------

                    //Exibe consulta de preço ----------------------------------------------------------------------------------------------
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
                        //Exibe mensagem das linhas ----------------------------------------------------------------------------------------
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
                    //----------------------------------------------------------------------------------------------------------------------
                }
            }
            else
            {
                tReiniciar.Start();
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


        //Ao Mudar a Combo Box ---------------------------------------------------------------------------------------------------
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

                    pReiniciarConfig.Visible = false;
                    txtResultadoConsulta.Visible = false;
                    txtResultadoConsulta2.Visible = false;
                    isG2 = false;
                    CarregarImagem("buscapreco.jpg");
                    VerificarDisp(isG2);
                    Conexao.Cliente.ModeloTerminal = "#tc502|4.0\0";
                }
                txtResultadoConsulta.Text = "";
                txtResultadoConsulta2.Text = "";
                ReiniciarEquipamento();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------



        //Seleciona o GIF ---------------------------------------------------------------------------------------------------------
        private void CarregarImagem(string nomeImagem)
        {
            string diretorioAtual = Directory.GetCurrentDirectory();

            string pastaRaiz = diretorioAtual + @"\Imagens\";

            var caminhoImagem = Path.Combine(pastaRaiz, nomeImagem);
            pbImagemG2.Image = Image.FromFile(caminhoImagem);
        }
        //------------------------------------------------------------------------------------------------------------------------



        //Imprime a comunicação com o servidor no debug --------------------------------------------------------------------------
        public void ImprimirDebug()
        {
            do
            {
                if (Conexao.Cliente.Debug != msgAtual)
                {
                    Console.WriteLine(Conexao.Cliente.Debug);
                    msgAtual = Conexao.Cliente.Debug;
                    Invoke(new Action(() => EscreverDebug(msgAtual)));
                }
            } while (true);
        }
        //Escreve na Debug
        public void EscreverDebug(string Texto)
        {
            txtDebug.Text += Environment.NewLine + Texto;
        }
        //Faz acmopanhar a barra de rolagem
        private void TxtDebug_TextChanged(object sender, EventArgs e)
        {
            txtDebug.SelectionStart = txtDebug.Text.Length;
            txtDebug.ScrollToCaret();
        }
        //------------------------------------------------------------------------------------------------------------------------



        //Responsavel por mudar os textos que serão exibidos ---------------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------



        //Faz as mudanças das textBox somente na consulta de preço ---------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------



        //Layout pré definido do Busca Preço G2 ----------------------------------------------------------------------------------
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
            txtIniciaG1.Visible = false;

            //TEXTO LINHA 2
            txtResultadoConsulta2.Location = new Point(119, 285);
            txtResultadoConsulta2.Size = new Size(146, 40);
            txtResultadoConsulta2.BackColor = Color.FromArgb(247, 242, 242);
            txtResultadoConsulta.MaxLength = 32767;
            txtResultadoConsulta2.ForeColor = Color.FromArgb(0, 97, 150);
            txtResultadoConsulta2.Multiline = true;
        }
        //------------------------------------------------------------------------------------------------------------------------


        //Layout pré definido do Busca Preço G1 ----------------------------------------------------------------------------------
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
            txtIniciaG1.Location=txtResultadoConsulta.Location;
            txtIniciaG1.Size = txtResultadoConsulta.Size;
            txtIniciaG1.BackColor = txtResultadoConsulta.BackColor;
            txtIniciaG1.ForeColor = txtResultadoConsulta.ForeColor;
            txtIniciaG1.Font = txtResultadoConsulta.Font;

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
        //------------------------------------------------------------------------------------------------------------------------


        //Faz a verificação de qual dispositivo está selecionado -----------------------------------------------------------------
        private void VerificarDisp(bool isG2)
        {
            if (isG2)
                ConfigurarLayoutG2();
            else
                ConfigurarLayoutG1();
        }
        //------------------------------------------------------------------------------------------------------------------------



        //Exibe o produto consultado ---------------------------------------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------



        //Reproduz os Gifs já carregados -----------------------------------------------------------------------------------------
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
        //------------------------------------------------------------------------------------------------------------------------



        //Fecha as conecções e treads ao fechar o Form ---------------------------------------------------------------------------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Conexao.Conectado)
            {
                Conexao.Desconectar();
                CheckDebug.Abort();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------


        //Simula a reinicialização dos equipamentos ------------------------------------------------------------------------------
        private void ReiniciarEquipamento()
        {

            btnSalvarTab1.Enabled = false;
            btnSalvarTab2.Enabled = false;
            if (isG2)
            {
                pReiniciar.Visible = true;
            }
            else
            {
                pReiniciar.Visible = false;
            }
            trocaCarregar = 0;
            tReiniciar.Start();
            if (Conexao.Conectado)
            {
                timer1.Stop();
                troca = 0;
                Conexao.Desconectar();
                CheckDebug.Abort();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------




        //Timer base para o tempo de reinicialização -----------------------------------------------------------------------------
        private void TReiniciar_Tick(object sender, EventArgs e)
        {
            //Faz a atualização dos campos a cada Tick do Timer ---------------------------------------------------------------------
            if (!Conexao.Conectado)
            {
                botaoConectar.Text = "Conectar";
                botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
                botaoConsulta.Enabled = false;
            }
            else
            {
                botaoConectar.Text = "Desconectar";
                botaoConectar.BackColor = Color.FromArgb(0, 97, 150);
                botaoConsulta.Enabled = true;
                cbModelo.Enabled = false;
            }

            //------------------------------------------------------------------------------------------------------------------------

            if (isG2)
            {
                trocaCarregar++;
                lblIpServidor.Text = "IP DO SERVIDOR: " + Conexao.Cliente.Ipserv;
                lblIpLocal.Text = "IP LOCAL: " + Conexao.Cliente.IpCli;
                Trocar(trocaCarregar);
            }
            else
            {
                trocaCarregar++;
                Trocar(trocaCarregar);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------


        //Responsável por fazer as trocas das telas de inicialização -------------------------------------------------------------
        private void Trocar(int troca)
        {
            if (troca == 1)
            {
                if (isG2)
                {
                    pCarregar1.BackColor = Color.FromArgb(18, 29, 91);
                    pCarregar2.BackColor = Color.DarkGray;
                    pCarregar3.BackColor = Color.LightGray;
                }
                else
                {
                    txtIniciaG1.Visible = true;
                    txtIniciaG1.Text = "Inicializando .";
                    txtResultadoConsulta2.Text = "";
                                    }
            }
            if (troca == 2)
            {
                if (isG2)
                {
                    pCarregar1.BackColor = Color.DarkGray;
                    pCarregar2.BackColor = Color.FromArgb(18, 29, 91);
                    pCarregar3.BackColor = Color.LightGray;
                }
                else
                {
                    txtIniciaG1.Visible = true;
                    txtIniciaG1.Text = "Inicializando ..";
                    txtResultadoConsulta2.Text = "";
                }
            }
            if (troca == 3)
            {
                if (isG2)
                {
                    pCarregar1.BackColor = Color.LightGray;
                    pCarregar2.BackColor = Color.DarkGray;
                    pCarregar3.BackColor = Color.FromArgb(18, 29, 91);
                }
                else
                {
                    txtIniciaG1.Visible = true;
                    txtIniciaG1.Text = "Inicializando ...";
                    txtResultadoConsulta2.Text = "";
                }
            }
            if (troca == 4)
            {
                btnSalvarTab1.Enabled = true;
                btnSalvarTab2.Enabled = true;
                if (isG2)
                {
                    pReiniciar.Visible = false;
                    pReiniciarConfig.Visible = true;
                    pCarregar4.BackColor = Color.FromArgb(18, 29, 91);
                    pCarregar5.BackColor = Color.DarkGray;
                    pCarregar6.BackColor = Color.LightGray;
                }
                else
                {
                    txtIniciaG1.Visible = true;
                    txtIniciaG1.Text = "Conectando ....";
                    txtResultadoConsulta2.Text = "";
                }
            }
            if (troca == 5)
            {
                if (isG2)
                {
                    pReiniciar.Visible = false;
                    pReiniciarConfig.Visible = true;
                    pCarregar4.BackColor = Color.DarkGray;
                    pCarregar5.BackColor = Color.FromArgb(18, 29, 91);
                    pCarregar6.BackColor = Color.LightGray;
                }
                else
                {
                    txtIniciaG1.Visible = true;
                    txtIniciaG1.Text = "Conectando .....";
                    txtResultadoConsulta2.Text = "";
                }
            }
            if(troca == 6)
            {
                if (isG2)
                {
                    pReiniciar.Visible = false;
                    pReiniciarConfig.Visible = true;
                    pCarregar4.BackColor = Color.LightGray;
                    pCarregar5.BackColor = Color.DarkGray;
                    pCarregar6.BackColor = Color.FromArgb(18, 29, 91);
                }
                else
                {
                    txtIniciaG1.Visible = true;
                    txtIniciaG1.Text = "Conectando ......";
                    txtResultadoConsulta2.Text = "";
                }
            }
            if(troca == 7)
            {
                if (Conexao.Cliente.Reconectar)
                {
                    Conectar();
                    Conexao.Cliente.Reconectar = false;
                }
                if (Conexao.Conectado)
                {
                    trocaCarregar = 0;
                    tReiniciar.Stop();
                    if (isG2)
                    {
                        pReiniciarConfig.Visible = false;
                    }
                    else
                    {
                        txtIniciaG1.Visible = false;
                        txtResultadoConsulta.Visible = true;
                        txtResultadoConsulta2.Visible = true;
                    }
                }
                else
                {
                    trocaCarregar = 3;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------



        private void btnSalvarTab1_Click(object sender, EventArgs e)
        {
            if (!Conexao.Conectado)
            {
                ReiniciarEquipamento();
                EnviarConfig();
                ReceberConfig();
            }
            else
            {
                Conexao.Cliente.Reconectar = true;
                ReiniciarEquipamento();
                EnviarConfig();
                ReceberConfig();
            }
        }


        //Envia as configurações para o servidor ---------------------------------------------------------------------------------
        private void EnviarConfig()
        {
            //FAZ AS CONFIGURAÇÕES DO EMULADOR
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
        }
        //------------------------------------------------------------------------------------------------------------------------


        //Recebe as informações do servidor --------------------------------------------------------------------------------------
        private void ReceberConfig()
        {
            //RECEBE AS INFORMAÇÕES DO SERVIDOR
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


        public void Conectar()
        {
            try
            {
                if (!Conexao.Conectado)
                {
                    timer1.Start();
                    CheckDebug = new Thread(ImprimirDebug);
                    CheckDebug.Start();
                    Conexao.Conectar(ipServidor.Text, int.Parse(porta.Text));
                    botaoConectar.Text = "Desconectar";
                    botaoConectar.BackColor = Color.FromArgb(0, 97, 150);
                    cbModelo.Enabled = false;
                    botaoConsulta.Enabled = false;
                    trocaCarregar = 3;
                }
                else
                {
                    timer1.Stop();
                    troca = 0;
                    Conexao.Desconectar();
                    CheckDebug.Abort();
                    botaoConectar.Text = "Conectar";
                    botaoConectar.BackColor = Color.FromArgb(249, 161, 0);
                    botaoConsulta.Enabled = false;
                    cbModelo.Enabled = true;
                    tReiniciar.Start();
                }
            }
            catch (Exception x)
            {
                EscreverDebug(x.Message);
            }
        }
    }
}
