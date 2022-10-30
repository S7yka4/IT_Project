using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class CpuFan:Device
    {

        public double TDP
        { get; set; }
        public string Sockets
        { get; set; }

        public static CpuFan IdealCpuFan
        {
            get
            {
                CpuFan result = new CpuFan();
                result.Sockets = "-";
                result.TDP = 1488;
                return result;
            }
            set
            { }
        }

        public CpuFan(string img, string name, string sockets, double tdp, int count, double cost) : base(name, img, count, cost, "CpuFan")
        {
            TDP = tdp;
            Sockets = sockets;
        }

        public CpuFan():base("CpuFan")
        {
            TDP = 0;
            Sockets = "";
        }
    }
}
