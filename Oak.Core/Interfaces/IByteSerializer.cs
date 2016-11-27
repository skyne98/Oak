using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface IByteSerializer
    {
        ITreePackage DeserializePackage(byte[] byteArray);
        byte[] SerializePackage(ITreePackage rootPackage, IByteCompressor compressor = null);
    }
}
