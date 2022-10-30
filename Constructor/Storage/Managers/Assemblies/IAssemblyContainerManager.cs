using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Models;

namespace Constructor.Storage.Managers.Assemblies
{
    public interface IAssemblyContainerManager
    {
        public AssemblyContainer Assembly
        { get; set; }


        public void FillContainer(Guid id);

        public Task<int> ChangesCheck(Assembly Assembly);
        public Assembly Downgrade();

    }
}
