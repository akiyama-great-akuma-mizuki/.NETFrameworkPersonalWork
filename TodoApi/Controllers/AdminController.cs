using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Service;
using Server.Entity;

namespace Server.Controller
{
    [ApiController]
    [Route("[admin]")]
    public class AdminController : ControllerBase
    {
        //业务层依赖
        private readonly IEnumerable<IService> _services;

        public AdminController(IEnumerable<IService> services)
        {
            _services = services;
        }
    }
}