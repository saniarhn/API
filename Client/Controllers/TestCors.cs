﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class TestCors : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Belajar()
        {
            return View();
        }
    }
}
