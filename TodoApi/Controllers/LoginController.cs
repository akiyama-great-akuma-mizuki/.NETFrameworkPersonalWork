using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Service;
using Server.Entity;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace Server.Controller
{
    [ApiController]
    [Route("login")]
    public class LoginController
    {
        public class AuthorizationResult
        {
            public bool Isvaild { get; set; }
            public string Token { get; set; }
        }

        public class RegisterResult
        {
            public bool Ok { get; set; }
            public string Uid { get; set; }
        }

        public class TempUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        //业务层依赖
        private readonly IEnumerable<IService> _services;

        public LoginController(IEnumerable<IService> services)
        {
            _services = services;
        }

        [HttpGet("authorize/user")]
        public ActionResult<AuthorizationResult> AuthorizeUser(string id, string pwd)
        {
            UserService userService = _services.First(m =>
                    m.ServiceName == nameof(UserService))
                    as UserService;

            TokenService tokenService = _services.First(m =>
                    m.ServiceName == nameof(TokenService))
                    as TokenService;

            var res = userService.GetUserById(id);
            if (res == null || res.UserPassword != pwd)
            {
                return new BadRequestResult();
            }
            else
            {
                return new AuthorizationResult()
                { Isvaild = true, Token = tokenService.GenerateToken() };
            }
        }


        [HttpPost("register")]
        public ActionResult<RegisterResult> UserLogon(TempUser temp)
        {
            UserService userService = _services.First(m =>
                    m.ServiceName == nameof(UserService))
                    as UserService;

            if (userService.AddUser(temp.UserName, temp.Password))
            {
                return new RegisterResult() { Ok = true, Uid = temp.UserName };

            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}