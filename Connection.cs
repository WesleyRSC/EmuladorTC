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

        public string IpServ, Porta, NomeCli, IpCli, Mascara, Gateway;

        Socket client;
        byte[] bytes;

        string mensagemAnterior = "aguardando...";
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
            } while (mensagem == mensagemAnterior);

            if (mensagem == "#ok")
            {
                byte[] comando = Encoding.ASCII.GetBytes("#tc406|6.5");
                client.Send(comando);
                mensagemAnterior = mensagem;
                Comunicacao();
            }


            if (mensagem == "#updconfig?")
            {
                byte[] comando = Encoding.ASCII.GetBytes("#updconfig;"+ Gateway + ";2;3;4;5");//Pegar valores do terminal
                client.Send(comando);
                Comunicacao();
                mensagemAnterior = mensagem;
            }


            if (mensagem == "#live?")
            {
                byte[] comando = Encoding.ASCII.GetBytes("#live");
                client.Send(comando);
                mensagemAnterior = mensagem;
                Comunicacao();
            }
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

        public void Receber(string IpServ, string Porta, string NomeCli, string IpCli, string Mascara, string Gateway)
        {
            this.IpServ = IpServ;
            this.Porta = Porta;
            this.NomeCli = NomeCli;
            this.IpCli = IpCli;
            this.Mascara = Mascara;
            this.Gateway = Gateway;
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

