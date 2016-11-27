using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.FileIO
{
    public interface IFileWriter
    {
        void WriteFile(long position, byte value, FileStream fileStream);
    }
}
