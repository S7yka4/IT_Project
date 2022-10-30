using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class FSP:Device
    {
        public static FSP IdealFSP
        {
            get
            {
                return new FSP(" "," ",4000,"SFX",0,0);
            }
        }
        public double Output
        { get; set; }
        public string FormFactor
        { get; set; }

        public FSP():base("FSP")
        {
            Output = 0;
            FormFactor = "";
        }
        public FSP( string img,string name,double output,string formfactor, int count, double cost):base(name,img,count,cost,"FSP")
        {
            Output = output;
            FormFactor = formfactor;
        }
        public bool  GPUandCPUandDrive_Compatibility(GPU _gpu, CPU _cpu)
        {
            if (_gpu.TDP + _cpu.TDP + 20 + (_gpu.TDP + _cpu.TDP + 20) * 0.25 < Output)
                return true;
            else
                return false;
        }
    }
}
