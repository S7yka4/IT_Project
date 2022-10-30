using Constructor.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Storage.Managers.Devices
{
    class DevicesManager:IDevicesManager
    {

        private readonly DbContent _DbContext;
        public List<Device> Devices
        { get; set; }

        public DevicesManager(DbContent DbContext)
        {
            var _Devices = new List<Device>();
            _DbContext = DbContext;
            
                foreach (var c in _DbContext.Cases.ToList())
                    _Devices.Add(c);
            foreach (var c in _DbContext.CpuFans.ToList())
                _Devices.Add(c);
            foreach (var c in _DbContext.CPUs.ToList())
                _Devices.Add(c);
            
                foreach (var c in _DbContext.Drives.ToList())
                _Devices.Add(c);
            
                foreach (var c in _DbContext.FANs.ToList())
                _Devices.Add(c);
            
                foreach (var c in _DbContext.FSPs.ToList())
                _Devices.Add(c);
       
                foreach (var c in _DbContext.GPUs.ToList())
                _Devices.Add(c);
           
                foreach (var c in _DbContext.Motherboards.ToList())
                _Devices.Add(c);
           
                foreach (var c in _DbContext.RAMs.ToList())
                _Devices.Add(c);
            Devices = _Devices;
        }



        public Device GetDeviceById(Guid id)
        {
            foreach (var tmp in Devices)
                if (tmp.Id == id)
                    return tmp;
            return null;
        }

        public void OrderByPopularity()
        {
            Device tmp;
            for (int j = 1; j < Devices.Count-1; j++)
                for (int i = 0; i < Devices.Count - 2; i++)
                    if (Devices[i].Popularity > Devices[i + 1].Popularity)
                    {
                        tmp = Devices[i];
                        Devices[i] = Devices[i + 1];
                        Devices[i + 1] = tmp;
                    }

        }
    }
}
