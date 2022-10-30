using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.CPUs
{
    public class CPUsManager:ICPUsManager
    {
        private readonly DbContent DbContext;

        public CPUsManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        public List<CPU> GetAll()
        {
            return DbContext.CPUs.ToList();
        }

        async public Task<int> Add(CPU tmp)
        {
            var entity = new CPU( tmp.Img, tmp.Name, tmp.Socket, tmp.Frequency, tmp.ECC, tmp.TDP, tmp.Count, tmp.Cost);
            DbContext.CPUs.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<CPU> GetCompableCPU(AssemblyContainer Container)//+
        {
            var Result = DbContext.CPUs.ToList();
            Motherboard Motherboard;
            FSP FSP;
            GPU GPU;
            CpuFan CpuFan;
            if (Container.Motherboard == null)
                Motherboard = Motherboard.IdealMotherboard;
            else
                Motherboard = Container.Motherboard;
            if (Container.CpuFan == null)
                CpuFan = CpuFan.IdealCpuFan;
            else
                CpuFan = Container.CpuFan;
            if (Container.FSP == null)
                FSP = FSP.IdealFSP;
            else
                FSP = Container.FSP;
            if (Container.GPU == null)
                GPU = GPU.IdealGPU;
            else
                GPU = Container.GPU;
            for (int i = 0; i < Result.Count; i++)
                if ((Result[i].TDP > ((FSP.Output - 20 - 1.25 * GPU.TDP - 5) / 1.25)) || ((Motherboard.Socket != Result[i].Socket) && (Motherboard.Socket != "-"))||((!CpuFan.Sockets.Contains(Result[i].Socket))&&(CpuFan.Sockets!="-")))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            return Result;
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.CPUs.FirstOrDefault(tmp => tmp.Id == id);
            DbContext.CPUs.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Redact(Guid id, CPU tmp)
        {
            var entity = DbContext.CPUs.FirstOrDefault(C => C.Id == id);
            
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.Socket = tmp.Socket;
            entity.Frequency = tmp.Frequency;
            entity.ECC = tmp.ECC;
            entity.TDP = tmp.TDP;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<CPU> Search(string Word)
        {
            List<CPU> CPUs = DbContext.CPUs.ToList();
            for(int i=0; i<CPUs.Count-1;i++)
                if ((CPUs[i].Name != Word) && (CPUs[i].Socket != Word) && (CPUs[i].Frequency != Convert.ToDouble(Word)) && (CPUs[i].ECC != Word) && (CPUs[i].TDP != Convert.ToDouble(Word)) && (CPUs[i].Cost != Convert.ToInt32(Word)))
                    CPUs.Remove(CPUs[i]);
            return CPUs;
        }

        public void Change(int i, List<CPU> CPUs)
        {
            var tmp = new CPU();
            tmp = CPUs[i];
            CPUs[i] = CPUs[i + 1];
            CPUs[i + 1] = tmp;
        }

        public List<CPU> OrderBy(string Field)
        {
            var CPUs = DbContext.CPUs.ToList();
            if (Field == "Name")
                for (int j = 1; j < CPUs.Count; j++)
                    for (int i = 0; i < CPUs.Count - 1; i++)
                        if (String.Compare(CPUs[i].Name, CPUs[i + 1].Name) > 0)
                            Change(i,CPUs);
            if (Field == "Socket")
                for (int j = 1; j < CPUs.Count; j++)
                    for (int i = 0; i < CPUs.Count - 1; i++)
                        if (String.Compare(CPUs[i].Socket, CPUs[i + 1].Socket) > 0)
                            Change(i, CPUs);
            if (Field == "Frequency")
                for (int j = 1; j < CPUs.Count; j++)
                    for (int i = 0; i < CPUs.Count - 1; i++)
                        if (CPUs[i].Frequency > CPUs[i + 1].Frequency)
                            Change(i, CPUs);
            if (Field == "TDP")
                for (int j = 1; j < CPUs.Count; j++)
                    for (int i = 0; i < CPUs.Count - 1; i++)
                        if (CPUs[i].TDP > CPUs[i + 1].TDP)
                            Change(i, CPUs);
            if (Field == "Count")
                for (int j = 1; j < CPUs.Count; j++)
                    for (int i = 0; i < CPUs.Count - 1; i++)
                        if (CPUs[i].Count > CPUs[i + 1].Count)
                            Change(i, CPUs);
            if (Field == "Cost")
                for (int j = 1; j < CPUs.Count; j++)
                    for (int i = 0; i < CPUs.Count - 1; i++)
                        if (CPUs[i].Cost > CPUs[i + 1].Cost)
                            Change(i, CPUs);
            return CPUs;
        }

        public CPU GetById(Guid id)
        {
            var entity = DbContext.CPUs.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }


    }
}
