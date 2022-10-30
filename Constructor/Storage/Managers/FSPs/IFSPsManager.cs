using Constructor.Storage.Containers;
using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.FSPs
{
    public interface IFSPsManager
    {
        public Task<int> Add(FSP tmp);
        public List<FSP> GetAll();
        public Task<int> Delete(Guid id);
        public Task<int> Redact(Guid id, FSP tmp);
        public List<FSP> Search(string Word);
        public List<FSP> OrderBy(string Field);
        public List<FSP> GetCompableFSPs(AssemblyContainer Container);
        public FSP GetById(Guid id);
    }
}
