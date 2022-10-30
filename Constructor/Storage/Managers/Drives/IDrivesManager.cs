using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Drives
{
    public interface IDrivesManager
    {
        public Task<int> Add(Drive tmp);
        public List<Drive> GetAll();
        public Task<int> Delete(Guid id);
        public Task<int> Redact(Guid id, Drive tmp);
        public List<Drive> Search(string Word);
        public List<Drive> OrderBy(string Field);
        public Drive GetById(Guid id);
        public DrivesAndAssemblyContainer GetCompableDrives(AssemblyContainer Container);

    }
}
