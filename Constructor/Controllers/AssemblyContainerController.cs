using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.Pairs;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Controllers
{
    public class AssemblyContainerController : Controller
    {
        public IAssemblyContainerManager Manager;


        public AssemblyContainerController(IAssemblyContainerManager _Manager)
        {
            Manager = _Manager;
        }
        
        async public Task<IActionResult> AssemblyPage(Assembly _Assembly)
        {
            await Manager.ChangesCheck(_Assembly);
            Manager.FillContainer(_Assembly.Id);
            return View(Manager.Assembly);
        }


    }
}
