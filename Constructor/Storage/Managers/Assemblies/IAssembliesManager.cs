using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Assemblies
{
    public interface IAssembliesManager
    {
        public List<Assembly> GetAll();
        public Task<int> Add(Assembly tmp);
        public Task<int> Delete(Guid AssemblyId);
        public Assembly Find(Guid AssemblyId);
        public Task<int> Redact(Assembly tmp);

    }
}
