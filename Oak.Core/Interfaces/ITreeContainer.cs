using Oak.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Interfaces
{
    public interface ITreeContainer
    {
        //Loaders
        void Load(string filePath);
        void Load(byte[] byteArray);
        void Load(List<byte> byteList);
        void Load(ITreePackage rootNode);

        //Savers
        void SaveToFile(string filePath, IByteCompressor compressor = null);
        void SaveToByteArray(out byte[] byteArray, IByteCompressor compressor = null);
        void SaveToByteList(out List<byte> byteList, IByteCompressor compressor = null);

        //Getters
        ITreePackage GetRootPackage();
    }
}
