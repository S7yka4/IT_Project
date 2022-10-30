using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Assemblies
{
    public class AssemblyContainerManager: IAssemblyContainerManager
    {
        private readonly DbContent DbContext;

        public  AssemblyContainer Assembly
        { get; set; }

        public AssemblyContainerManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }


        async public Task<int> ChangesCheck(Assembly Assembly)
        {
            var tmp = DbContext.Assemblies.FirstOrDefault(c => c.Id == Assembly.Id);
            if (Assembly.Case != tmp.Case)
                tmp.Case = Assembly.Case;
            if (Assembly.CPU != tmp.CPU)
                tmp.CPU = Assembly.CPU;
            if (Assembly.CpuFan != tmp.CpuFan)
                tmp.CPU = Assembly.CPU;
            if (Assembly.DriveCount != tmp.DriveCount)
                tmp.DriveCount = Assembly.DriveCount;
            if (Assembly.FANCount != tmp.FANCount)
                tmp.FANCount = Assembly.FANCount;
            if (Assembly.FSP != tmp.FSP)
                tmp.FSP = Assembly.FSP;
            if (Assembly.GPU != tmp.GPU)
                tmp.GPU = Assembly.GPU;
            if (Assembly.Motherboard != tmp.Motherboard)
                tmp.Motherboard = Assembly.Motherboard;
            if (Assembly.RAMCount != tmp.RAMCount)
                tmp.FANCount = Assembly.FANCount;
            await DbContext.SaveChangesAsync();
            return 0;
        }


        public List<FAN> GetFansFromPair(Guid AssemblyId)
        {
            var tmp = DbContext.FAPairs.Where(c => c.IdOfAssembly == AssemblyId);
            var Ids = new List<Guid>();
            var Result = new List<FAN>();
            foreach (var c in tmp)
                Ids.Add(c.IdOfFan);
            foreach (var c in Ids)
                Result.Add(DbContext.FANs.FirstOrDefault(d => d.Id == c));
            return Result;
        }

        public List<Drive> GetDrivesFromPair(Guid AssemblyId)
        {
            var tmp = DbContext.DAPairs.Where(c => c.IdOfAssembly == AssemblyId);
            var Ids = new List<Guid>();
            var Result = new List<Drive>();
            foreach (var c in tmp)
                Ids.Add(c.IdOfDrive);
            foreach (var c in Ids)
                Result.Add(DbContext.Drives.FirstOrDefault(d => d.Id == c));
            return Result;
        }

        public List<RAM> GetRamsFromPair(Guid AssemblyId)
        {
            var tmp = DbContext.RAPairs.Where(c => c.IdOfAssembly == AssemblyId);
            var Ids = new List<Guid>();
            var Result = new List<RAM>();
            foreach (var c in tmp)
                Ids.Add(c.IdOfRam);
            foreach (var c in Ids)
                Result.Add(DbContext.RAMs.FirstOrDefault(d => d.Id == c));
            return Result;
        }

        public Assembly Downgrade()
        {
            Assembly Result = new Assembly();
            Result.Id = Assembly.Id;
            if (Assembly.Case != null)
                Result.Case = Assembly.Case.Id;
            if (Assembly.CPU != null)
                Result.CPU = Assembly.CPU.Id;
            if (Assembly.CpuFan != null)
                Result.CpuFan = Assembly.CpuFan.Id;
            if (Assembly.Drives != null)
                Result.DriveCount = Assembly.Drives.Count;
            if (Assembly.FANs != null)
                Result.FANCount = Assembly.FANs.Count;
            if (Assembly.FSP != null)
                Result.FSP = Assembly.FSP.Id;
            if (Assembly.GPU != null)
                Result.GPU = Assembly.GPU.Id;
            if (Assembly.Motherboard != null)
                Result.Motherboard = Assembly.Motherboard.Id;
            if (Assembly.Rams != null)
                Result.RAMCount = Assembly.Rams.Count;
            return Result;
        }

        public void FillContainer(Guid Id)
        {
            var tmp = DbContext.Assemblies.FirstOrDefault(c => c.Id == Id);
            var _Assembly = new AssemblyContainer();
            _Assembly.Id = tmp.Id;
            if (tmp.Case != Guid.Empty)
                _Assembly.Case = DbContext.Cases.FirstOrDefault(c => c.Id == tmp.Case);
            if (tmp.CPU!= Guid.Empty)
                _Assembly.CPU = DbContext.CPUs.FirstOrDefault(c => c.Id == tmp.CPU);
            if (tmp.CpuFan != Guid.Empty)
                _Assembly.CpuFan = DbContext.CpuFans.FirstOrDefault(c => c.Id == tmp.CpuFan);
            if (tmp.FSP != Guid.Empty)
                _Assembly.FSP = DbContext.FSPs.FirstOrDefault(c => c.Id == tmp.FSP);
            if (tmp.GPU != Guid.Empty)
                _Assembly.GPU = DbContext.GPUs.FirstOrDefault(c => c.Id == tmp.GPU);
            if (tmp.Motherboard != Guid.Empty)
                _Assembly.Motherboard = DbContext.Motherboards.FirstOrDefault(c => c.Id == tmp.Motherboard);
            _Assembly.Drives = GetDrivesFromPair(Id);
            _Assembly.FANs = GetFansFromPair(Id);
            _Assembly.Rams = GetRamsFromPair(Id);
            Assembly = _Assembly;
        }
    }
}
