using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.FSPs
{
    public class FSPsManager:IFSPsManager
    {
        private readonly DbContent DbContext;

        public FSPsManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> Add(FSP tmp)
        {
            var entity = new FSP( tmp.Img, tmp.Name,tmp.Output,tmp.FormFactor , tmp.Count, tmp.Cost);
            DbContext.FSPs.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<FSP> GetAll()
        {
            return DbContext.FSPs.ToList();
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.FSPs.FirstOrDefault(tmp => tmp.Id == id);
            DbContext.FSPs.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Redact(Guid id, FSP tmp)
        {
            var entity = DbContext.FSPs.FirstOrDefault(C => C.Id == id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.Output=tmp.Output;
            entity.FormFactor = tmp.FormFactor;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<FSP> Search(string Word)
        {
            List<FSP> FSPs = DbContext.FSPs.ToList();
            for (int i = 0; i < FSPs.Count - 1; i++)
                if ((FSPs[i].Name != Word) && (FSPs[i].Output!=Convert.ToDouble(Word))&&(FSPs[i].FormFactor!=Word)&& (FSPs[i].Cost != Convert.ToInt32(Word)))
                    FSPs.Remove(FSPs[i]);
            return FSPs;
        }

        public void Change(int i, List<FSP> FSPs)
        {
            var tmp = new FSP();
            tmp = FSPs[i];
            FSPs[i] = FSPs[i + 1];
            FSPs[i + 1] = tmp;
        }

        public List<FSP> OrderBy(string Field)
        {
            var FSPs = DbContext.FSPs.ToList();
            if (Field == "Name")
                for (int j = 1; j < FSPs.Count; j++)
                    for (int i = 0; i < FSPs.Count - 1; i++)
                        if (String.Compare(FSPs[i].Name, FSPs[i + 1].Name) > 0)
                            Change(i, FSPs);
            if (Field == "Output")
                for (int j = 1; j < FSPs.Count; j++)
                    for (int i = 0; i < FSPs.Count - 1; i++)
                        if (FSPs[i].Output > FSPs[i + 1].Output)
                            Change(i, FSPs);
            if (Field == "FormFactor")
                for (int j = 1; j < FSPs.Count; j++)
                    for (int i = 0; i < FSPs.Count - 1; i++)
                        if (String.Compare(FSPs[i].FormFactor, FSPs[i + 1].FormFactor) > 0)
                            Change(i, FSPs);
            if (Field == "Count")
                for (int j = 1; j < FSPs.Count; j++)
                    for (int i = 0; i < FSPs.Count - 1; i++)
                        if (FSPs[i].Count > FSPs[i + 1].Count)
                            Change(i, FSPs);
            if (Field == "Cost")
                for (int j = 1; j < FSPs.Count; j++)
                    for (int i = 0; i < FSPs.Count - 1; i++)
                        if (FSPs[i].Cost > FSPs[i + 1].Cost)
                            Change(i, FSPs);
            return FSPs;
        }

        public List<FSP> GetCompableFSPs(AssemblyContainer Container)
        {
            var Result = DbContext.FSPs.ToList();
            CPU CPU;
            GPU GPU;
            Case Case;
            if (Container.GPU == null)
                GPU = GPU.IdealGPU;
            else
                GPU = Container.GPU;
            if (Container.CPU == null)
                CPU = CPU.IdealCPU;
            else
                CPU = Container.CPU;
            if (Container.Case == null)
                Case = Case.IdealCase;
            else
                Case = Container.Case;
            for (int i = 0; i < Result.Count; i++)
                if (Case.CompareFF(Result[i].FormFactor, Case.FormFactor) || ((1.25 * (CPU.TDP + GPU.TDP) + 25) > Result[i].Output))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            return Result;
        }

        public FSP GetById(Guid id)
        {
            var entity = DbContext.FSPs.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }
    }
}
