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
        private bool conectado;
        TcpClient client;
        private NetworkStream saida;
        private BinaryWriter escrever;
        private BinaryReader ler;




        public bool Conectado
        {
            get
            {
                return conectado;
            }

            set
            {
                conectado = value;
            }
        }


        public NetworkStream Saida
        {
            get
            {
                return saida;
            }

            set
            {
                saida = value;
            }
        }

        public BinaryWriter Escrever
        {
            get
            {
                return escrever;
            }

            set
            {
                escrever = value;
            }
        }

        public BinaryReader Ler
        {
            get
            {
                return ler;
            }

            set
            {
                ler = value;
            }
        }

        public void Connect(string IpServer, int Porta)
        {
            client = new TcpClient();
            client.Connect(IpServer, Porta);
            MessageBox.Show("Conexão sucedida");
            Conectado = true;
        }


        public void Disconnect(string IpServer, int Porta)
        {
            client.Close();
            MessageBox.Show("Conexão desfeita");
            Conectado = false;
        }
        public void Comunicacao(string Nome)
        {
            Saida = client.GetStream();
            Escrever = new BinaryWriter(Saida);
            Ler = new BinaryReader(Saida);

            string mensagem = "";
            do
            {
                try
                {
                    mensagem = Ler.ReadString();
                    MessageBox.Show(mensagem, "Mensagem Recebida");
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "ERRO");
                    mensagem = "FIM";
                }
            } while (mensagem != "FIM");
            //Fazer a comunicação de envio e recebimento de dados como está no link
            //https://pt.scribd.com/document/294246137/Comunicacao-via-Socket-Com-C-Sylverio
        }
    }
}
