using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.GPUs;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class GPUController : Controller
    {
        private readonly IGPUsManager Manager;
        public static Guid Id;
        private readonly IAssemblyContainerManager ContainerManager;

        public GPUController(IGPUsManager _Manager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _Manager;
            ContainerManager = _ContainerManager;
        }

        
        [HttpPost]
        public ViewResult Output(Guid _Container)
        {
            Id = _Container;
            ViewData["id"] = Id;
            ContainerManager.FillContainer(Id);
            return View(Manager.GetCompableGPUs(ContainerManager.Assembly));
        }

        [HttpPost]
        public async Task<ActionResult> AddGPU(string _Name, string _Img, string _Count, string _Cost, string _Clock, string _MemorySize, string _MemoryType, string _TDP, string _RecommendFSPPower)
        {
            try
            {
                var _case = new GPU(_Img, _Name, Convert.ToDouble(_Clock), Convert.ToDouble(_MemorySize), _MemoryType, Convert.ToDouble(_TDP), Convert.ToDouble(_RecommendFSPPower), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Add(_case);
                return RedirectToAction("Output","DbChange");
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
        public async Task<ActionResult> RedactGPU(string _Name, string _Img, string _Count, string _Cost, string _Clock, string _MemorySize, string _MemoryType, string _TDP, string _RecommendFSPPower, Guid id)
        {
            try
            {
                var tmp = new GPU(_Img, _Name, Convert.ToDouble(_Clock), Convert.ToDouble(_MemorySize), _MemoryType, Convert.ToDouble(_TDP), Convert.ToDouble(_RecommendFSPPower), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Redact(id, tmp);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage", "DbChange");
            }
        }

        [HttpPost]
        public IActionResult EditAssembly(Guid GPUId)
        {
            ContainerManager.FillContainer(Id);
            Assembly _Assembly = ContainerManager.Downgrade();
            _Assembly.GPU = GPUId;
            return RedirectToAction("AssemblyPage","AssemblyContainer", _Assembly);
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
