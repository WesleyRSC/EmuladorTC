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

        public string ipServ, porta, nomeCli, ipCli, mascara, gateway;
        int tamanhoIpServ, tamanhoIpCliente, tamanhoMascara, tamanhoNome, tamanhoGateway;

        Socket client;
        private string mensagem = "aguardando...";
        private Thread ComunicacaoThread;

        private void ComunicarServidor()
        {
            Comunicacao();
        }
        //Inicia a conexão com o servidor
        public void Connect(string IpServer, int Porta)
        {
            // Estabelece a conexão com o servidor via socket. 
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(IpServer);
            IPEndPoint remoteEP = new IPEndPoint(ipaddress, Porta);

            // Cria o TCP/IP socket.  
            client = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(remoteEP);

            Conectado = true;

            ComunicacaoThread = new Thread(ComunicarServidor);
            ComunicacaoThread.Start();
        }

        public void Disconnect()
        {
            ComunicacaoThread.Abort();
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            Conectado = false;
            mensagem = "";
        }

        public void Comunicacao()
        {
            try
            {
                do
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = client.Receive(bytes);
                    mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine(mensagem);

                    if (mensagem == "#ok")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#tc502|6.5");
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }
                    if (mensagem == "#live?")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#live");
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }
                    if (mensagem == "#updconfig?")
                    { 
                        byte[] comando = Encoding.ASCII.GetBytes("#updconfig" 
                            + Convert.ToChar(tamanhoGateway) + gateway + ";Sem Suporte" 
                            + Convert.ToChar(tamanhoNome) + nomeCli + 
                            ";Sem Suporte;Sem Suporte;Sem Suporte");//Pegar valores do terminal
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#paramconfig?")
                    //Pegar valores do terminal #paramconfig00 = para ipfixo e #paramconfig01 = para ipdinamico
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#paramconfig00");//Pegar valores do terminal #paramconfig00 =
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#config02?")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#config02" 
                            + Convert.ToChar(tamanhoIpServ) + ipServ 
                            + Convert.ToChar(tamanhoIpCliente) + ipCli 
                            + Convert.ToChar(tamanhoMascara) + mascara
                            + ":PASSE DUAS6COISAS8PASSE UM7PRODUTO5"); //Pegar valores do terminal
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }
                    
                } while (true);
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public string MsgServer()
        {
            return mensagem;
        }
        public void DadosCliente(string IpServ, string Porta, string NomeCli, string IpCli, string Mascara, string Gateway)
        {
            ipServ = IpServ;
            porta = Porta;
            nomeCli = NomeCli;
            ipCli = IpCli;
            mascara = Mascara;
            gateway = Gateway;
            tamanhoIpServ = ipServ.Length + 48;
            tamanhoIpCliente = ipCli.Length + 48;
            tamanhoMascara = mascara.Length + 48;
            tamanhoNome = nomeCli.Length + 48;
            tamanhoGateway = gateway.Length + 48;
        }
        public string EnviarProduto(string codBarras)
        {
            if (Conectado)
            {
                string produto;
                byte[] comando = Encoding.ASCII.GetBytes("#" + codBarras);
                client.Send(comando);

                byte[] bytes = new byte[1024];
                int bytesRec = client.Receive(bytes);
                produto = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                return produto;
            }
            else
            {
                MessageBox.Show("Necessário Conectar Antes");
                return "";
            }
        }
    }
}

