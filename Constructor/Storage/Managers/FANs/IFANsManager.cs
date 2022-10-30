using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.FANs
{
    public interface IFANsManager
    {
        public Task<int> Add(FAN tmp);

        public List<FAN> GetAll();
        public Task<int> Delete(Guid id);
        public Task<int> Redact(Guid id, FAN tmp);
        public List<FAN> Search(string Word);
        public List<FAN> OrderBy(string Field);
        public FAN GetById(Guid id);
        public FanAndAssemblyContainer GetCompableFANs(AssemblyContainer Container);
    }
}
