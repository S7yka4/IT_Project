using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.FANs;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class FANController : Controller
    {
        private readonly IFANsManager Manager;
        public static Guid AssemblyId;
        private readonly IAssemblyContainerManager ContainerManager;

        public FANController(IFANsManager _manager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _manager;
            ContainerManager = _ContainerManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddFAN(string _Name, string _Img, string _Count, string _Cost, string _Size)
        {

            try
            {   
            var _case = new FAN(_Img, _Name, Convert.ToDouble(_Size), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
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


        [HttpGet]
        public IActionResult Output(Guid id)
        {
            AssemblyId = id;
            ContainerManager.FillContainer(AssemblyId);
            ViewData["id"] = id;
            FanAndAssemblyContainer Result = Manager.GetCompableFANs(ContainerManager.Assembly);
            if (Result.FANs.Count != 0)
                return View(Result);
            else
                return RedirectToAction(nameof(TooMuchFANs));
        }

        public ViewResult TooMuchFANs()
        {
            return View(AssemblyId);
        }



        [HttpPost]
        public async Task<ActionResult> RedactFAN(string _Name, string _Img, string _Count, string _Cost, string _Size, Guid id)
        {
            try
            {
                var tmp = new FAN(_Img, _Name, Convert.ToDouble(_Size), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Redact(id, tmp);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage", "DbChange");
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
    }
}
