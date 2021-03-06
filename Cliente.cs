﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmuladorTC
{
    class Cliente
    {
        public string Ipserv { get; set; }
        public string Porta { get; set; }
        public string NomeCli { get; set; }
        public string IpCli { get; set; }
        public string MascaraCli { get; set; }
        public string GatewayCli { get; set; }
        public string Texto1 { get; set; }
        public string Texto2 { get; set; }
        public string Texto3 { get; set; }
        public string Texto4 { get; set; }
        public string TempoExibicao { get; set; }
        public bool DHCP { get; set; }
        public bool RecebeConfig { get; set; }
        public string Texto1Temp { get; set; }
        public string Texto2Temp { get; set; }
        public string Mac { get; set; }
        public int TempoExibicaoTemp { get; set; }
        public string ModeloTerminal { get; set; }
        public bool Wifi { get; set; }

        private List<string> debug = new List<string>();

        public List<string> GetDebug()
        {
            return debug;
        }

        public void SetDebug(List<string> value)
        {
            debug = value;
        }

        public int IndiceGif { get; set; }
        public int NumeroLoopsGif { get; set; }
        public int TempoGif { get; set; }
        public int TamanhoQuadroGif { get; set; }
        public bool GifRecebida { get; set; }
        public bool Reconectar { get; set; }

        public byte [] Imagem { get; set; }
        public Char SomarTamanhoStringCom48(string texto)
        {
            return Convert.ToChar(texto.Length + 48);
        }        
        public string EnviarDhcp()
        {
            if (DHCP == true)
            {
                return "10";
            }
            else
            {
                return "00";
            }
        }
    }
}
