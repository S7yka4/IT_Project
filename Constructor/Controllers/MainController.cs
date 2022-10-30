using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage;

using Constructor.Storage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Constructor.Controllers
{
    public class MainController : Controller
    {


        public ViewResult ShowInf()
        {
            return View();
        }


        public ViewResult MainPage()
        {
            return View();
        }
    }
}
