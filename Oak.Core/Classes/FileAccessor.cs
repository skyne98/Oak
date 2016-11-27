using Oak.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Classes
{
    public class FileAccessor : IFileAccessor
    {
        public byte[] LoadAll(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }

            return null;
        }

        public byte[] LoadRegion(string path, long offset, long amount)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    long length = amount;
                    byte[] buffer = new byte[length];
                    long count = 0;

                    while ((count = fileStream.Read(buffer, (int)offset, (int)length - (int)offset)) > 0)
                    {
                        offset += count;
                    }

                    return buffer;
                }
            }

            return null;
        }

        public void SaveAll(string path, byte[] byteArray)
        {
            File.WriteAllBytes(path, byteArray);
        }

        public void SaveRegion(string path, byte[] byteArray, long offset, long amount)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fileStream.Write(byteArray, (int)offset, (int)amount);
                }
            }
        }
    }
}
