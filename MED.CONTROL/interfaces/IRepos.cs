using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.CONTROL.interfaces
{
    internal interface IRepos<T>
    {
        T Create();
    }
}
