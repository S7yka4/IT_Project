using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Containers
{
    public class RAMAndAssemblyContainer
    {
        public Guid AssemblyId
        { get; set; }
        public List<RAM> RAMs
        { get; set; }

        public RAMAndAssemblyContainer(Guid _AssemblyId, List<RAM> _RAMs)
        {
            AssemblyId = _AssemblyId;
            RAMs = _RAMs;
        }
    }
}
