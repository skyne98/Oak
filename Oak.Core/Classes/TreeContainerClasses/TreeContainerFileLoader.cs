using Oak.Core.Interfaces.TreeContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Core.Interfaces;

namespace Oak.Core.Classes.TreeContainerClasses
{
    public class TreeContainerFileLoader : ITreeContainerFileLoader
    {
        IFileAccessor _accessor;
        IByteSerializer _serializer;

        public TreeContainerFileLoader(IFileAccessor _accessor, IByteSerializer _serializer)
        {
            this._serializer = _serializer;
            this._accessor = _accessor;
        }

        public ITreePackage Load(string filePath)
        {
            byte[] byteArray = _accessor.LoadAll(filePath);

            return _serializer.DeserializePackage(byteArray);
        }
    }
}
