using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Motherboards
{
    public class MotherboardsManager: IMotherboardsManager
    {
        private readonly DbContent DbContext;

        public MotherboardsManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        public List<Motherboard> GetAll()
        {
            return DbContext.Motherboards.ToList();
        }

        async public Task<int> Add(Motherboard tmp)
        {
            var entity = new Motherboard( tmp.Img, tmp.Name, tmp.Socket, tmp.Chipset, tmp.MemoryType, tmp.ECC, tmp.RAMCount,tmp.FANCount,tmp.DriveCount, tmp.FormFactor, tmp.Count, tmp.Cost);
            DbContext.Motherboards.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.Motherboards.FirstOrDefault(tmp => tmp.Id == id);
            DbContext.Motherboards.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }

        async public Task<int> Redact(Guid id, Motherboard tmp)
        {
            var entity = DbContext.Motherboards.FirstOrDefault(C => C.Id == id);
            entity.Name = tmp.Name;
            entity.Img = tmp.Img;
            entity.Socket = tmp.Socket;
            entity.Chipset = tmp.Chipset;
            entity.MemoryType = tmp.MemoryType;
            entity.ECC = tmp.ECC;
            entity.RAMCount = tmp.RAMCount;
            entity.FANCount = tmp.FANCount;
            entity.DriveCount = tmp.DriveCount;
            entity.FormFactor = tmp.FormFactor;
            entity.Count = tmp.Count;
            entity.Cost = tmp.Cost;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<Motherboard> Search(string Word)
        {
            List<Motherboard> Motherboards = DbContext.Motherboards.ToList();
            for (int i = 0; i < Motherboards.Count - 1; i++)
                if ((Motherboards[i].Name != Word) && (Motherboards[i].Socket != Word) && (Motherboards[i].Chipset!= Word) && (Motherboards[i].MemoryType != Word) && (Motherboards[i].ECC != Word) && (Motherboards[i].RAMCount != Convert.ToDouble(Word)) && (Motherboards[i].FormFactor != Word) && (Motherboards[i].Cost != Convert.ToInt32(Word)))
                    Motherboards.Remove(Motherboards[i]);
            return Motherboards;
        }

        public void Change(int i, List<Motherboard> Motherboards)
        {
            var tmp = new Motherboard();
            tmp = Motherboards[i];
            Motherboards[i] = Motherboards[i + 1];
            Motherboards[i + 1] = tmp;
        }

        public List<Motherboard> OrderBy(string Field)
        {
            var Motherboards = DbContext.Motherboards.ToList();
            if (Field == "Name")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (String.Compare(Motherboards[i].Name, Motherboards[i + 1].Name) > 0)
                            Change(i, Motherboards);
            if (Field == "Socket")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (String.Compare(Motherboards[i].Name, Motherboards[i + 1].Name) > 0)
                            Change(i, Motherboards);
            if (Field == "Chipset")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (String.Compare(Motherboards[i].Chipset, Motherboards[i + 1].Chipset)>0)
                            Change(i, Motherboards);
            if (Field == "MemoryType")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (String.Compare(Motherboards[i].MemoryType, Motherboards[i + 1].MemoryType) > 0)
                            Change(i, Motherboards);
            if (Field == "ECC")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (String.Compare(Motherboards[i].ECC , Motherboards[i + 1].ECC)>0)
                            Change(i, Motherboards);
          /*  if (Field == "FANCount")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (Motherboards[i].FANCount> Motherboards[i + 1].FANCount)
                            Change(i, Motherboards);*/
            if (Field == "FormFactor")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (String.Compare(Motherboards[i].FormFactor, Motherboards[i + 1].FormFactor) > 0)
                            Change(i, Motherboards);
            if (Field == "Count")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (Motherboards[i].Count > Motherboards[i + 1].Count)
                            Change(i, Motherboards);
            if (Field == "Cost")
                for (int j = 1; j < Motherboards.Count; j++)
                    for (int i = 0; i < Motherboards.Count - 1; i++)
                        if (Motherboards[i].Cost > Motherboards[i + 1].Cost)
                            Change(i, Motherboards);
            return Motherboards;
        }

        public  List<Motherboard> GetCompableMotherboards(AssemblyContainer Container)
        {
            var Result = DbContext.Motherboards.ToList();
            CPU CPU;
            Case Case;
            CpuFan CpuFan;
            if (Container.FANs == null)
                Container.FANs = new List<FAN>();
            if (Container.Drives == null)
                Container.Drives = new List<Drive>();
            if (Container.Rams.Count == 0)
            {
                Container.Rams = new List<RAM>();
                Container.Rams.Add(RAM.IdealRAM);
            }
            if (Container.CPU == null)
                CPU = CPU.IdealCPU;
            else
                CPU = Container.CPU;
            if (Container.CpuFan == null)
                CpuFan = CpuFan.IdealCpuFan;
            else
                CpuFan = Container.CpuFan;
            if (Container.Case == null)
                Case = Case.IdealCase;
            else
                Case = Container.Case;
            for (int i = 0; i < Result.Count; i++)
                if (((CPU.Socket != Result[i].Socket) && (CPU.Socket != "-")) || (Case.CompareFF(Result[i].FormFactor, Case.FormFactor)) || (Container.Rams.Count > Result[i].RAMCount) || (Container.FANs.Count > Result[i].FANCount) || (Container.Drives.Count > Result[i].DriveCount) || ((Container.Rams[0].MemoryType != Result[i].MemoryType) && (Container.Rams[0].MemoryType != "-"))||((!CpuFan.Sockets.Contains(Result[i].Socket))&&(CpuFan.Sockets!="-")))
                {
                    Result.Remove(Result[i]);
                    i--;
                }
            return Result;
        }

        public Motherboard GetById(Guid id)
        {
            var entity = DbContext.Motherboards.FirstOrDefault(tmp => tmp.Id == id);
            return entity;
        }
    }
}
