using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Drives
{
    public class DrivesManager:IDrivesManager
    {
        private readonly DbContent DbContext;

        public DrivesManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> Add(Drive tmp)
        {
            var entity = new Drive( tmp.Img, tmp.Name, tmp.Size,tmp.Volume,tmp.Count, tmp.Cost);
            DbContext.Drives.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<Drive> GetAll()
        {
            return DbContext.Drives.ToList();
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.Drives.FirstOrDefault(tmp => tmp.Id == id);
            DbContext.Drives.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Redact(Guid id, Drive tmp)
        {
            var entity = DbContext.Drives.FirstOrDefault(C => C.Id == id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.Size = tmp.Size;
            entity.Volume = tmp.Volume;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<Drive> Search(string Word)
        {
            List<Drive> Drives = DbContext.Drives.ToList();
            for (int i = 0; i < Drives.Count - 1; i++)
                if ((Drives[i].Name != Word) && (Drives[i].Size == Convert.ToInt32(Word))&& (Drives[i].Volume == Convert.ToDouble(Word)) && (Drives[i].Cost != Convert.ToInt32(Word)))
                    Drives.Remove(Drives[i]);
            return Drives;
        }

        public void Change(int i, List<Drive> Drives)
        {
            var tmp = new Drive();
            tmp = Drives[i];
            Drives[i] = Drives[i + 1];
            Drives[i + 1] = tmp;
        }

        public List<Drive> OrderBy(string Field)
        {
            List<Drive> Drives = DbContext.Drives.ToList();
            if (Field == "Name")
                for (int j = 1; j < Drives.Count; j++)
                    for (int i = 0; i < Drives.Count - 1; i++)
                        if (String.Compare(Drives[i].Name, Drives[i + 1].Name) > 0)
                            Change(i,Drives);
            if (Field == "Size")
                for (int j = 1; j < Drives.Count; j++)
                    for (int i = 0; i < Drives.Count - 1; i++)
                        if (Drives[i].Size > Drives[i + 1].Size)
                            Change(i, Drives);
            if (Field == "Volume")
                for (int j = 1; j < Drives.Count; j++)
                    for (int i = 0; i < Drives.Count - 1; i++)
                        if (Drives[i].Volume > Drives[i + 1].Volume)
                            Change(i, Drives);
            if (Field == "Count")
                for (int j = 1; j < Drives.Count; j++)
                    for (int i = 0; i < Drives.Count - 1; i++)
                        if (Drives[i].Count > Drives[i + 1].Count)
                            Change(i, Drives);
            if (Field == "Cost")
                for (int j = 1; j < Drives.Count; j++)
                    for (int i = 0; i < Drives.Count - 1; i++)
                        if (Drives[i].Cost > Drives[i + 1].Cost)
                            Change(i, Drives);
            return Drives;
        }

        public DrivesAndAssemblyContainer GetCompableDrives(AssemblyContainer Container)
        {
            var Result = DbContext.Drives.ToList();
            if (Container.Case == null)
                Container.Case = Case.IdealCase;
            if (Container.Drives == null)
                Container.Drives = new List<Drive>();
            if (Container.Motherboard == null)
                Container.Motherboard = Motherboard.IdealMotherboard;
            for (int i = 0; i < Result.Count; i++)
                if (!Container.FreeDriveSlot(Result[i]))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            Container.Case = null;
            Container.Drives = null;
            return new DrivesAndAssemblyContainer(Container.Id,Result);
        }

        public Drive GetById(Guid id)
        {
            var entity = DbContext.Drives.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }
    }
}
