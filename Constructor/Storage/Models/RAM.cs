using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class RAM:Device
    {

        public double MemorySize
        { get; set; }
        public string MemoryType
        { get; set; }
        public string ECC
        { get; set; }

        public static RAM IdealRAM
        {
            get
            {
                RAM tmp = new RAM();
                tmp.MemoryType = "-";
                tmp.ECC = "-";
                return tmp;
            }
        }

        public RAM():base("RAM")
        {
            MemorySize = 0;
            MemoryType = "";
            ECC = "";
        }
        public RAM( string img,string name, double memorysize, string memorytype, string ecc,int count, double cost):base(name,img,count,cost,"RAM")
        {
            MemorySize = memorysize;
            MemoryType = memorytype;
            ECC = ecc;
        }

        public bool Motherboard_Compatibility(Motherboard tmp)
        {
            if (tmp.ECC == ECC)
                return true;
            else
                return false;
        }
    }
}
