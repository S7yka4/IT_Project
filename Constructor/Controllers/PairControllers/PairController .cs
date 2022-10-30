using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage.Managers.Pairs;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers.PairControllers
{
    public class PairController : Controller
    {
        private readonly IDAPManager DAPManager;
        private readonly IRAPManager RAPManager;
        private readonly IFAPManager FAPManager;

        public PairController(IDAPManager _DAPManager, IRAPManager _RAPManager, IFAPManager _FAPManager)
        {
            DAPManager = _DAPManager;
            RAPManager= _RAPManager;
            FAPManager= _FAPManager;
        }

        async public Task<IActionResult> DeleteAllPairsWithThisAssembly(Guid AssemblyId)
        {
            await DAPManager.DeleteAllPairsWithAssemblyId(AssemblyId);
            await FAPManager.DeleteAllPairsWithAssemblyId(AssemblyId);
            await RAPManager.DeleteAllPairsWithAssemblyId(AssemblyId);
            return RedirectToAction("Output", "Assembly");
        }

        [HttpPost]
        async public Task<IActionResult> EditDriveInAssembly(Guid AssemblyId,Guid DriveId)
        {
            await DAPManager.MakeNewPair(AssemblyId, DriveId);
            return RedirectToAction("Output", "Drive", new { Id = AssemblyId });
        }

        [HttpPost]
        async public Task<IActionResult> EditFANInAssembly(Guid AssemblyId, Guid FANId)
        {
            await FAPManager.MakeNewPair(AssemblyId, FANId);
            return RedirectToAction("Output", "FAN", new { Id = AssemblyId });
        }

        [HttpGet]
        async public Task<IActionResult> ChangeDriveInPair(Guid AssemblyId, Guid DriveId)
        {

            await DAPManager.DeletePair(AssemblyId, DriveId);
            return RedirectToAction("Output", "Drive", new { Id = AssemblyId });
        }

        [HttpGet]
        async public Task<IActionResult> ChangeFANInPair(Guid AssemblyId, Guid FANId)
        {

            await FAPManager.DeletePair(AssemblyId, FANId);
            return RedirectToAction("Output", "FAN", new { Id = AssemblyId });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDrivePair(Guid AssemblyId, Guid DriveId)
        {
            await DAPManager.DeletePair(AssemblyId, DriveId);
            return RedirectToAction("TakeAssembly", "Assembly", new { Id = AssemblyId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFANPair(Guid AssemblyId, Guid FANId)
        {
            await FAPManager.DeletePair(AssemblyId, FANId);
            return RedirectToAction("TakeAssembly", "Assembly", new { Id = AssemblyId });
        }

        [HttpPost]
        async public Task<IActionResult> EditRAMInAssembly(Guid AssemblyId, Guid RAMId)
        {
            await RAPManager.MakeNewPair(AssemblyId, RAMId);
            return RedirectToAction("Output", "RAM",  new { _Container = AssemblyId } );
        }

        [HttpPost]
        async public Task<IActionResult> DeleteRAMPair(Guid AssemblyId, Guid RAMId)
        {
            await RAPManager.DeletePair(AssemblyId, RAMId);
            return RedirectToAction("TakeAssembly", "Assembly", new { Id = AssemblyId });
        }

        [HttpGet]
        async public Task<IActionResult> ChangeRAMInPair(Guid AssemblyId, Guid RAMId)
        {
            await RAPManager.DeletePair(AssemblyId, RAMId);
            return RedirectToAction("Output", "RAM", new { _Container = AssemblyId });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
