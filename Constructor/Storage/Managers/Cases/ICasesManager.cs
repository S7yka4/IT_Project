using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Models;

namespace Constructor.Storage.Managers.Cases
{
    public interface ICasesManager
    {
        public List<Case> GetAll();
        public Task<int> Add(Case tmp);
        public Task<int> Delete(Guid id);
        public Task<int> Redact(Guid id, Case tmp);
        public List<Case> Search(string Word);
        public List<Case> OrderBy(string Field);
        public Case GetById(Guid id);
        public List<Case> GetCompableCases(AssemblyContainer Container);
    }
}
