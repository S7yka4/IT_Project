using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.GPUs
{
    public interface IGPUsManager
    {
        public Task<int> Add(GPU tmp);
        public Task<int> Delete(Guid id);
        public List<GPU> GetAll();
        public Task<int> Redact(Guid id, GPU tmp);
        public List<GPU> Search(string Word);
        public List<GPU> OrderBy(string Field);
        public List<GPU> GetCompableGPUs(AssemblyContainer Container);
        public GPU GetById(Guid id);
    }
}
