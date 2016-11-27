using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces.TreeContainer
{
    public interface ITreeContainerFIleSaver
    {
        void Save(string filePath, ITreePackage rootPackage, IByteCompressor compressor = null);
    }
}
