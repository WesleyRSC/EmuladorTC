using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace EmuladorTC
{
    class Connection
    {
        public bool Conectado { get; set; }
        public NetworkStream Saida { get; set; }
        public BinaryWriter Escrever { get; set; }
        public BinaryReader Ler { get; set; }

        Socket client;
        byte[] bytes;

        private string mensagem = "aguardando...";


        //Inicia a conexão com o servidor
        public void Connect(string IpServer, int Porta)
        {


            // Estabelece a conexão com o servidor via socket. 
            IPHostEntry ipHostInfo = Dns.GetHostEntry(IpServer);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Porta);

            // Cria o TCP/IP socket.  
            client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(remoteEP);

            Comunicacao();

            Conectado = true;
        }

        public void Comunicacao()
        {
            bytes = new byte[255];
            int bytesRec = client.Receive(bytes);

            mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            /*
            if (mensagem == "#ok")
            {
                bytes = new byte[10];
                bytes = Encoding.ASCII.GetBytes("#tc406|4.0");
                client.Send(bytes);
            }
            */
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

