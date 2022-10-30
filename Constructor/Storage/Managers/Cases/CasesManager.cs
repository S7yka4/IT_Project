using Constructor.Storage.Containers;
using Constructor.Storage.Managers.Devices;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Cases
{
    public class CasesManager:ICasesManager
    {
        public readonly DbContent DbContext;
        private readonly IWebHostEnvironment _hostEnviroment;

        public CasesManager(DbContent _DbContext, IWebHostEnvironment hostEnviroment)
        {
            DbContext = _DbContext;
            _hostEnviroment = hostEnviroment;
        }

        public List<Case> GetAll()
        {
            return DbContext.Cases.ToList();
        }

        async public Task<int> Add(Case tmp)
        {
            var entity = new Case( tmp.Img, tmp.Name, tmp.FormFactor, tmp.Fan140Count, tmp.Fan120Count, tmp.Fan90Count, tmp.Drive25Count, tmp.Drive35Count, tmp.Count, tmp.Cost);
            DbContext.Cases.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Redact(Guid id, Case tmp)
        {
            var entity=await DbContext.Cases.FirstOrDefaultAsync(c => c.Id==id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.FormFactor = tmp.FormFactor;
            entity.Fan140Count = tmp.Fan140Count;
            entity.Fan120Count = tmp.Fan120Count;
            entity.Fan90Count = tmp.Fan90Count;
            entity.Drive25Count = tmp.Drive25Count;
            entity.Drive35Count = tmp.Drive35Count;

            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = await DbContext.Cases.FirstOrDefaultAsync(c => c.Id == id);
            DbContext.Cases.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<Case> Search(string Word)
        {
            var Cases = DbContext.Cases.ToList();
            for(int i=0; i<Cases.Count-1;i++)
            {
                if ((Cases[i].Name != Word) && (Cases[i].FormFactor != Word) && (Cases[i].Fan140Count != Convert.ToInt32(Word)) && (Cases[i].Fan120Count != Convert.ToInt32(Word)) && (Cases[i].Fan90Count != Convert.ToInt32(Word)) && (Cases[i].Drive25Count != Convert.ToInt32(Word)) && (Cases[i].Drive35Count != Convert.ToInt32(Word)) && (Cases[i].Count != Convert.ToInt32(Word)) && (Cases[i].Cost != Convert.ToInt32(Word)))
                    Cases.Remove(Cases[i]);    
            }
            return Cases;
        }

        public void Change(int i,List<Case> Cases)
        {
            var tmp = new Case();
            tmp = Cases[i];
            Cases[i] = Cases[i + 1];
            Cases[i + 1] = tmp;
        }

        public List<Case> OrderBy(string Field)
        {
            var Cases= DbContext.Cases.ToList();
            if (Field == "Name")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (String.Compare(Cases[i].Name, Cases[i + 1].Name) > 0)
                            Change(i,Cases);
            if (Field == "FormFactor")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (String.Compare(Cases[i].FormFactor, Cases[i + 1].FormFactor) > 0)
                            Change(i, Cases);
            /*if (Field == "FanCount")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (Cases[i].FanCount > Cases[i + 1].FanCount)
                            Change(i, Cases);
            if (Field == "DriveCount")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (Cases[i].DriveCount > Cases[i + 1].DriveCount)
                            Change(i, Cases);
            if (Field == "DriveSize")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (Cases[i].DriveSize > Cases[i + 1].DriveSize)
                            Change(i, Cases);
            if (Field == "FanSize")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (Cases[i].FanSize > Cases[i + 1].FanSize)
                            Change(i, Cases);*/
            if (Field == "Count")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (Cases[i].Count > Cases[i + 1].Count)
                            Change(i, Cases);
            if (Field == "Cost")
                for (int j = 1; j < Cases.Count; j++)
                    for (int i = 0; i < Cases.Count - 1; i++)
                        if (Cases[i].Cost > Cases[i + 1].Cost)
                            Change(i, Cases);
            return Cases;
        }

        public List<Case> GetCompableCases(AssemblyContainer Container)
        {
            var Result = DbContext.Cases.ToList();
            FSP FSP;
            /*if (Container.FANs == null)
                Container.FANs = new List<FAN>();
            if (Container.Drives == null)
                Container.Drives = new List<Drive>();*/
            if (Container.FSP == null)
                FSP = FSP.IdealFSP;
            else
                FSP = Container.FSP;
            Motherboard Motherboard;
            if (Container.Motherboard == null)
                Motherboard = Motherboard.IdealMotherboard;
            else
                Motherboard = Container.Motherboard;
            for (int i = 0; i < Result.Count; i++)
                if (Case.CompareFF(FSP.FormFactor, Result[i].FormFactor) || Case.CompareFF(Motherboard.FormFactor, Result[i].FormFactor) || (Container.GetCountOfBusyFanSlots(140) > Result[i].Fan140Count) || (Container.GetCountOfBusyFanSlots(120) > Result[i].Fan120Count) || (Container.GetCountOfBusyFanSlots(90) > Result[i].Fan90Count) || (Container.GetCountOfBusyDriveSlots(3.5) > Result[i].Drive35Count) || (Container.GetCountOfBusyDriveSlots(2.5) > Result[i].Drive25Count))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            return Result;
        }

        public Case GetById(Guid id)
        {
            var entity = DbContext.Cases.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }

    }
}
