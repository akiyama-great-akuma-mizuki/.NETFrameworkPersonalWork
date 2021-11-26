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
    [Route("user")]
    public class UserController : ControllerBase
    { 
        //业务层依赖
        private readonly IEnumerable<IService> _services;

        public UserController(IEnumerable<IService> services)
        {
            _services = services;
        }

        //修改个人信息
        [HttpPut("modify")]
        public void UserInfomationModify(string uid,string newname,string newpwd)
        {
            UserService userService = _services.First(m =>
                   m.ServiceName == nameof(UserService))
                   as UserService;
            userService.SetUserPassword(uid, newpwd);
            userService.SetUserInfo(uid, newname);
            
        }

        //注销账户
        [HttpDelete("logout")]
        public bool UserLogout(string uid,string password)
        {
            UserService userService = _services.First(m =>
                   m.ServiceName == nameof(UserService))
                   as UserService;

            var res = userService.GetUserById(uid);
            if(res == null || res.UserPassword != password)
            {
                return false;
            }
            userService.RemoveUserById(uid);
            return true;
        }
    }
}