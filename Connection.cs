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

        TcpClient client;

        private string mensagem = "TESTE de Mesagem";

        public void Connect(string IpServer, int Porta)
        {
            client = new TcpClient();
            client.Connect(IpServer, Porta);
            Conectado = true;
        }


        public void Disconnect()
        {
            client.Close();
            Conectado = false;
        }

        
        public void Comunicacao()
        {
            Saida = client.GetStream();
            Escrever = new BinaryWriter(Saida);
            Ler = new BinaryReader(Saida);

            mensagem = Ler.ReadString();
            //Fazer a comunicação de envio e recebimento de dados como está no link
            //https://pt.scribd.com/document/294246137/Comunicacao-via-Socket-Com-C-Sylverio
        }
                

        public string MsgServer()
        {
            return mensagem;
        }

    }
}
