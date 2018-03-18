using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyMenu
{
    public interface INetworkFunctions
    {
        void callAsyncPHPScript(string commandline);
    }
}
