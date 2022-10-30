using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.GPUs
{
    public class GPUsManager:IGPUsManager
    {
        private readonly DbContent DbContext;

        public GPUsManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> Add(GPU tmp)
        {
            var entity = new GPU(tmp.Img, tmp.Name, tmp.Clock, tmp.MemorySize,tmp.MemoryType,tmp.TDP,tmp.RecommendFSPPower, tmp.Count, tmp.Cost);
            DbContext.GPUs.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.GPUs.FirstOrDefault(tmp => tmp.Id == id);
            DbContext.GPUs.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<GPU> GetAll()
        {
            return DbContext.GPUs.ToList();
        }

        async public Task<int> Redact(Guid id, GPU tmp)
        {
            var entity = DbContext.GPUs.FirstOrDefault(C => C.Id == id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.Clock = tmp.Clock;
            entity.MemorySize = tmp.MemorySize;
            entity.MemoryType = tmp.MemoryType;
            entity.TDP = tmp.TDP;
            entity.RecommendFSPPower = tmp.RecommendFSPPower;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<GPU> Search(string Word)
        {
            List<GPU> GPUs = DbContext.GPUs.ToList();
            for (int i = 0; i < GPUs.Count - 1; i++)
                if ((GPUs[i].Name != Word) && (GPUs[i].Clock!=Convert.ToDouble(Word))&& (GPUs[i].MemorySize != Convert.ToDouble(Word))&& (GPUs[i].MemoryType!=Word)&& (GPUs[i].TDP != Convert.ToDouble(Word))&& (GPUs[i].RecommendFSPPower != Convert.ToDouble(Word))&&(GPUs[i].Cost != Convert.ToInt32(Word)))//дописать
                    GPUs.Remove(GPUs[i]);
            return GPUs;
        }

        public void Change(int i, List<GPU> GPUs)
        {
            var tmp = new GPU();
            tmp = GPUs[i];
            GPUs[i] = GPUs[i + 1];
            GPUs[i + 1] = tmp;
        }

        public List<GPU> OrderBy(string Field)
        {
            var GPUs = DbContext.GPUs.ToList();
            if (Field == "Name")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (String.Compare(GPUs[i].Name, GPUs[i + 1].Name) > 0)
                            Change(i, GPUs);
            if (Field == "Clock")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (GPUs[i].Clock > GPUs[i + 1].Clock)
                            Change(i, GPUs);
            if (Field == "MemorySize")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (GPUs[i].MemorySize > GPUs[i + 1].MemorySize)
                            Change(i, GPUs);
            if (Field == "MemoryType")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (String.Compare(GPUs[i].MemoryType, GPUs[i + 1].MemoryType) > 0)
                            Change(i, GPUs);
            if (Field == "TDP")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (GPUs[i].TDP> GPUs[i + 1].TDP)
                            Change(i, GPUs);
            if (Field == "RecommendFSPPower")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (GPUs[i].RecommendFSPPower > GPUs[i + 1].RecommendFSPPower)
                            Change(i, GPUs);
            if (Field == "Count")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (GPUs[i].Count > GPUs[i + 1].Count)
                            Change(i, GPUs);
            if (Field == "Cost")
                for (int j = 1; j < GPUs.Count; j++)
                    for (int i = 0; i < GPUs.Count - 1; i++)
                        if (GPUs[i].Cost > GPUs[i + 1].Cost)
                            Change(i, GPUs);
            return GPUs;
        }

        public List<GPU> GetCompableGPUs(AssemblyContainer Container)
        {
            var Result = DbContext.GPUs.ToList();
            FSP FSP;
            CPU CPU;
            if (Container.FSP == null)
                FSP = FSP.IdealFSP;
            else
                FSP = Container.FSP;
            if (Container.CPU == null)
                CPU = CPU.IdealCPU;
            else
                CPU = Container.CPU;
            for (int i = 0; i < Result.Count; i++)
                if (Result[i].TDP > ((FSP.Output - 25 - 1.25 * CPU.TDP) / 1.25))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            return Result;
        }

        public GPU GetById(Guid id)
        {
            var entity = DbContext.GPUs.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }
    }
}
