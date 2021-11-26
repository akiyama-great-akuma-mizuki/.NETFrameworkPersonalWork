using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Server.DAO
{
    public class DBContextGenerator
    {
        public static AdminDAO adminDAO { get; set; }
        public static UserDAO userDAO { get; set; }
        public static ServerContext context { get; set; }
    }
}
