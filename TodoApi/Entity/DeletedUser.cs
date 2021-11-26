using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Server.Entity
{
    public class DeletedUser
    {
        private static User userInstance = null; 
        private DeletedUser(){}

        public static User GetInstance()
        {
            if(userInstance == null)
            {
                userInstance = new User();
                userInstance.UserName = "已注销用户";
                userInstance.UserId = "已注销用户";
            }
            return userInstance;
        }
    }
}
