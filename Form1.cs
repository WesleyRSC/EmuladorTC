using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace EmuladorTC
{

    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }
                Connection Iniciar = new Connection();
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Iniciar.Conectado==false)
                {
                    textConectado.Text = "Cliente ON";
                    botaoConectar.Text = "Desconectar";
                    Iniciar.Connect(ipServidor.Text, int.Parse(porta.Text));
                }
                else
                {
                    textConectado.Text = "Cliente OFF";
                    botaoConectar.Text = "Conectar";
                    Iniciar.Disconnect(ipServidor.Text, int.Parse(porta.Text));
                }

            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Iniciar.Comunicacao(nomeCliente.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
