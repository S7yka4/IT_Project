using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Assemblies
{
    public class AssembliesManager : IAssembliesManager
    {
        private readonly DbContent DbContext;
        public AssembliesManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }
        public List<Assembly> GetAll()
        {
            var Result = DbContext.Assemblies.ToList();
            return Result;
        }

        async public Task<int> Redact(Assembly tmp)
        {
            var entity = Find(tmp.Id);
            entity.Case = tmp.Case;
            entity.CPU = tmp.CPU;
            entity.DriveCount = tmp.DriveCount;
            entity.FANCount = tmp.FANCount;
            entity.FSP = tmp.FSP;
            entity.GPU = tmp.GPU;
            entity.Motherboard = tmp.Motherboard;
            entity.RAMCount = tmp.RAMCount;
            await DbContext.SaveChangesAsync();
            return 0;
        }

         async public Task<int> Add(Assembly tmp)
        {
            var entity = new Assembly(tmp);
            DbContext.Assemblies.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }
        async public Task<int> Delete(Guid AssemblyId)
        {
            var entity = Find(AssemblyId);
            DbContext.Assemblies.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }
        public Assembly Find(Guid AssemblyId)
        {
            var entity = DbContext.Assemblies.FirstOrDefault(c => c.Id == AssemblyId);
            return entity;
        }
    }
}
