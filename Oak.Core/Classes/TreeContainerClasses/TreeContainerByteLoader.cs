using Oak.Core.Interfaces.TreeContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Core.Interfaces;

namespace Oak.Core.Classes.TreeContainerClasses
{
    public class TreeContainerByteLoader : ITreeContainerByteLoader
    {
        IByteSerializer _serializer;

        public TreeContainerByteLoader(IByteSerializer _serializer)
        {
            this._serializer = _serializer;
        }

        public ITreePackage Load(byte[] byteArray)
        {
            return _serializer.DeserializePackage(byteArray);
        }
    }
}
