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

        private string mensagem;


        //Inicia a conexão com o servidor
        public void Connect(string IpServer, int Porta)
        {
            byte[] bytes = new byte[1024];

            // Estabelece a conexão com o servidor via socket. 
            IPHostEntry ipHostInfo = Dns.GetHostEntry(IpServer);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Porta);

            // Cria o TCP/IP socket.  
            client = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

            client.Connect(remoteEP);

            int bytesRec = client.Receive(bytes);

            mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);

            Conectado = true;
        }


        public void Disconnect()
        {
            client.Close();
            Conectado = false;
            mensagem = "";
        }

                        

        public string MsgServer()
        {
            return mensagem;
        }

    }
}
