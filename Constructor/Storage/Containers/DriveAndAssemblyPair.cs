using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Constructor.Storage.Containers
{
    public class DriveAndAssemblyPair
    {
        public Guid Id
        { get; set; }
        public Guid IdOfDrive
        { get; set; }
        public Guid IdOfAssembly
        { get; set; }

        public DriveAndAssemblyPair()
        {
            Id=Guid.NewGuid();
            IdOfDrive = Guid.Empty;
            IdOfAssembly = Guid.Empty;
        }

        public DriveAndAssemblyPair(Guid AssemblyId,Guid DriveId)
        {
            Id = Guid.NewGuid();
            IdOfDrive = DriveId;
            IdOfAssembly = AssemblyId;

        }
    }
}
