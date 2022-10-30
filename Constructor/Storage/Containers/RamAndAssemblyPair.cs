using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Containers
{
    public class RamAndAssemblyPair
    {
        public Guid Id
        { get; set; }
        public Guid IdOfRam
        { get; set; }
        public Guid IdOfAssembly
        { get; set; }

        public RamAndAssemblyPair()
        {
            Id = Guid.NewGuid();
            IdOfAssembly = Guid.Empty;
            IdOfRam = Guid.Empty;
        }
        public RamAndAssemblyPair(Guid AssemblyId, Guid RamId)
        {
            Id = Guid.NewGuid();
            IdOfAssembly = AssemblyId;
            IdOfRam = RamId;
        }
    }
}
