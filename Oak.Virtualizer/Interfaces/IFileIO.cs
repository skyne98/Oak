using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces
{
    public interface IFileIO : IDisposable
    {
        void WriteByte(long position, byte value);
        byte ReadByte(long position);
        FileStream GetFileStream();
    }
}
