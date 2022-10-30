using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Models;
using Constructor.Storage.Managers.Pairs;

namespace Constructor.Storage.Containers
{
    public class AssemblyContainer
    {
        public  Guid Id
        { get; set; }
        public  Case Case
        { get; set; }
        public CpuFan CpuFan
        { get; set; }
        public  CPU CPU
        { get; set; }
        public  FSP FSP
        { get; set; }
        public GPU GPU
        { get; set; }
        public  Motherboard Motherboard
        { get; set; }
        public  List<FAN> FANs
        { get; set; }
        public List<Drive> Drives
        { get; set; }
        public  List<RAM> Rams
        { get; set; }

        
        public int GetCountOfBusyFanSlots(int Size)
        {
            int Result=0;
            if(FANs!=null)
            foreach (var tmp in FANs)
                if (tmp.Size == Size)
                    Result++;
            return Result;
        }

        public int GetCountOfBusyDriveSlots(double Size)
        {
            int Result = 0;
            if(Drives!=null)
            foreach (var tmp in Drives)
                if (tmp.Size == Size)
                    Result++;
            return Result;
        }

        public bool FreeFanSlot(FAN tmp)
        {
            int count = 1;
            if ((Case != null) && (FANs != null)&&(Motherboard!=null))
            {
                foreach (var c in FANs)
                    if (c.Size == tmp.Size)
                        count++;
                if ((((tmp.Size == 140) && (Case.Fan140Count >= count))|| ((tmp.Size == 120) && (Case.Fan120Count >= count))|| ((tmp.Size == 90) && (Case.Fan90Count >= count)))&&(Motherboard.FANCount>=(FANs.Count+1)))
                    return true;
            }
            return false;
        }

        public bool FreeDriveSlot(Drive tmp)
        {
            int count = 1;
            if ((Case != null) && (Drives != null)&&(Motherboard!=null))
            {
                foreach (var c in Drives)
                    if (c.Size == tmp.Size)
                        count++;
                if ((((tmp.Size == 2.5) && (count <= Case.Drive25Count))|| ((tmp.Size == 3.5) && (count <= Case.Drive35Count)))&&(Motherboard.DriveCount>=(Drives.Count+1)))
                    return true;
                
            }
            return false;

        }
        public bool FreeRamSlot(RAM tmp)
        {
            if ((Motherboard != null) && (Rams != null))
                if (Rams.Count <= Motherboard.RAMCount)
                    return true;
            return false;
        }
    }
}
