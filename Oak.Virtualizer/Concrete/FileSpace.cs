using Oak.Virtualizer.Abstract.FileCache;
using Oak.Virtualizer.Concrete.FileCache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete
{
    /// <summary>
    /// Represents a file, containing Blocks of virtual memory
    /// </summary>
    public class FileSpace : IDisposable
    {
        IFileCacheProxy _fileCacheProxy;
        BlockFileConverter _blockFileConverter;

        //Statics
        public static FileSpace Create(string filePath, long segmentSize)
        {
            using (var fileStream = File.Create(filePath))
            {
                fileStream.WriteByte(0);

                fileStream.Seek(1, SeekOrigin.Begin);
                fileStream.Write(BitConverter.GetBytes(segmentSize), 0, 8);
            }

            return Load(filePath);
        }
        public static FileSpace Load(string filePath)
        {
            return new FileSpace(filePath);
        }

        //Non-statics
        public bool UseCache
        {
            get { return _fileCacheProxy.UseCache; }
            set { _fileCacheProxy.UseCache = value; }
        }

        private FileSpace(string filePath)
        {
            _blockFileConverter = new BlockFileConverter();
            _fileCacheProxy = FileCacheProxy.Create(filePath, _blockFileConverter);
            _blockFileConverter.Load(_fileCacheProxy);
        }

        public Block AllocateBlock(int id)
        {
            return _fileCacheProxy.AllocateBlock(id, _blockFileConverter);
        }
        public Block GetBlock(int id)
        {
            return _fileCacheProxy.GetBlocks(_blockFileConverter).FirstOrDefault(a => a.GetId() == id);
        }
        public void DeallocateBlock(Block block)
        {
            _fileCacheProxy.DeallocateBlock(block);
        }
        public Block[] GetBlocks()
        {
            return _fileCacheProxy.GetBlocks(_blockFileConverter).ToArray();
        }
        public void Flush()
        {
            _fileCacheProxy.Flush();
        }

        void IDisposable.Dispose()
        {
            (_fileCacheProxy as IDisposable).Dispose();
        }
    }
}
