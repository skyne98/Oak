using Oak.Virtualizer.Interfaces.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Oak.Virtualizer.Classes.FileIOClasses
{
    public class FileReader : IFileReader
    {
        public byte ReadByte(long position, FileStream fileStream)
        {
            fileStream.Seek(position, SeekOrigin.Begin);
            return (byte)fileStream.ReadByte();
        }
    }
}
