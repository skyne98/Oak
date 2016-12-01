using Oak.Virtualizer.Abstract.FileCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete
{
    internal class BlockFileConverter
    {
        IFileCacheProxy _fileCacheProxy;

        internal BlockFileConverter(IFileCacheProxy fileCacheProxy)
        {
            _fileCacheProxy = fileCacheProxy;
        }

        Segment FindSegment(long position, Block block)
        {
            throw new NotImplementedException();
        }
        internal void FileToBlock(long position, Block block)
        {
            throw new NotImplementedException();
        }
        internal void BlockToFile(long position, Block block)
        {
            throw new NotImplementedException();
        }
    }
}
