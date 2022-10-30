using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Containers
{
    public class DrivesAndAssemblyContainer
    {
        public Guid Id
        { get; set; }
        public List<Drive> Drives
        { get; set; }

        public DrivesAndAssemblyContainer(Guid _Id,List<Drive> _Drives)
        {
            Id = _Id;
            Drives = _Drives;
        }
    }
}
