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
        public NetworkStream Saida { get; set; }
        public BinaryWriter Escrever { get; set; }
        public BinaryReader Ler { get; set; }

        Socket client;

        string mensagemAnterior = "aguardando...";
        private string mensagem = "aguardando...";


        //Inicia a conexão com o servidor
        public void Connect(string IpServer, int Porta)
        {


            // Estabelece a conexão com o servidor via socket. 
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(IpServer);
            IPEndPoint remoteEP = new IPEndPoint(ipaddress, Porta);

            // Cria o TCP/IP socket.  
            client = new Socket(ipaddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

            client.Connect(remoteEP);

            Conectado = true;

            Comunicacao();
        }

        public void Comunicacao()
        {
          //  do
           // {
                byte[] bytes = new byte[1024];
                int bytesRec = client.Receive(bytes);
                mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (mensagem == "#ok")
                {
                    byte[] comando = Encoding.ASCII.GetBytes("#tc507|6.5");
                    client.Send(comando);
                    Comunicacao();
                }

                if (mensagem == "#live?")
                {
                    byte[] comando = Encoding.ASCII.GetBytes("#live?");
                    client.Send(comando);
                    Comunicacao();
                }

           // } while (Conectado == true);
        }

        public void Disconnect()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
            Conectado = false;
        }

                        

        public string MsgServer()
        {
            return mensagem;
        }

    }
}
