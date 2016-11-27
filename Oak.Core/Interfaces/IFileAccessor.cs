using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface IFileAccessor
    {
        byte[] LoadAll(string path);
        byte[] LoadRegion(string path, long offset, long amount);
        void SaveAll(string path, byte[] byteArray);
        void SaveRegion(string path, byte[] byteArray, long offset, long amount);
    }
}
