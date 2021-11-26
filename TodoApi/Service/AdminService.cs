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
 * ��ѯ������id��ѯ
 * ɾ��������idɾ��
 * ���ӣ�����id�ֶκ�password�ֶ��½�����
 * �޸ģ�����
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

        //��ѯ
        public Admin GetAdminById(int id)
        {
            return context.Admins.Where(m => m.AdminId == id).FirstOrDefault();
        }
        //ɾ��
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
        //����
        public void AddAdmin(string password)
        {
            context.Admins.Add(new Admin() { AdminPassword = password });
            context.SaveChanges();
        }
    }
}