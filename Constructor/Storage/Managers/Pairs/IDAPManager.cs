using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Models;
using Constructor.Storage.Containers;
namespace Constructor.Storage.Managers.Pairs
{
    public interface IDAPManager
    {
        public Task<int> MakeNewPair(Guid AssemblyId, Guid DriveId);
        public DriveAndAssemblyPair FindPair(Guid AssemblyId, Guid DriveId);
        public Task<int> DeletePair(Guid AssemblyId, Guid DriveId);
        public List<Drive> GetDrivesFromPair(Guid AssemblyId);

        public Task<int> DeleteAllPairsWithAssemblyId(Guid AssemblyId);
        public Task<int> ChangeDriveInPair(Guid AssemblyId, Guid DriveId, Guid NewDriveId);
    }
}
