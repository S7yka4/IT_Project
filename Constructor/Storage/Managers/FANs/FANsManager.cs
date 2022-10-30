using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.FANs
{
    public class FANsManager:IFANsManager
    {
        private readonly DbContent DbContext;

        public FANsManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> Add(FAN tmp)
        {
            var entity = new FAN( tmp.Img, tmp.Name, tmp.Size, tmp.Count, tmp.Cost);
            DbContext.FANs.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<FAN> GetAll()
        {
            return DbContext.FANs.ToList();
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.FANs.FirstOrDefault(tmp => tmp.Id == id);
            DbContext.FANs.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Redact(Guid id, FAN tmp)
        {
            var entity = DbContext.FANs.FirstOrDefault(C => C.Id == id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.Size = tmp.Size;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<FAN> Search(string Word)
        {
            List<FAN> FANs = DbContext.FANs.ToList();
            for (int i = 0; i < FANs.Count - 1; i++)
                if ((FANs[i].Name != Word) && (FANs[i].Size!=Convert.ToDouble(Word)) && (FANs[i].Cost != Convert.ToInt32(Word)))
                    FANs.Remove(FANs[i]);
            return FANs;
        }

        public void Change(int i, List<FAN> FANs)
        {
            var tmp = new FAN();
            tmp = FANs[i];
            FANs[i] = FANs[i + 1];
            FANs[i + 1] = tmp;
        }

        public List<FAN> OrderBy(string Field)
        {
            var FANs = DbContext.FANs.ToList();
            if (Field == "Name")
                for (int j = 1; j < FANs.Count; j++)
                    for (int i = 0; i < FANs.Count - 1; i++)
                        if (String.Compare(FANs[i].Name, FANs[i + 1].Name) > 0)
                            Change(i, FANs);
            if (Field == "Size")
                for (int j = 1; j < FANs.Count; j++)
                    for (int i = 0; i < FANs.Count - 1; i++)
                        if (FANs[i].Count > FANs[i + 1].Count)
                            Change(i, FANs);
            if (Field == "Count")
                for (int j = 1; j < FANs.Count; j++)
                    for (int i = 0; i < FANs.Count - 1; i++)
                        if (FANs[i].Count > FANs[i + 1].Count)
                            Change(i, FANs);
            if (Field == "Cost")
                for (int j = 1; j < FANs.Count; j++)
                    for (int i = 0; i < FANs.Count - 1; i++)
                        if (FANs[i].Cost > FANs[i + 1].Cost)
                            Change(i, FANs);
            return FANs;
        }

        public FanAndAssemblyContainer GetCompableFANs(AssemblyContainer Container)
        {
            var Result = DbContext.FANs.ToList();
            if (Container.Case == null)
                Container.Case = Case.IdealCase;
            if (Container.FANs == null)
                Container.FANs = new List<FAN>();
            if (Container.Motherboard == null)
                Container.Motherboard = Motherboard.IdealMotherboard;
            for (int i = 0; i < Result.Count; i++)
                if (!Container.FreeFanSlot(Result[i]))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            Container.Case = null;
            Container.FANs = null;
            return new FanAndAssemblyContainer(Container.Id,Result);
        }

        public FAN GetById(Guid id)
        {
            var entity = DbContext.FANs.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }
    }
}
