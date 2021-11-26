using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Server.Entity;
using System.Linq;
using Server.DAO;
using System;
/**
 * 查询：根据id查询
 * 删除：根据id删除
 * 增加：根据id字段和password字段新建对象
 * 修改：暂无
 */
namespace Server.Service {
    public class AdminService : IService
    {
        public string ServiceName => nameof(AdminService);
        private ServerContext context;
        public AdminService(ServerContext context)
        {
            this.context = context;
        }

        //查询
        public Admin GetAdminById(int id)
        {
            return context.Admins.Where(m => m.AdminId == id).FirstOrDefault();
        }
        //删除
        public bool RemoveAdminById(int id)
        {
            var res = context.Admins.Where(m =>
                    m.AdminId == id).FirstOrDefault();
            if (res == null)
                return false;
            context.Admins.Remove(res);
            context.SaveChanges();
            return true;

        }
        //增加
        public void AddAdmin(string password)
        {
            context.Admins.Add(new Admin() { AdminPassword = password });
            context.SaveChanges();
        }
    }
}