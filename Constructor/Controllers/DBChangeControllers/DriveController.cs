using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Containers;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.Cases;
using Constructor.Storage.Managers.Drives;
using Constructor.Storage.Managers.Pairs;
using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.DBChangeControllers
{
    public class DriveController : Controller
    {
        private readonly IDrivesManager Manager;
        public static Guid AssemblyId;
        private readonly IAssemblyContainerManager ContainerManager;


        public DriveController(IDrivesManager _manager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _manager;
            ContainerManager = _ContainerManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddDrive(string _Name, string _Img, string _Count, string _Cost, string _Size, string _Volume)
        {
            try
            {
                var _drive = new Drive(_Img, _Name, Convert.ToDouble(_Size), Convert.ToDouble(_Volume), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Add(_drive);
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
        public IActionResult Output(Guid Id)
        {
            AssemblyId = Id;
            ContainerManager.FillContainer(AssemblyId);
            ViewData["id"] = Id;
            DrivesAndAssemblyContainer Result = Manager.GetCompableDrives(ContainerManager.Assembly);
            if (Result.Drives.Count != 0)
                return View(Result);
            else
                return RedirectToAction(nameof(TooMuchDrives));
        }

        public ViewResult TooMuchDrives()
        {
            return View(AssemblyId);
        }

        

        [HttpPost]
        public async Task<ActionResult> RedactDrive(string _Name, string _Img, string _Count, string _Cost, string _Size, string _Volume, Guid id)
        {
            try
            {
                var tmp = new Drive(_Img, _Name, Convert.ToDouble(_Size), Convert.ToDouble(_Volume), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
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