using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Core.Interfaces;
using Oak.Core.Interfaces.TreeContainer;

namespace Oak.Core.Classes.TreeContainerClasses
{
    public class TreeContainerFileSaver : ITreeContainerFIleSaver
    {
        IByteSerializer _serializer;
        IFileAccessor _accessor;

        public TreeContainerFileSaver(IFileAccessor _accessor, IByteSerializer _serializer)
        {
            this._accessor = _accessor;
            this._serializer = _serializer;
        }
        public void Save(string filePath, ITreePackage rootPackage, IByteCompressor compressor = null)
        {
            _accessor.SaveAll(filePath, _serializer.SerializePackage(rootPackage, compressor));
        }
    }
}
