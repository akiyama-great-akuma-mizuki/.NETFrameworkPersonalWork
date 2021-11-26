using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Server.Service
{
    public interface IService
    {
        public string ServiceName { get; }
        
    }
}