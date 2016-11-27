using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface ITreeNode
    {
        //Getters
        ITreePackage GetParent();
        byte[] GetBytes(IByteCompressor compressor = null);
    }
}
