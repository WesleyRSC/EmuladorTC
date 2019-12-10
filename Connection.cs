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

        public string ipServ, porta, nomeCli=" ", ipCli, mascara, gateway;

        Socket client;
        private string mensagem = "aguardando...";


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

            Comunicacao();
        }

        public void Comunicacao()
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
                int tamanhoNome=nomeCli.Length;
                byte[] comando = Encoding.ASCII.GetBytes("#updconfig;"+ gateway + ";Sem Suporte"+tamanhoNome+nomeCli+";Sem Suporte;Sem Suporte;Sem Suporte");//Pegar valores do terminal
                client.Send(comando);
                Console.WriteLine(comando);
            }

            if (mensagem == "#paramconfig?")
            {
                byte[] comando = Encoding.ASCII.GetBytes("");//Pegar valores do terminal
                client.Send(comando);
                Console.WriteLine(comando);
            }
            } while (true);
        }

        public void Disconnect()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            Conectado = false;
            mensagem = "";
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
        }

        /*  public void IsLive()
          {
              bytes = new byte[255];
              try
              {
                  client.Receive(bytes);
              }
              catch (Exception)
              {

              }

              int bytesRec=0;
              if (bytesRec>0)
              {
                  mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                  if (mensagem == "#live?")
                  {
                      bytes = new byte[5];
                      bytes = Encoding.ASCII.GetBytes("#live");
                      client.Send(bytes);
                  }
              }*/

    }
}

