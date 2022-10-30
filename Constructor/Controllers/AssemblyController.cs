using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructor.Controllers
{
    public class AssemblyController : Controller
    {
        public IAssembliesManager Manager;
        public AssemblyController(IAssembliesManager _Manager)
        {
            Manager = _Manager;
        }
        public IActionResult Output()
        {
            return View(Manager.GetAll());
        }

        async public Task<IActionResult> DeleteAssembly(Guid Id)
        {
            await Manager.Delete(Id);
            return RedirectToAction("DeleteAllPairsWithThisAssembly","Pair", new { AssemblyId = Id });
        }

        [HttpGet]
        async public Task<IActionResult> MakeAssembly()
        {
            Assembly tmp = new Assembly();
            await Manager.Add(tmp);
            return RedirectToAction("AssemblyPage", "AssemblyContainer", tmp);
        }

        [HttpGet]
        public IActionResult TakeAssembly(Guid Id)
        {
            Assembly tmp = Manager.Find(Id);
            return RedirectToAction("AssemblyPage", "AssemblyContainer", tmp);
        }

    }
}
