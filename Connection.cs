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
                        EnviarDados("#tc502|4.0");                        
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

                    if (mensagem == "#rconf")
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
                        EnviarDados("#paramconfig"+Cliente.RetornarDhcp());
                        Console.WriteLine("#paramconfig" + Cliente.RetornarDhcp());
                    }

                    if (mensagem == "#config02?") //Pegar valores do terminal
                    {
                    
                    
                        EnviarDados("#config02"
                            + Cliente.SomarTamanhoStringCom48(Cliente.Ipserv) +Cliente.Ipserv
                            + Cliente.SomarTamanhoStringCom48(Cliente.IpCli) + Cliente.IpCli
                            + Cliente.SomarTamanhoStringCom48(Cliente.MascaraCli) + Cliente.MascaraCli
                            + Cliente.SomarTamanhoStringCom48(Cliente.Texto1) + Cliente.Texto1
                            + Cliente.SomarTamanhoStringCom48(Cliente.Texto2) + Cliente.Texto2
                            + Cliente.SomarTamanhoStringCom48(Cliente.Texto3) + Cliente.Texto3
                            + Cliente.SomarTamanhoStringCom48(Cliente.Texto4) + Cliente.Texto4
                            + Convert.ToChar(Cliente.TempoExibicao));
                            
                    }

                    if (mensagem == "#config?") //Pegar valores do terminal
                    {
                    
                        EnviarDados("#config"
                            + Cliente.SomarTamanhoStringCom48(Cliente.Ipserv) + Cliente.Ipserv
                            + Cliente.SomarTamanhoStringCom48(Cliente.IpCli) + Cliente.IpCli
                            + Cliente.SomarTamanhoStringCom48(Cliente.MascaraCli) + Cliente.MascaraCli
                            + Cliente.SomarTamanhoStringCom48(Cliente.Texto1) + Cliente.Texto1
                            + Cliente.SomarTamanhoStringCom48(Cliente.Texto2) + Cliente.Texto2
                            + Convert.ToChar(Cliente.TempoExibicao));

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
                            + Convert.ToChar(Cliente.TempoExibicao)
                            + Cliente.RetornarDhcp());                                 // 00 ip fixo, 10 ip dinamico
                            Console.WriteLine("#extconfig" + Cliente.RetornarDhcp());
                }

              
            }catch(Exception e)
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
    }
}

