using Oak.Virtualizer.Interfaces.FileSpace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes.FileSpaceClasses
{
    public class FileCreator : IFileCreator
    {
        public void Create(string filePath, int segmentSize)
        {
            using (FileStream fileStream = File.Create(filePath))
            {
                fileStream.WriteByte(0);

                byte[] segmentSizeBytes = BitConverter.GetBytes(segmentSize);

                fileStream.Write(segmentSizeBytes, 0, 4);
            }
        }
    }
}
