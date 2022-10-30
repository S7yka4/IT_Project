using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.RAMs;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class RAMController : Controller
    {
        private readonly IRAMsManager Manager;
        public static Guid Id;
        private readonly IAssemblyContainerManager ContainerManager;

        public RAMController(IRAMsManager _Manager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _Manager;
            ContainerManager = _ContainerManager;
        }


        [HttpGet]
        public IActionResult Output(Guid _Container)
        {
            Id = _Container;
            ViewData["id"] = Id;
            ContainerManager.FillContainer(_Container);
            RAMAndAssemblyContainer Result = Manager.GetCompableRAMs(ContainerManager.Assembly);
            if (Result.RAMs.Count != 0)
                return View(Result);
            else
                return RedirectToAction("TooMuchRAMs","RAM");
        }


        [HttpPost]
        public async Task<ActionResult> AddRAM(string _Name, string _Img, string _Count, string _Cost, string _MemorySize, string _MemoryType, string _ECC)
        {
            try
            {
                var _case = new RAM(_Img, _Name, Convert.ToDouble(_MemorySize), _MemoryType, _ECC, Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
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
        public async Task<ActionResult> RedactRAM(string _Name, string _Img, string _Count, string _Cost, string _MemorySize, string _MemoryType, string _ECC, Guid id)
        {
            try
            {
                var tmp = new RAM(_Img, _Name, Convert.ToDouble(_MemorySize), _MemoryType, _ECC, Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Redact(id, tmp);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage", "DbChange");
            }
        }

        public ViewResult TooMuchRAMs()
        {
            return View(Id);
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
