using Oak.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Classes
{
    public class ByteSerializer : IByteSerializer
    {
        public ITreePackage DeserializePackage(byte[] byteArray)
        {
            return null;
        }

        public byte[] SerializePackage(ITreePackage rootPackage, IByteCompressor compressor = null)
        {
            return null;
        }
    }
}
