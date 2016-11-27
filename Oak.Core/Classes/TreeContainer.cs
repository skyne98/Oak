using Oak.Core.Classes.TreeContainerClasses;
using Oak.Core.Interfaces;
using Oak.Core.Interfaces.TreeContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Core.Classes
{
    public class TreeContainer : ITreeContainer
    {
        ITreePackage _rootPackage;

        //Loaders
        ITreeContainerPackageLoader _packageLoader;
        ITreeContainerByteLoader _byteLoader;
        ITreeContainerFileLoader _fileLoader;

        //Savers
        ITreeContainerByteSaver _byteSaver;
        ITreeContainerFIleSaver _fileSaver;

        //Serializer
        IByteSerializer _byteSerializer;

        //Accessor
        IFileAccessor _fileAccessor;

        public TreeContainer()
        {
            _packageLoader = new TreeContainerPackageLoader();

            _fileAccessor = new FileAccessor();
            _byteSerializer = new ByteSerializer();

            _byteLoader = new TreeContainerByteLoader(_byteSerializer);
            _fileLoader = new TreeContainerFileLoader(_fileAccessor, _byteSerializer);

            _byteSaver = new TreeContainerByteSaver(_byteSerializer);
            _fileSaver = new TreeContainerFileSaver(_fileAccessor, _byteSerializer);
        }

        public ITreePackage GetRootPackage()
        {
            return _rootPackage;
        }

        public void Load(ITreePackage treePackage)
        {
            _rootPackage = _packageLoader.Load(treePackage);
        }

        public void Load(byte[] byteArray)
        {
            _rootPackage = _byteLoader.Load(byteArray);
        }

        public void Load(List<byte> byteList)
        {
            _rootPackage = _byteLoader.Load(byteList.ToArray());
        }

        public void Load(string filePath)
        {
            _rootPackage = _fileLoader.Load(filePath);
        }

        public void SaveToByteArray(out byte[] byteArray, IByteCompressor compressor = null)
        {
            _byteSaver.SaveToArray(out byteArray, _rootPackage, compressor);
        }

        public void SaveToByteList(out List<byte> byteList, IByteCompressor compressor = null)
        {
            byte[] tempArray;
            _byteSaver.SaveToArray(out tempArray, _rootPackage, compressor);

            byteList = tempArray.ToList();
        }

        public void SaveToFile(string filePath, IByteCompressor compressor = null)
        {
            _fileSaver.Save(filePath, _rootPackage, compressor);
        }
    }
}
