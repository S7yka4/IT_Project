using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Containers
{
    public class FanAndAssemblyContainer
    {
        public Guid Id
        { get; set; }
        public List<FAN> FANs
        { get; set; }

        public FanAndAssemblyContainer(Guid _Id, List<FAN> _FANs)
        {
            Id = _Id;
            FANs = _FANs;
        }

    }
}
