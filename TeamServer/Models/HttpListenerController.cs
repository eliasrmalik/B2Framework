﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamServer.Models
{
    [Controller]
    public class HttpListenerController : ControllerBase
    {

        public IActionResult HandleImplant()
        {
            return Ok("Success! Listener works");
        }

    }
}