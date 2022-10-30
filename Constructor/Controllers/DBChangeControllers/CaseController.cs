using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Managers.Assemblies;
//using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.Cases;
using Constructor.Storage.Managers.Devices;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class CaseController : Controller
    {
        private readonly ICasesManager Manager;
        public static Guid AssemblyId;
        private readonly IAssembliesManager AssembliesManager;
        private readonly IAssemblyContainerManager ContainerManager;

        public CaseController(ICasesManager _Manager, IAssembliesManager _AssembliesManager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _Manager;
            AssembliesManager = _AssembliesManager;
            ContainerManager = _ContainerManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddCase(string _Name, string _Img, string _Count, string _Cost, string _FormFactor, string _Fan140Count, string _Fan120Count, string _Fan90Count, string _Drive25Count, string _Drive35Count)
        {
            try
            {
                var _case = new Case(_Img, _Name, _FormFactor, Convert.ToInt32(_Fan140Count), Convert.ToInt32(_Fan120Count), Convert.ToInt32(_Fan90Count), Convert.ToInt32(_Drive25Count), Convert.ToInt32(_Drive35Count), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Add(_case);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage","DbChange");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await Manager.Delete(id);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage", "DbChange");
            }
        }

        [HttpPost]
        public async Task<ActionResult> RedactCase(string _Name, string _Img, string _Count, string _Cost, string _FormFactor, string _Fan140Count, string _Fan120Count, string _Fan90Count, string _Drive25Count, string _Drive35Count, Guid id)
        {
            try
            {
                var tmp = new Case(_Img, _Name, _FormFactor, Convert.ToInt32(_Fan140Count), Convert.ToInt32(_Fan120Count), Convert.ToInt32(_Fan90Count), Convert.ToInt32(_Drive25Count), Convert.ToInt32(_Drive35Count), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Redact(id, tmp);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage","DbChange");
            }
        }
        
        [HttpGet]
        public ViewResult RedactPage(Guid id)
        {
            return View(Manager.GetById(id));
        }

        [HttpGet]
        public ViewResult ShowMore(Guid id)
        {
            return View(Manager.GetById(id));
        }




        [HttpGet]
        public IActionResult Output(Guid _Container)
        {
            AssemblyId = _Container;
            ViewData["id"] = AssemblyId;
            ContainerManager.FillContainer(AssemblyId);
            return View(Manager.GetCompableCases(ContainerManager.Assembly));
        }

        [HttpPost]
        async public Task<IActionResult> EditAssembly(Guid CaseId)
        {
            var Assembly = AssembliesManager.Find(AssemblyId);
            Assembly.Case = CaseId;
            await AssembliesManager.Redact(Assembly);
            return RedirectToAction("AssemblyPage","AssemblyContainer", Assembly);
        }

         /////////////////////////


       /* [HttpPost]
        public ViewResult Output2(Guid _tmp)
        {
            Id = _tmp;
            ViewData["id"] = Id;
            return View(Manager.GetAll());
        }*/

    }
}
