using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.Motherboards;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class MotherboardController : Controller
    {
        private readonly IMotherboardsManager Manager;
        private readonly IAssemblyContainerManager ContainerManager;
        public static Guid Id;

        public MotherboardController(IMotherboardsManager _Manager, IAssemblyContainerManager _ContainerManager)
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
            return View(Manager.GetCompableMotherboards(ContainerManager.Assembly));
        }

        [HttpPost]
        public IActionResult EditAssembly(Guid MotherboardId)
        {
            ContainerManager.FillContainer(Id);
            var Assembly= ContainerManager.Downgrade();
            Assembly.Motherboard = MotherboardId;
            return RedirectToAction("AssemblyPage","AssemblyContainer",Assembly);

        }

        [HttpPost]
        public async Task<ActionResult> AddMotherboard(string _Name, string _Img, string _Count, string _Cost, string _Socket, string _Chipset, string _MemoryType, string _ECC,string _RAMCount, string _FANCount, string _DriveCount,  string _FormFactor)
        {
            try
            {
                var _case = new Motherboard(_Img, _Name, _Socket, _Chipset, _MemoryType, _ECC, Convert.ToInt32(_RAMCount), Convert.ToInt32(_FANCount), Convert.ToInt32(_DriveCount), _FormFactor, Convert.ToInt32(_Count), Convert.ToDouble(_Cost));

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
        public async Task<ActionResult> RedactMotherboard(string _Name, string _Img, string _Count, string _Cost, string _Socket, string _Chipset, string _MemoryType, string _ECC, string _RAMCount, string _FANCount, string _DriveCount, string _FormFactor, Guid id)
        {
            try
            {
                var tmp = new Motherboard(_Img, _Name, _Socket, _Chipset, _MemoryType, _ECC, Convert.ToInt32(_RAMCount), Convert.ToInt32(_FANCount), Convert.ToInt32(_DriveCount), _FormFactor, Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
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
