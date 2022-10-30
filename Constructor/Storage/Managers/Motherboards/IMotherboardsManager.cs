using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Models;

namespace Constructor.Storage.Managers.Motherboards
{
    public interface IMotherboardsManager
    {
        public Task<int> Add(Motherboard tmp);
        public List<Motherboard> GetAll();
        public Task<int> Delete(Guid id);
        public Task<int> Redact(Guid id, Motherboard tmp);
        public List<Motherboard> Search(string Word);
        public List<Motherboard> OrderBy(string Field);
        public Motherboard GetById(Guid id);
        public List<Motherboard> GetCompableMotherboards(AssemblyContainer Container);
    }
}
