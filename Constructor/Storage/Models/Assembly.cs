using Constructor.Storage.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Models
{
    public class Assembly
    {
        public Guid Id
        { get; set; }
        public Guid Case
        { get; set; }
        public Guid CPU
        { get; set; }
        public Guid CpuFan
        { get; set; }
        public int DriveCount
        { get; set; }
        public int FANCount
        { get; set; }
        public Guid FSP
        { get; set; }
        public Guid GPU
        { get; set; }
        public Guid Motherboard
        { get; set; }
        public int RAMCount
        { get; set; }
        

        public Assembly(Guid _case, Guid cpu, Guid cpufanid,int drivecount, int fancount, Guid fsp, Guid gpu, Guid motherboard, int ramcount)
        {
            
            Id = Guid.NewGuid();
            Case = _case;
            CPU = cpu;
            CpuFan = cpufanid;
            DriveCount = drivecount;
            FANCount = fancount;
            FSP = fsp;
            GPU = gpu;
            Motherboard = motherboard;
            RAMCount = ramcount;

        }

       


        public Assembly()
        {
            Id = Guid.NewGuid();
            Case = Guid.Empty;
            CPU = Guid.Empty;
            CpuFan = Guid.Empty;
            DriveCount = 0;
            FANCount = 0;
            FSP = Guid.Empty;
            GPU = Guid.Empty;
            Motherboard = Guid.Empty;
            RAMCount = 0;
        }

        public Assembly(Assembly tmp)
        {
            Id = tmp.Id;
            Case = tmp.Case;
            CPU = tmp.CPU;
            CpuFan = tmp.CpuFan;
            DriveCount = tmp.DriveCount;
            FANCount = tmp.FANCount;
            FSP = tmp.FSP;
            GPU = tmp.GPU;
            Motherboard = tmp.Motherboard;
            RAMCount = tmp.RAMCount;
        
        }


    }
}
