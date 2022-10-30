using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Models;

namespace Constructor.Storage.Managers.Pairs
{
    public class DAPManager:IDAPManager
    {
        private readonly DbContent DbContext;

        public DAPManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }

        async public Task<int> MakeNewPair(Guid AssemblyId, Guid DriveId)
        {
            var tmp = new DriveAndAssemblyPair(AssemblyId, DriveId);
            DbContext.DAPairs.Add(tmp);
            await DbContext.SaveChangesAsync();
            return 0;
        }
        public DriveAndAssemblyPair FindPair(Guid AssemblyId, Guid DriveId)
        {
            var tmp = DbContext.DAPairs.FirstOrDefault(tmp => ((tmp.IdOfAssembly == AssemblyId) && (tmp.IdOfDrive == DriveId)));
            return tmp;
        }
        async public Task<int> DeletePair(Guid AssemblyId, Guid DriveId)
        {
            var tmp = FindPair(AssemblyId, DriveId);
            DbContext.DAPairs.Remove(tmp);
            await DbContext.SaveChangesAsync();
            return 0;

        }
        async public Task<int> ChangeDriveInPair(Guid AssemblyId, Guid DriveId, Guid NewDriveId)
        {
            var tmp = FindPair(AssemblyId, DriveId);
            tmp.IdOfDrive = NewDriveId;
            await DbContext.SaveChangesAsync();
            return 0;
        }

        public List<Drive> GetDrivesFromPair(Guid AssemblyId)
        {
            var tmp = DbContext.DAPairs.Where(c => c.IdOfAssembly == AssemblyId);
            var Ids = new List<Guid>();
            var Result = new List<Drive>();
            foreach (var c in tmp)
                Ids.Add(c.IdOfDrive);
            foreach (var c in Ids)
                Result.Add(DbContext.Drives.FirstOrDefault(d => d.Id == c));
            return Result;
        }

        async public Task<int> DeleteAllPairsWithAssemblyId(Guid AssemblyId)
        {
            DriveAndAssemblyPair tmp;
            do
            {
                tmp = DbContext.DAPairs.FirstOrDefault(c => c.IdOfAssembly == AssemblyId);
                if (tmp != null)
                {
                    DbContext.DAPairs.Remove(tmp);
                    await DbContext.SaveChangesAsync();
                }
            } while (tmp != null);
            
            return 0;
        }

    }
}
