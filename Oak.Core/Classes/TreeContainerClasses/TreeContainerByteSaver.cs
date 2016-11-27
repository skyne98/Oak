using Oak.Core.Interfaces.TreeContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Core.Interfaces;

namespace Oak.Core.Classes.TreeContainerClasses
{
    public class TreeContainerByteSaver : ITreeContainerByteSaver
    {
        IByteSerializer _serializer;

        public TreeContainerByteSaver(IByteSerializer _serializer)
        {
            this._serializer = _serializer;
        }

        public void SaveToArray(out byte[] byteArray, ITreePackage rootPackage, IByteCompressor compressor = null)
        {
            byteArray = _serializer.SerializePackage(rootPackage, compressor);
        }
    }
}
