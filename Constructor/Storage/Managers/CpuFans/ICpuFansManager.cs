using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers
{
    public interface ICpuFansManager
    {
        public Task<int> Add(CpuFan tmp);
        public Task<int> Delete(Guid id);
        public CpuFan GetById(Guid id);
        public List<CpuFan> GetAll();
        public Task<int> Redact(CpuFan tmp,Guid id);
        public List<CpuFan> GetCompableCpuFans(AssemblyContainer Container);
    }
}
