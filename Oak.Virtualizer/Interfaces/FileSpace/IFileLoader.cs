using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.FileSpace
{
    public interface IFileLoader
    {
        IFileSpace Load(string pathLoader);
    }
}
