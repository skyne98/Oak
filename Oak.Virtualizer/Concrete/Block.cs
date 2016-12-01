using Oak.Virtualizer.Abstract.FileCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete
{
    public class Block
    {
        IFileCacheProxy _fileCacheProxy;
        BlockFileConverter _blockFileConverter;
        int _id;

        internal Block(int id, IFileCacheProxy fileCacheProxy, BlockFileConverter blockFileConverter)
        {
            _id = id;
            _fileCacheProxy = fileCacheProxy;
            _blockFileConverter = blockFileConverter;
        }

        public bool InBounds(long position)
        {
            throw new NotImplementedException();
        }
        public byte ReadByte(long position)
        {
            throw new NotImplementedException();
        }
        public void WriteByte(long position, byte value)
        {
            throw new NotImplementedException();
        }
        public void AllocateSegment()
        {
            throw new NotImplementedException();
        }
        public void DeallocateSegment()
        {
            throw new NotImplementedException();
        }
        public int GetSegmentsCount()
        {
            throw new NotImplementedException();
        }
        public int GetId()
        {
            return _id;
        }
    }
}
