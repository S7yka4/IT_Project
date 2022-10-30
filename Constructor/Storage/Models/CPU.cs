using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class CPU:Device
    {
        public string Socket
        { get; set; }
        public double Frequency
        { get; set; }
        public string ECC
        { get; set; }
        public double TDP
        { get; set; }

        public static CPU IdealCPU
        {
            get
            {
                var tmp = new CPU();
                tmp.Socket = "-";
                tmp.TDP = 0;
                return tmp;
            }
        }

        public CPU() : base("CPU")
        {
            Socket = "";
            Frequency = 0;
            ECC = "";
            TDP = 0;
        }

        public CPU( string img, string name, string socket, double frequency,string ecc,double tdp,int count, double cost):base(name,img,count,cost,"CPU")
        {
            Socket = socket;
            Frequency = frequency;
            ECC = ecc;
            TDP = tdp;
        }

        public bool Motherboard_Compatibilty(Motherboard tmp)
        {
            if (tmp.Socket == Socket)
                return true;
            else
                return false;
        }
    }
}
