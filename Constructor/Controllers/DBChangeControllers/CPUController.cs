using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Managers;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class CPUController : Controller
    {

        private readonly ICPUsManager Manager;
        public static Guid AssemblyId;
        private readonly IAssembliesManager AssembliesManager;
        private readonly IAssemblyContainerManager ContainerManager;

        public CPUController(ICPUsManager _manager, IAssembliesManager _AssembliesManager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _manager;
            AssembliesManager = _AssembliesManager;
            ContainerManager = _ContainerManager;
        }

        [HttpGet]
        public ViewResult Output(Guid Id)
        {
            AssemblyId = Id;
            ViewData["id"] = AssemblyId;
            ContainerManager.FillContainer(AssemblyId);
            return View(Manager.GetCompableCPU(ContainerManager.Assembly));
        }

        

        [HttpPost]
        public async Task<ActionResult> AddCPU(string _Name, string _Img, string _Count, string _Cost, string _Socket, string _Frequency, string _ECC, string _TDP)
        {
            try
            {
                var _case = new CPU(_Img, _Name, _Socket, Convert.ToDouble(_Frequency), _ECC, Convert.ToDouble(_TDP), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Add(_case);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage", "DbChange");
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
        public async Task<ActionResult> RedactCPU(string _Name, string _Img, string _Count, string _Cost, string _Socket, string _Frequency, string _ECC, string _TDP, Guid id)
        {
            try
            {
                var tmp = new CPU(_Img, _Name, _Socket, Convert.ToDouble(_Frequency), _ECC, Convert.ToDouble(_TDP), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Redact(id, tmp);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage", "DbChange");
            }
        }

        [HttpPost]
        async public Task<IActionResult> EditAssembly(Guid CPUId)
        {
            var Assembly = AssembliesManager.Find(AssemblyId);
            Assembly.CPU = CPUId;
            await AssembliesManager.Redact(Assembly);
            return RedirectToAction("AssemblyPage", "AssemblyContainer", Assembly);
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


    }
}
