using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class FAN:Device
    {
        public static FAN IdealFAN
        {
            get
            {
                var tmp = new FAN();
                tmp.Size = 10;
                return tmp;
            }
        }
        public double Size
        { get; set; }

        public FAN():base("FAN")
        {
            Size = 0;
        }
        public FAN(string img,string name,double size, int count, double cost):base(name,img,count,cost,"FAN")
        {
            Size = size;
        }

    }
}
