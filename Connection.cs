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

            Comunicacao();

            Conectado = true;
        }

        public void Comunicacao()
        {
            byte[] bytes = new byte[1024];
            int bytesRec = client.Receive(bytes);

            mensagem = Encoding.ASCII.GetString(bytes, 0, bytesRec);


            if (mensagem == "#ok")
            {
                byte[] comando = Encoding.ASCII.GetBytes("#tc406|4.0");
                Console.WriteLine("lalalalala2" + comando);
                client.Send(comando);
                Console.WriteLine("lalalalala1" + comando);
            }
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
