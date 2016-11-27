using Oak.Virtualizer.Interfaces.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Oak.Virtualizer.Classes.FileIOClasses
{
    public class FileWriter : IFileWriter
    {
        public void WriteFile(long position, byte value, FileStream fileStream)
        {
            fileStream.Seek(position, SeekOrigin.Begin);
            fileStream.WriteByte(value);
        }
    }
}
