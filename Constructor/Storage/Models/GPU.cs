using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class GPU:Device
    {
        public double Clock
        { get; set; }
        public double MemorySize
        { get; set; }
        public string MemoryType
        { get; set; }
        public double TDP
        { get; set; }
        public double RecommendFSPPower
        { get; set; }

        public static GPU IdealGPU
        {
            get
            {
                var tmp = new GPU("-", "-", 0, 0, "-", 0, 0, 0, 0);
                return tmp;
            }
        }

        public GPU():base("GPU")
        {
            Clock = 0;
            MemorySize = 0;
            MemoryType = "";
            TDP = 0;
            RecommendFSPPower = 0;
        }

        public GPU(string img,string name,double clock, double memorysize, string memorytype, double tdp,double rfp,int count,double cost):base(name,img,count,cost,"GPU")
        {
            Clock = clock;
            MemorySize = memorysize;
            MemoryType = memorytype;
            TDP = tdp;
            RecommendFSPPower = rfp;
        }
    }
}
