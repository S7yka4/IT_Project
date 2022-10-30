using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.CpuFans
{
    public class CpuFansManager:ICpuFansManager
    {
        private readonly DbContent DbContext;

        public CpuFansManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> Add(CpuFan tmp)
        {
            var entity = new CpuFan(tmp.Img, tmp.Name, tmp.Sockets, tmp.TDP, tmp.Count, tmp.Cost); 
            DbContext.CpuFans.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Delete(Guid id)
        {
            var tmp = DbContext.CpuFans.FirstOrDefault(c => c.Id == id);
            DbContext.CpuFans.Remove(tmp);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public CpuFan GetById(Guid id)
        {
            var tmp = DbContext.CpuFans.FirstOrDefault(c => c.Id == id);
            return tmp;
        }

        public List<CpuFan> GetAll()
        {
            var tmp = DbContext.CpuFans.ToList();
            return tmp;
        }

        async public Task<int> Redact(CpuFan tmp,Guid id)
        {
            var entity = DbContext.CpuFans.FirstOrDefault(c => c.Id == id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.Sockets = tmp.Sockets;
            entity.TDP = tmp.TDP;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<CpuFan> GetCompableCpuFans(AssemblyContainer Container)
        {
            List<CpuFan> Result = DbContext.CpuFans.ToList();
            Motherboard Motherboard;
            CPU CPU;
            if (Container.Motherboard == null)
                Motherboard = Motherboard.IdealMotherboard;
            else
                Motherboard = Container.Motherboard;
            if (Container.CPU == null)
                CPU = CPU.IdealCPU;
            else
                CPU = Container.CPU;
            
            for (int i = 0; i < Result.Count; i++)
                if ((!String.Concat(Result[i].Sockets, " -").Contains(Motherboard.Socket)) || (!String.Concat(Result[i].Sockets, " -").Contains(CPU.Socket)) || (CPU.TDP > Result[i].TDP))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            return Result;
        }
    }
}
