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

        Socket conexao;
        private string mensagem = "aguardando...";
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
            ComunicacaoThread.Abort();
            conexao.Shutdown(SocketShutdown.Both);
            conexao.Close();
            Conectado = false;
            mensagem = "";
        }
        public void Comunicar()
        {
            try
            {
                byte[] bytes = new byte[1024];
                int bytesRec = conexao.Receive(bytes);
                mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine(mensagem);

                if (mensagem == "#ok")
                {
                    EnviarDados(Cliente.ModeloTerminal);
                    Console.WriteLine(Cliente.ModeloTerminal);
                }

                if (mensagem == "#live?")
                {
                    EnviarDados("#live");
                }

                if (mensagem == "#alwayslive")
                {
                    EnviarDados("#alwayslive_ok");
                }

                if (mensagem == "#checklive")
                {
                    EnviarDados("#checklive_ok");
                }

                /*
                if (mensagem == "#restartsoft")
                {

                }
                */

                if (mensagem == "#updconfig?")
                {
                    EnviarDados("#updconfig"
                    + Cliente.SomarTamanhoStringCom48(Cliente.GatewayCli) + Cliente.GatewayCli
                    + ";Sem Suporte"
                    + Cliente.SomarTamanhoStringCom48(Cliente.NomeCli) + Cliente.NomeCli
                    + ";Sem Suporte;Sem Suporte;Sem Suporte");
                }

                if (mensagem == "#paramconfig?")
                //Pegar valores do terminal #paramconfig00 = para ipfixo e #paramconfig10 = para ipdinamico
                {
                    Console.WriteLine("#paramconfig" + Cliente.EnviarDhcp());
                    EnviarDados("#paramconfig" + Cliente.EnviarDhcp());
                }

                if (mensagem == "#config02?") //Pegar valores do terminal
                {
                    EnviarDados("#config02"
                        + Cliente.SomarTamanhoStringCom48(Cliente.Ipserv) + Cliente.Ipserv
                        + Cliente.SomarTamanhoStringCom48(Cliente.IpCli) + Cliente.IpCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.MascaraCli) + Cliente.MascaraCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto1) + Cliente.Texto1
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto2) + Cliente.Texto2
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto3) + Cliente.Texto3
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto4) + Cliente.Texto4
                        + Convert.ToChar(Convert.ToInt32(Cliente.TempoExibicao) + 48));
                }

                if (mensagem == "#config?") //Pegar valores do terminal
                {
                    EnviarDados("#config"
                        + Cliente.SomarTamanhoStringCom48(Cliente.Ipserv) + Cliente.Ipserv
                        + Cliente.SomarTamanhoStringCom48(Cliente.IpCli) + Cliente.IpCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.MascaraCli) + Cliente.MascaraCli
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto1) + Cliente.Texto1
                        + Cliente.SomarTamanhoStringCom48(Cliente.Texto2) + Cliente.Texto2
                        + Convert.ToChar(Convert.ToInt32(Cliente.TempoExibicao) + 48));
                }

                if (mensagem == "#extconfig?")
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
                        + Cliente.EnviarDhcp());                                 // 00 ip fixo, 10 ip dinamico
                }

                if (mensagem.IndexOf("#rconf") >= 0)
                {
                    int tamanhoTemp = 6;

                    Cliente.Ipserv = ReceberConfig(1, tamanhoTemp, mensagem);
                    Cliente.IpCli = ReceberConfig(2, tamanhoTemp, mensagem);
                    Cliente.MascaraCli = ReceberConfig(3, tamanhoTemp, mensagem);
                    Cliente.Texto1 = ReceberConfig(4, tamanhoTemp, mensagem);
                    Cliente.Texto2 = ReceberConfig(5, tamanhoTemp, mensagem);
                    Cliente.TempoExibicao = Convert.ToString(Convert.ToChar(ReceberConfig(6, tamanhoTemp, mensagem, true)) - 48);
                }

                if (mensagem.IndexOf("#reconf02") >= 0)
                {
                    int tamanhoTemp = 9;

                    Cliente.Ipserv = ReceberConfig(1, tamanhoTemp, mensagem);
                    Cliente.IpCli = ReceberConfig(2, tamanhoTemp, mensagem);
                    Cliente.MascaraCli = ReceberConfig(3, tamanhoTemp, mensagem);
                    Cliente.Texto1 = ReceberConfig(4, tamanhoTemp, mensagem);
                    Cliente.Texto2 = ReceberConfig(5, tamanhoTemp, mensagem);
                    Cliente.Texto3 = ReceberConfig(6, tamanhoTemp, mensagem);
                    Cliente.Texto4 = ReceberConfig(7, tamanhoTemp, mensagem);
                    Cliente.TempoExibicao = Convert.ToString(Convert.ToChar(ReceberConfig(8, tamanhoTemp, mensagem, true)) - 48);                    
                }

                if (mensagem.IndexOf("#rextconf") >= 0)
                {
                    int tamanhoTemp = 9;

                    Cliente.Ipserv = ReceberConfig(1, tamanhoTemp, mensagem);
                    Cliente.IpCli = ReceberConfig(2, tamanhoTemp, mensagem);
                    Cliente.MascaraCli = ReceberConfig(3, tamanhoTemp, mensagem);
                    Cliente.GatewayCli = ReceberConfig(4, tamanhoTemp, mensagem); //Opção 5 é para ser descartada
                    Cliente.NomeCli = ReceberConfig(6, tamanhoTemp, mensagem);
                    Cliente.Texto1 = ReceberConfig(7, tamanhoTemp, mensagem);
                    Cliente.Texto2 = ReceberConfig(8, tamanhoTemp, mensagem); //Opções 9, 10 e 11 serão descartadas
                    Cliente.TempoExibicao = Convert.ToString(Convert.ToChar(ReceberConfig(12, tamanhoTemp, mensagem, true)) - 48);
                    EnviarDados("#rextconf_ok");
                }

                if (mensagem.IndexOf("#rparamconfig") >= 0)
                {
                    int tamanhoTemp = 13;

                    ReceberDHCP(mensagem.Substring(tamanhoTemp));

                    EnviarDados("#rparamconfig_ok");
                }

                if (mensagem.IndexOf("#rupdconfig") >= 0)
                {
                    int tamanhoTemp = 11;

                    Cliente.GatewayCli = ReceberConfig(1, tamanhoTemp, mensagem); //Opção 3 será descartada
                    Cliente.NomeCli = ReceberConfig(3, tamanhoTemp, mensagem);

                    EnviarDados("#rupdconfig_ok");
                }

                if (mensagem.IndexOf("#mesg")>=0)
                {
                    int tamanhoTemp = 5;

                    Cliente.Texto1Temp = ReceberConfig(1, tamanhoTemp, mensagem);
                    Cliente.Texto2Temp = ReceberConfig(2, tamanhoTemp, mensagem);
                    Cliente.TempoExibicaoTemp = Convert.ToInt32(Convert.ToChar(ReceberConfig(3, tamanhoTemp, mensagem, true)) - 48);
                }
                /*
                if(mensagem.IndexOf("#gif") >= 0)
                {
                    //VERIFICAR IMPLEMENTAÇÃO DO GIF
                }
                */
                if(mensagem == "#macaddr?")
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
            byte[] comando = Encoding.ASCII.GetBytes(resposta);
            conexao.Send(comando);
            Console.WriteLine(comando);
        }
        public void EnviarProduto(string codBarras)
        {
            if (Conectado)
            {
                byte[] comando = Encoding.ASCII.GetBytes("#" + codBarras);
                conexao.Send(comando);
            }
            else
            {
                MessageBox.Show("Necessário Conectar Antes");
            }
        }
        public string RetornarProduto()
        {
            return mensagem;
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
            if(dhcp == "10")
            {
                Cliente.DHCP = true;
            }
            else
            {
                Cliente.DHCP = false;
            }
        }
    }
}

