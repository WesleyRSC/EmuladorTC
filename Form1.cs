﻿using System;
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

        Connection Conexao = new Connection();      

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Conexao.Conectado==false)
                {
                    textConectado.Text = "Cliente ON";
                    botaoConectar.Text = "Desconectar";
                    Conexao.Connect(ipServidor.Text, int.Parse(porta.Text));
                }
                else
                {
                    textConectado.Text = "Cliente OFF";
                    botaoConectar.Text = "Conectar";
                    Conexao.Disconnect();
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
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            msgRecebida.Text = Conexao.MsgServer();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
