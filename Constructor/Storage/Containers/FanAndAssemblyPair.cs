using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Containers
{
    public class FanAndAssemblyPair
    {
        public Guid Id
        { get; set; }
        public Guid IdOfAssembly
        { get; set; }
        public Guid IdOfFan
        { get; set; }

        public FanAndAssemblyPair()
        {
            Id = Guid.NewGuid();
            IdOfAssembly = Guid.Empty;
            IdOfFan = Guid.Empty;
        }
        public FanAndAssemblyPair(Guid AssemblyId, Guid FanId)
        {
            Id = Guid.NewGuid();
            IdOfAssembly = AssemblyId;
            IdOfFan =FanId;
        }
    }
}
