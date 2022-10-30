using Microsoft.AspNetCore.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Models;

namespace Constructor.Storage.Managers.Pairs
{
    public interface IFAPManager
    {
        public Task<int> MakeNewPair(Guid AssemblyId, Guid FanId);
        public FanAndAssemblyPair FindPair(Guid AssemblyId, Guid FanId);
        public Task<int> DeletePair(Guid AssemblyId, Guid FanId);
        public List<FAN> GetFansFromPair(Guid AssemblyId);
        public Task<int> ChangeFANInPair(Guid AssemblyId, Guid FanId,Guid NewFanId);
        public Task<int> DeleteAllPairsWithAssemblyId(Guid AssemblyId);


    }
}
