using Constructor.Storage.Managers;
using Constructor.Storage.Managers.Assemblies;

using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;
using System;

using System.Threading.Tasks;

namespace Constructor.Controllers.DBChangeControllers
{
    public class CpuFanController : Controller
    {
        private readonly ICpuFansManager Manager;
        private readonly IAssemblyContainerManager ContainerManager;
        private readonly IAssembliesManager AssembliesManager;
        public static Guid AssemblyId;

        public CpuFanController(ICpuFansManager _Manager, IAssembliesManager _AssembliesManager, IAssemblyContainerManager _ContainerManager)
        {
            Manager = _Manager;
            AssembliesManager = _AssembliesManager;
            ContainerManager = _ContainerManager;
        }

        [HttpGet]
        public ActionResult Output(Guid Id)
        {
            AssemblyId = Id;
            ViewData["id"] = AssemblyId;
            ContainerManager.FillContainer(AssemblyId);
            //return View(Manager.GetAll());
            return View(Manager.GetCompableCpuFans(ContainerManager.Assembly));
        }

        [HttpPost]
        async public Task<ActionResult> AddCpuFan(string _Name,string _Img,string _Count, string _Cost,string _TDP, string _Sockets)
        {
            try
            {
                var entity = new CpuFan(_Img,_Name, _Sockets, Convert.ToInt32(_TDP), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Add(entity);
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
        public async Task<ActionResult> RedactCpuFan(string _Name, string _Img, string _Count, string _Cost, string _Sockets,  string _TDP, Guid id)
        {
            try
            {
                var tmp = new CpuFan(_Img, _Name, _Sockets, Convert.ToInt32(_TDP), Convert.ToInt32(_Count), Convert.ToDouble(_Cost));
                await Manager.Redact( tmp,id);
                return RedirectToAction("Output", "DbChange");
            }
            catch
            {
                return RedirectToAction("ErrorPage", "DbChange");
            }
        }

        [HttpPost]
        async public Task<IActionResult> EditAssembly(Guid CpuFanId)
        {
            var Assembly = AssembliesManager.Find(AssemblyId);
            Assembly.CpuFan = CpuFanId;
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
