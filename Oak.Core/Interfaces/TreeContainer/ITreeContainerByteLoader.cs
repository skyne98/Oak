using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces.TreeContainer
{
    public interface ITreeContainerByteLoader
    {
        ITreePackage Load(byte[] byteArray);
    }
}
