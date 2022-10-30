using Constructor.Storage.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Pairs
{
    public class CAPManager:ICAPManager
    {
       /* private readonly DbContent DbContext;

        public CAPManager(DbContent _DbContext)
        {
            DbContext = _DbContext;
        }
        async public Task<int> Add(CpuFanAndSocketPair tmp)
        {
            var entity = new CpuFanAndSocketPair(tmp.CpuFanId, tmp.SocketId);
            DbContext.CAPs.Add(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }
        async public Task<int> Delete(Guid id)
        {
            var entity = DbContext.CAPs.FirstOrDefault(c => c.Id == id);
            DbContext.CAPs.Remove(entity);
            await DbContext.SaveChangesAsync();
            return 0;
        }
        public CpuFanAndSocketPair GetById(Guid id)
        {
            var entity = DbContext.CAPs.FirstOrDefault(c => c.Id == id);
            return entity;
        }
        public List<CpuFanAndSocketPair> GetAll()
        {
            var entity = DbContext.CAPs.ToList();
            return entity;
        }
        async public Task<int> Redact(CpuFanAndSocketPair tmp)
        {
            var entity = DbContext.CAPs.FirstOrDefault(c => c.Id == tmp.Id);
            entity.CpuFanId = tmp.CpuFanId;
            entity.SocketId = tmp.SocketId;
            await DbContext.SaveChangesAsync();
            return 0;
        }*/
    }
}
