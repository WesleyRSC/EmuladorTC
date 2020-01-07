using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace EmuladorTC
{
    class Connection
    {
        public bool Conectado { get; set; }
        public Cliente Cliente { get; set; }
        public string Mensagem { get; set; }
        
        Socket conexao;        
        private Thread ComunicacaoThread;


        private void ComunicarServidor()
        {
            do
            {
                if (conexao.Connected)
                {
                    Comunicar();
                }
                else
                {
                    break;
                }
            } while (true);
        }

        //Inicia a conexão com o servidor
        public void Conectar(string IpServer, int Porta)
        {
            // Estabelece a conexão com o servidor via socket. 
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(IpServer);
            IPEndPoint remoteEP = new IPEndPoint(ipaddress, Porta);

            // Cria o TCP/IP socket.  
            conexao = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            conexao.Connect(remoteEP);

            Conectado = true;

            ComunicacaoThread = new Thread(ComunicarServidor);
            ComunicacaoThread.Start();
        }

        public void Desconectar()
        {
            //ComunicacaoThread.Abort();
            //conexao.Close();
            conexao.Shutdown(SocketShutdown.Both);            
            Conectado = false;
            Mensagem = "";
        }
        public void Comunicar()
        {
            try
            {
                byte[] bytes = new byte[1024];
                int bytesRec = conexao.Receive(bytes);
                Mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                LogMensagemEnviada("Recebida - " + Mensagem);

                if (Mensagem == "")
                    Desconectar();

                if (Mensagem == "#ok")
                    EnviarDados(Cliente.ModeloTerminal);

                if (Mensagem == "#live?")
                    EnviarDados("#live\0");

                if (Mensagem == "#alwayslive")
                    EnviarDados("#alwayslive_ok\0");

                if (Mensagem == "#checklive")
                    EnviarDados("#checklive_ok\0");

                if (Mensagem.IndexOf("#restartsoft") >= 0)
                {

                    MessageBox.Show("Terminal Reniciado");
                    EnviarDados("#restartsoft_ok\0");
                    Desconectar();
                    ComunicarServidor();
                }

                if (Mensagem == "#updconfig?")
                {
                    EnviarDados("#updconfig"
                    + Cliente.SomarTamanhoStringCom48(Cliente.GatewayCli) + Cliente.GatewayCli
                    + ";Sem Suporte"
                    + Cliente.SomarTamanhoStringCom48(Cliente.NomeCli) + Cliente.NomeCli
                    + ";Sem Suporte;Sem Suporte;Sem Suporte");
                }

                if (Mensagem == "#paramconfig?")
                //Pegar valores do terminal #paramconfig00 = para ipfixo e #paramconfig10 = para ipdinamico
                {                    
                    EnviarDados("#paramconfig" + Cliente.EnviarDhcp() + "\0");
                }

                if (Mensagem == "#config02?\0") //Pegar valores do terminal
                {
                    EnviarDados("#config02"
                        + Cliente.SomarTamanhoStringCom48(Cliente.Ipserv) + Cliente.Ipserv
                        + Cliente.SomarTamanhoStringCom48(Cliente.IpCli) + Cliente.IpCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.MascaraCli) + Cliente.MascaraCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto1) + Cliente.Texto1
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto2) + Cliente.Texto2
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto3) + Cliente.Texto3
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto4) + Cliente.Texto4
                        + Convert.ToChar(Convert.ToInt32(Cliente.TempoExibicao) + 48)
                        + "\0");
                }

                if (Mensagem == "#config?") //Pegar valores do terminal
                {
                    EnviarDados("#config"
                        + Cliente.SomarTamanhoStringCom48(Cliente.Ipserv) + Cliente.Ipserv
                        + Cliente.SomarTamanhoStringCom48(Cliente.IpCli) + Cliente.IpCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.MascaraCli) + Cliente.MascaraCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto1) + Cliente.Texto1
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto2) + Cliente.Texto2
                        + Convert.ToChar(Convert.ToInt32(Cliente.TempoExibicao) + 48));
                }

                if (Mensagem == "#extconfig?\0")
                {
                    EnviarDados("#extconfig"
                        + Cliente.SomarTamanhoStringCom48(Cliente.Ipserv) + Cliente.Ipserv
                        + Cliente.SomarTamanhoStringCom48(Cliente.IpCli) + Cliente.IpCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.MascaraCli) + Cliente.MascaraCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.GatewayCli) + Cliente.GatewayCli
                        + ";Sem Suporte"
                        + Cliente.SomarTamanhoStringCom48(Cliente.NomeCli) + Cliente.NomeCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto1) + Cliente.Texto1
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto2) + Cliente.Texto2
                        + ";Sem Suporte;Sem Suporte;Sem Suporte"
                        + Cliente.SomarTamanhoStringCom48(Cliente.TempoExibicao)
                        + Cliente.EnviarDhcp()
                        + "\0");                                 // 00 ip fixo, 10 ip dinamico
                }

                if (Mensagem.IndexOf("#rconf") >= 0)
                {
                    int tamanhoTemp = 6;

                    Cliente.Ipserv = ReceberConfig(1, tamanhoTemp, Mensagem);
                    Cliente.IpCli = ReceberConfig(2, tamanhoTemp, Mensagem);
                    Cliente.MascaraCli = ReceberConfig(3, tamanhoTemp, Mensagem);
                    Cliente.Texto1 = ReceberConfig(4, tamanhoTemp, Mensagem);
                    Cliente.Texto2 = ReceberConfig(5, tamanhoTemp, Mensagem);
                    Cliente.TempoExibicao = Convert.ToString(Convert.ToChar(ReceberConfig(6, tamanhoTemp, Mensagem, true)) - 48);
                }

                if (Mensagem.IndexOf("#reconf02") >= 0)
                {
                    int tamanhoTemp = 9;

                    Cliente.Ipserv = ReceberConfig(1, tamanhoTemp, Mensagem);
                    Cliente.IpCli = ReceberConfig(2, tamanhoTemp, Mensagem);
                    Cliente.MascaraCli = ReceberConfig(3, tamanhoTemp, Mensagem);
                    Cliente.Texto1 = ReceberConfig(4, tamanhoTemp, Mensagem);
                    Cliente.Texto2 = ReceberConfig(5, tamanhoTemp, Mensagem);
                    Cliente.Texto3 = ReceberConfig(6, tamanhoTemp, Mensagem);
                    Cliente.Texto4 = ReceberConfig(7, tamanhoTemp, Mensagem);
                    Cliente.TempoExibicao = Convert.ToString(Convert.ToChar(ReceberConfig(8, tamanhoTemp, Mensagem, true)) - 48);                    
                }

                if (Mensagem.IndexOf("#rextconf") >= 0)
                {
                    int tamanhoTemp = 9;

                    Cliente.Ipserv = ReceberConfig(1, tamanhoTemp, Mensagem);
                    Cliente.IpCli = ReceberConfig(2, tamanhoTemp, Mensagem);
                    Cliente.MascaraCli = ReceberConfig(3, tamanhoTemp, Mensagem);
                    Cliente.GatewayCli = ReceberConfig(4, tamanhoTemp, Mensagem); //Opção 5 é para ser descartada
                    Cliente.NomeCli = ReceberConfig(6, tamanhoTemp, Mensagem);
                    Cliente.Texto1 = ReceberConfig(7, tamanhoTemp, Mensagem);
                    Cliente.Texto2 = ReceberConfig(8, tamanhoTemp, Mensagem); //Opções 9, 10 e 11 serão descartadas
                    Cliente.TempoExibicao = Convert.ToString(Convert.ToChar(ReceberConfig(12, tamanhoTemp, Mensagem, true)) - 48);
                    EnviarDados("#rextconf_ok\0");
                }

                if (Mensagem.IndexOf("#rparamconfig") >= 0)
                {
                    int tamanhoTemp = 13;
                    ReceberDHCP(Mensagem.Substring(tamanhoTemp, 1));
                    EnviarDados("#rparamconfig_ok\0");
                }

                if (Mensagem.IndexOf("#rupdconfig") >= 0)
                {
                    int tamanhoTemp = 11;

                    Cliente.GatewayCli = ReceberConfig(1, tamanhoTemp, Mensagem); //Opção 2 será descartada
                    Cliente.NomeCli = ReceberConfig(3, tamanhoTemp, Mensagem);

                    EnviarDados("#rupdconfig_ok\0");
                }

                if (Mensagem.IndexOf("#mesg")>=0)
                {
                    int tamanhoTemp = 5;

                    Cliente.Texto1Temp = ReceberConfig(1, tamanhoTemp, Mensagem);
                    Cliente.Texto2Temp = ReceberConfig(2, tamanhoTemp, Mensagem);
                    Cliente.TempoExibicaoTemp = Convert.ToInt32(Convert.ToChar(ReceberConfig(3, tamanhoTemp, Mensagem, true)) - 48);
                }

                if (Mensagem.IndexOf("#gif") >= 0)
                {
                    Cliente.IndiceGif = int.Parse(Mensagem.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                    Cliente.NumeroLoopsGif = int.Parse(Mensagem.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
                    Cliente.TempoGif = int.Parse(Mensagem.Substring(8, 2), System.Globalization.NumberStyles.HexNumber);
                    Cliente.TamanhoQuadroGif = int.Parse(Mensagem.Substring(10, 6), System.Globalization.NumberStyles.HexNumber);

                    byte[] bytesGif = new byte[196608];
                    List<byte> Gif = new List<byte>();
                    Console.WriteLine("Tamanho da imagem recebida - " + Cliente.TamanhoQuadroGif + " Bytes");                   

                    do
                    {
                        int bytesRecGif = conexao.Receive(bytesGif);
                        for(int i = 0;i < bytesRecGif; i ++)
                        {
                            Gif.Add(bytesGif[i]);
                        }

                        Console.WriteLine("Imagem Recebida "+Gif.Count+" Bytes");

                    } while (Gif.Count < Cliente.TamanhoQuadroGif);

                    byte[] gifFinal = new byte[Cliente.TamanhoQuadroGif];

                    for (int i = 0; i < Cliente.TamanhoQuadroGif; i++)
                    {
                        gifFinal[i] = Gif[i];
                    }

                    string diretorioAtual = Directory.GetCurrentDirectory();
                    string pastaRaiz = diretorioAtual + @"\Imagens\imagem.gif";
                    File.WriteAllBytes(pastaRaiz, gifFinal);
                }

                if (Mensagem == "#macaddr?")
                {
                    string wifi = "";
                    if (Cliente.Wifi)
                    {
                        wifi = "1";
                    }
                    else
                    {
                        wifi = "0";
                    }
                    EnviarDados("#macaddr" + wifi + Cliente.SomarTamanhoStringCom48(Cliente.Mac));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void EnviarDados(string resposta)
        {
            LogMensagemEnviada("Enviada - " + resposta);
            byte[] comando = Encoding.ASCII.GetBytes(resposta);
            conexao.Send(comando);
        }
        public void EnviarProduto(string codBarras)
        {
            if (Conectado)
            {
                string codCompleto = "#" + codBarras + '\0';
                LogMensagemEnviada("Enviada - " + codCompleto);
                byte[] comando = Encoding.ASCII.GetBytes(codCompleto);
                conexao.Send(comando);
            }
            else
            {
                MessageBox.Show("Necessário Conectar Antes");
            }
        }
        
        public string ReceberConfig(int QntdCampos, int TamanhoInicial, string Informacoes)
        {
            int TamanhoRetorno = 0;
            for (int i = 1; i <= QntdCampos; i++)
            {
                if (i > 1)
                    TamanhoInicial += TamanhoRetorno + 1;

                TamanhoRetorno = Convert.ToChar(Informacoes.Substring(TamanhoInicial, 1)) - 48;
                
                if (i == QntdCampos)
                    return Informacoes.Substring(TamanhoInicial + 1, TamanhoRetorno);
            }
            return " ";
        }
        public string ReceberConfig(int QntdCampos, int TamanhoInicial, string Informacoes, bool ultimo)
        {
            int TamanhoRetorno = 0;
            for (int i = 1; i <= QntdCampos; i++)
            {
                if (i > 1)
                    TamanhoInicial += TamanhoRetorno + 1;

                TamanhoRetorno = Convert.ToChar(Informacoes.Substring(TamanhoInicial, 1)) - 48;

                if (i == QntdCampos && ultimo)
                    return Informacoes.Substring(TamanhoInicial, 1);
            }
            return " ";
        }
        public void ReceberDHCP(string dhcp)
        {
            if(dhcp == "1")
            {
                Cliente.DHCP = true;
            }
            else
            {
                Cliente.DHCP = false;
            }
        }
        private void LogMensagemEnviada(string mensagem)
        {            
            Cliente.Debug = mensagem;
            Console.WriteLine(mensagem);
        }
    }
}

