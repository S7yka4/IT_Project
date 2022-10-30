using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers
{
    public interface ICPUsManager
    {
        public Task<int> Add(CPU tmp);
        public List<CPU> GetAll();
        public Task<int> Delete(Guid id);
        public Task<int> Redact(Guid id, CPU tmp);
        public List<CPU> Search(string Word);
        public List<CPU> OrderBy(string Field);
        public CPU GetById(Guid id);
        public List<CPU> GetCompableCPU(AssemblyContainer Container);
    }
}
