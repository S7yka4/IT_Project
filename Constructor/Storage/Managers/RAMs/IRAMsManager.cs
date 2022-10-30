using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Models;

namespace Constructor.Storage.Managers.RAMs
{
    public interface IRAMsManager
    {
        public Task<int> Add(RAM tmp);
        public Task<int> Delete(Guid id);
        public List<RAM> GetAll();
        public Task<int> Redact(Guid id, RAM tmp);
        public List<RAM> Search(string Word);
        public List<RAM> OrderBy(string Field);
        public RAM GetById(Guid id);
        public RAMAndAssemblyContainer GetCompableRAMs(AssemblyContainer Container);
    }
}
