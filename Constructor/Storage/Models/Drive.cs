using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class Drive:Device
    {

        public static Drive IdealDrive
        {
            get
            {
                var tmp = new Drive();
                tmp.Size = 2.5;
                return tmp;
            }
        }
        public double Size
        { get; set; }
        public double Volume
        { get; set; }


        public Drive():base("Drive")
        {
            Size = 0;
            Volume = 0;
        }
        public Drive(string img,string name, double size, double volume, int count, double cost):base(name,img,count,cost,"Drive")
        {
            Size = size;
            Volume = volume;
        }
    }
}
