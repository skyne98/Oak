using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface IByteCompressor
    {
        byte[] Compress(byte[] byteArray);
        byte[] Decompress(byte[] byteArray);
    }
}
