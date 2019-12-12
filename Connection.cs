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

        public string ipServ, porta, nomeCli, ipCli, mascara, gateway, texto1, texto2, texto3, texto4, tempoExibicao;
        int tamanhoIpServ, tamanhoIpCliente, tamanhoMascara, tamanhoNome, tamanhoGateway, tamanhoTexto1, tamanhoTexto2, tamanhoTexto3, tamanhoTexto4;

   
        Socket client;
        private string mensagem = "aguardando...";
        private Thread ComunicacaoThread;

        private void ComunicarServidor()
        {
            do
            {
                Comunicacao();
            } while (true);
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

                    byte[] bytes = new byte[1024];
                    int bytesRec = client.Receive(bytes);
                    mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    Console.WriteLine(mensagem);

                    if (mensagem == "#ok")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#tc502|4.0");
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }
                    if (mensagem == "#live?")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#live");
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#alwayslive")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#alwayslive_ok");
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#checklive")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#checklive_ok");
                        client.Send(comando);
                        Console.WriteLine(comando);
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
                        byte[] comando = Encoding.ASCII.GetBytes("#updconfig" 
                            + Convert.ToChar(tamanhoGateway) + gateway
                            + ";Sem Suporte" 
                            + Convert.ToChar(tamanhoNome) + nomeCli 
                            + ";Sem Suporte;Sem Suporte;Sem Suporte");//Pegar valores do terminal
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#paramconfig?")
                    //Pegar valores do terminal #paramconfig00 = para ipfixo e #paramconfig10 = para ipdinamico
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#paramconfig00");
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#config02?") //Pegar valores do terminal
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#config02"
                            + Convert.ToChar(tamanhoIpServ) + ipServ
                            + Convert.ToChar(tamanhoIpCliente) + ipCli
                            + Convert.ToChar(tamanhoMascara) + mascara
                            + Convert.ToChar(tamanhoTexto1) + texto1
                            + Convert.ToChar(tamanhoTexto2) + texto2
                            + Convert.ToChar(tamanhoTexto3) + texto3
                            + Convert.ToChar(tamanhoTexto4) + texto4
                            + Convert.ToChar(tempoExibicao)); 
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#config?") //Pegar valores do terminal
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#config"
                            + Convert.ToChar(tamanhoIpServ) + ipServ
                            + Convert.ToChar(tamanhoIpCliente) + ipCli
                            + Convert.ToChar(tamanhoMascara) + mascara
                            + Convert.ToChar(tamanhoTexto1) + texto1
                            + Convert.ToChar(tamanhoTexto2) + texto2
                            + Convert.ToChar(tempoExibicao));
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

                    if (mensagem == "#extconfig?")
                    {
                        byte[] comando = Encoding.ASCII.GetBytes("#extconfig"
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
                        client.Send(comando);
                        Console.WriteLine(comando);
                    }

              
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void DadosCliente(string IpServ, string Porta, string NomeCli, string IpCli, string Mascara, string Gateway, 
            string Texto1, string Texto2, string Texto3, string Texto4, string TempoExibicao)
        {
            ipServ = IpServ;
            porta = Porta;
            nomeCli = NomeCli;
            ipCli = IpCli;
            mascara = Mascara;
            gateway = Gateway;
            texto1 = Texto1;
            texto2 = Texto2;
            texto3 = Texto3;
            texto4 = Texto4;
            tempoExibicao = TempoExibicao;
            tamanhoIpServ = ipServ.Length + 48;
            tamanhoIpCliente = ipCli.Length + 48;
            tamanhoMascara = mascara.Length + 48;
            tamanhoNome = nomeCli.Length + 48;
            tamanhoGateway = gateway.Length + 48;
            tamanhoTexto1 = texto1.Length + 48;
            tamanhoTexto2 = texto2.Length + 48;
            tamanhoTexto3 = texto3.Length + 48;
            tamanhoTexto4 = texto4.Length + 48;
        }

 
        public void EnviarProduto(string codBarras)
        {
            if (Conectado)
            {
        
                byte[] comando = Encoding.ASCII.GetBytes("#" + codBarras);
                client.Send(comando); 
            }
            else
            {
                MessageBox.Show("Necessário Conectar Antes");
               
            }
        }

        public string RetornoProduto()
        {

            return mensagem;
        }
    }
}

