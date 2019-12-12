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
        private Cliente cliente { get; set; }

        Socket conexao;
        private string mensagem = "aguardando...";
        private Thread ComunicacaoThread;
 

        private void ComunicarServidor()
        {
            do
            {
                Comunicar();
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
                        + cliente.ConverterAscii(cliente.GatewayCli) + cliente.GatewayCli
                        + ";Sem Suporte"
                        + cliente.ConverterAscii(cliente.NomeCli) + cliente.NomeCli 
                        + ";Sem Suporte;Sem Suporte;Sem Suporte");
                    }

                    if (mensagem == "#paramconfig?")
                    //Pegar valores do terminal #paramconfig00 = para ipfixo e #paramconfig10 = para ipdinamico
                    {
                        EnviarDados("#paramconfig00");
                    }

                    if (mensagem == "#config02?") //Pegar valores do terminal
                    {
                    /*
                        EnviarDados("#config02"
                            + Convert.ToChar(tamanhoIpServ) + ipServ
                            + Convert.ToChar(tamanhoIpCliente) + ipCli
                            + Convert.ToChar(tamanhoMascara) + mascara
                            + Convert.ToChar(tamanhoTexto1) + texto1
                            + Convert.ToChar(tamanhoTexto2) + texto2
                            + Convert.ToChar(tamanhoTexto3) + texto3
                            + Convert.ToChar(tamanhoTexto4) + texto4
                            + Convert.ToChar(tempoExibicao));
                            */
                    }

                    if (mensagem == "#config?") //Pegar valores do terminal
                    {
                    /*
                        EnviarDados("#config"
                            + Convert.ToChar(tamanhoIpServ) + ipServ
                            + Convert.ToChar(tamanhoIpCliente) + ipCli
                            + Convert.ToChar(tamanhoMascara) + mascara
                            + Convert.ToChar(tamanhoTexto1) + texto1
                            + Convert.ToChar(tamanhoTexto2) + texto2
                            + Convert.ToChar(tempoExibicao));
                     */
                    }
                    
                    if (mensagem == "#extconfig?")
                    {
                        /*
                        EnviarDados("#extconfig"
                            + Convert.ToChar(tamanhoIpServ) + ipServ
                            + Convert.ToChar(tamanhoIpCliente) + ipCli
                            + Convert.ToChar(tamanhoMascara) + mascara
                            + Convert.ToChar(tamanhoGateway) + gateway
                            + ";Sem Suporte"
                            + Convert.ToChar(tamanhoNome) + nomeCli
                            + Convert.ToChar(tamanhoTexto1) + texto1
                            + Convert.ToChar(tamanhoTexto2) + texto2
                            + ";Sem Suporte;Sem Suporte;Sem Suporte"
                            + Convert.ToChar(tempoExibicao)
                            + "00");                                 // 00 ip fixo, 10 ip dinamico
                            */
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

