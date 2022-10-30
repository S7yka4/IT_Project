using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.FSPs;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class FSPController : Controller
    {
        private readonly IFSPsManager Manager;
        public static Guid Id;
        private readonly IAssemblyContainerManager ContainerManager;

        public FSPController(IFSPsManager _Manager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _Manager;
            ContainerManager = _ContainerManager;
        }



        [HttpPost]
        public ViewResult Output(Guid _Container)
        {
            Id = _Container;
            ViewData["id"] = Id;
            ContainerManager.FillContainer(_Container);
            return View(Manager.GetCompableFSPs(ContainerManager.Assembly));
        }

        [HttpPost]
        public   IActionResult EditAssembly(Guid FSPId)
        {
            ContainerManager.FillContainer(Id);
            var Assembly = ContainerManager.Downgrade();
            Assembly.FSP = FSPId;
            return RedirectToAction("AssemblyPage", "AssemblyContainer", Assembly);
        }

        [HttpPost]
        async public Task<IActionResult>AddFSP(string _Name, string _Img, string _Count, string _Cost, string _Output, string _FormFactor)
        {
            var entity = new FSP(_Img, _Name, Convert.ToDouble(_Output), _FormFactor, Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
            await Manager.Add(entity);
            return RedirectToAction("Output", "DbChange");
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
        public async Task<ActionResult> RedactFSP(string _Name, string _Img, string _Count, string _Cost, string _Output, string _FormFactor, Guid id)
        {
            try
            {
                var tmp = new FSP(_Img, _Name, Convert.ToDouble(_Output), _FormFactor, Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
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
