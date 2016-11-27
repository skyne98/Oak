using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface IByteCompressorResolver
    {
        IByteCompressor Resolve(byte index);
    }
}
