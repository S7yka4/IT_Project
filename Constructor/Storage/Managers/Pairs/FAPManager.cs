using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Pairs
{
    public class FAPManager:IFAPManager
    {
        private readonly DbContent DbContext;

        public FAPManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> MakeNewPair(Guid AssemblyId, Guid FanId)
        {
            var tmp = new FanAndAssemblyPair(AssemblyId, FanId);
            DbContext.FAPairs.Add(tmp);
            await DbContext.SaveChangesAsync();
            return 0;
        }
        public FanAndAssemblyPair FindPair(Guid AssemblyId, Guid FanId)
        {
            var tmp = DbContext.FAPairs.FirstOrDefault(tmp => ((tmp.IdOfAssembly == AssemblyId) && (tmp.IdOfFan == FanId)));
            return tmp;
        }
       async public Task<int> DeletePair(Guid AssemblyId, Guid FanId)
        {
            var tmp = FindPair(AssemblyId, FanId);
            DbContext.FAPairs.Remove(tmp);
            await DbContext.SaveChangesAsync();
            return 0;

        }
        async public Task<int> ChangeFANInPair(Guid AssemblyId, Guid FanId, Guid NewFanId)
        {
            var tmp = FindPair(AssemblyId,FanId);
            tmp.IdOfFan = NewFanId;
            await DbContext.SaveChangesAsync();
            return 0;
        }
        public List<FAN> GetFansFromPair(Guid AssemblyId)
        {
            var tmp = DbContext.FAPairs.Where(c => c.IdOfAssembly == AssemblyId);
            var Ids = new List<Guid>();
            var Result = new List<FAN>();
            foreach (var c in tmp)
                Ids.Add(c.IdOfFan);
            foreach (var c in Ids)
                Result.Add(DbContext.FANs.FirstOrDefault(d => d.Id == c));
            return Result;
        }

        async public Task<int> DeleteAllPairsWithAssemblyId(Guid AssemblyId)
        {
            FanAndAssemblyPair tmp;
            do
            {
                tmp = DbContext.FAPairs.FirstOrDefault(c => c.IdOfAssembly == AssemblyId);
                if (tmp != null)
                {
                    DbContext.FAPairs.Remove(tmp);
                    await DbContext.SaveChangesAsync();
                }
            } while (tmp != null);

            return 0;
        }
    }
}
