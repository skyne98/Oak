using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.FileSpace
{
    public interface IFileCreator
    {
        void Create(string filePath, int segmentSize);
    }
}
