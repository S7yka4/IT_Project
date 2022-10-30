using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Pairs
{
    public interface IRAPManager
    {
        public Task<int> MakeNewPair(Guid AssemblyId, Guid RamId);
        public RamAndAssemblyPair FindPair(Guid AssemblyId, Guid RamId);
        public Task<int> DeletePair(Guid AssemblyId, Guid RamId);
        public List<RAM> GetRamsFromPair(Guid AssemblyId);
        public Task<int> ChangeRamInPair(Guid AssemblyId, Guid RamId, Guid NewRamId);
        public Task<int> DeleteAllPairsWithAssemblyId(Guid AssemblyId);

    }
}
