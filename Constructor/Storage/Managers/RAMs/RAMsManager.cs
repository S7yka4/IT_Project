using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.RAMs
{
    public class RAMsManager:IRAMsManager
    {
        private readonly DbContent DbContext;

        public RAMsManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> Add(RAM tmp)
        {
            var entity = new RAM( tmp.Img, tmp.Name, tmp.MemorySize, tmp.MemoryType, tmp.ECC, tmp.Count, tmp.Cost);
            DbContext.RAMs.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<RAM> GetAll()
        {
            return DbContext.RAMs.ToList();
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.RAMs.FirstOrDefault(tmp => tmp.Id == id);
            DbContext.RAMs.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Redact(Guid id, RAM tmp)
        {
            var entity = DbContext.RAMs.FirstOrDefault(C => C.Id == id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.MemorySize = tmp.MemorySize;
            entity.MemoryType = tmp.MemoryType;
            entity.ECC = tmp.ECC;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<RAM> Search(string Word)
        {
            List<RAM> RAMs = DbContext.RAMs.ToList();
            for (int i = 0; i < RAMs.Count - 1; i++)
                if ((RAMs[i].Name != Word) && (RAMs[i].MemorySize != Convert.ToDouble(Word)) && (RAMs[i].MemoryType != Word) && (RAMs[i].ECC != Word) && (RAMs[i].Cost != Convert.ToInt32(Word)))
                    RAMs.Remove(RAMs[i]);
            return RAMs;
        }

        public void Change(int i, List<RAM> RAMs)
        {
            var tmp = new RAM();
            tmp = RAMs[i];
            RAMs[i] = RAMs[i + 1];
            RAMs[i + 1] = tmp;
        }

        public List<RAM> OrderBy(string Field)
        {
            var RAMs = DbContext.RAMs.ToList();
            if (Field == "Name")
                for (int j = 1; j < RAMs.Count; j++)
                    for (int i = 0; i < RAMs.Count - 1; i++)
                        if (String.Compare(RAMs[i].Name, RAMs[i + 1].Name) > 0)
                            Change(i, RAMs);
            if (Field == "MemorySize")
                for (int j = 1; j < RAMs.Count; j++)
                    for (int i = 0; i < RAMs.Count - 1; i++)
                        if (RAMs[i].MemorySize > RAMs[i + 1].MemorySize)
                            Change(i, RAMs);
            if (Field == "MemoryType")
                for (int j = 1; j < RAMs.Count; j++)
                    for (int i = 0; i < RAMs.Count - 1; i++)
                        if (String.Compare(RAMs[i].MemoryType, RAMs[i + 1].MemoryType) > 0)
                            Change(i, RAMs);
            if (Field == "ECC")
                for (int j = 1; j < RAMs.Count; j++)
                    for (int i = 0; i < RAMs.Count - 1; i++)
                        if (String.Compare(RAMs[i].ECC, RAMs[i + 1].ECC) > 0)
                            Change(i, RAMs);
            if (Field == "Count")
                for (int j = 1; j < RAMs.Count; j++)
                    for (int i = 0; i < RAMs.Count - 1; i++)
                        if (RAMs[i].Count > RAMs[i + 1].Count)
                            Change(i, RAMs);
            if (Field == "Cost")
                for (int j = 1; j < RAMs.Count; j++)
                    for (int i = 0; i < RAMs.Count - 1; i++)
                        if (RAMs[i].Cost > RAMs[i + 1].Cost)
                            Change(i, RAMs);
            return RAMs;
        }

        public RAMAndAssemblyContainer GetCompableRAMs(AssemblyContainer Container)
        {
            var Result = DbContext.RAMs.ToList();
            if (Container.Motherboard == null)
                Container.Motherboard = Motherboard.IdealMotherboard;
            if (Container.Rams.Count == 0)
                Container.Rams.Add(RAM.IdealRAM);
            for (int i = 0; i < Result.Count; i++)
                if (((Container.Motherboard.MemoryType != Result[i].MemoryType) && (Container.Motherboard.MemoryType != "-")) || (!Container.FreeRamSlot(Result[i])) || ((Container.Motherboard.ECC != Result[i].ECC) && (Container.Motherboard.ECC != "-")) || ((Result[i].MemoryType != Container.Rams[0].MemoryType) && (Container.Rams[0].MemoryType != "-")))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            return new RAMAndAssemblyContainer(Container.Id,Result);
        }

        public RAM GetById(Guid id)
        {
            var entity = DbContext.RAMs.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }
    }
}
