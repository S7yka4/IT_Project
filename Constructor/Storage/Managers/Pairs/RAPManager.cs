using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Pairs
{
    public class RAPManager:IRAPManager
    {
        private readonly DbContent DbContext;

        public RAPManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> MakeNewPair(Guid AssemblyId, Guid RamId)
        {
            var tmp = new RamAndAssemblyPair(AssemblyId, RamId);
            DbContext.RAPairs.Add(tmp);
            await DbContext.SaveChangesAsync();
            return 0;
        }
        public RamAndAssemblyPair FindPair(Guid AssemblyId, Guid RamId)
        {
            var tmp = DbContext.RAPairs.FirstOrDefault(tmp => ((tmp.IdOfAssembly == AssemblyId) && (tmp.IdOfRam == RamId)));
            return tmp;
        }
        async public Task<int> DeletePair(Guid AssemblyId, Guid RamId)
        {
            var tmp = FindPair(AssemblyId, RamId);
            DbContext.RAPairs.Remove(tmp);
            await DbContext.SaveChangesAsync();
            return 0;

        }
        async public Task<int> ChangeRamInPair(Guid AssemblyId, Guid RamId, Guid NewRamId)
        {
            var tmp = FindPair(AssemblyId, RamId);
            tmp.IdOfRam = NewRamId;
            await DbContext.SaveChangesAsync();
            return 0;
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

        async public Task<int> DeleteAllPairsWithAssemblyId(Guid AssemblyId)
        {
            RamAndAssemblyPair tmp;
            do
            {
                tmp = DbContext.RAPairs.FirstOrDefault(c => c.IdOfAssembly == AssemblyId);
                if (tmp != null)
                {
                    DbContext.RAPairs.Remove(tmp);
                    await DbContext.SaveChangesAsync();
                }
            } while (tmp != null);

            return 0;
        }
    }
}
