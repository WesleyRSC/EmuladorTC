using System;
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

        public char ConverterAscii(string dado)
        {
            int tamanhoDado = dado.Length + 48;
            return Convert.ToChar(tamanhoDado);
        }
        
    }
}
