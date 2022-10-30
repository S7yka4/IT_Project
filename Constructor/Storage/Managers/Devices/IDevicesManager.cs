using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Devices
{
    public interface IDevicesManager
    {

        public List<Device> Devices
        {get; set;}
        //public List<Device> GetAssembly(Assembly tmp);
        public void OrderByPopularity();
        public Device GetDeviceById(Guid id);
    }
}
