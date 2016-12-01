using Oak.Virtualizer.Abstract.FileCache;
using Oak.Virtualizer.Concrete.FileCache;
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

        internal BlockFileConverter() { }

        internal void Load(IFileCacheProxy fileCacheProxy)
        {
            _fileCacheProxy = fileCacheProxy;
        }

        Segment FindSegment(long position, Block block)
        {
            foreach (var segment in _fileCacheProxy.GetSegmentsEnumerator())
            {
                if (segment.Occupied && segment.BlockID == block.GetId())
                {
                    long startPosition = segment.StartPosition;
                    long finishPosition = startPosition + _fileCacheProxy.GetSegmentSize() - 1;

                    if (startPosition <= position && position <= finishPosition)
                    {
                        return segment;
                    }
                }
            }

            return null;
        }
        internal long FileToBlock(long position, Block block)
        {
            throw new NotImplementedException();
        }
        internal long BlockToFile(long position, Block block)
        {
            Segment foundSegment = FindSegment(position, block);

            if (foundSegment != null)
            {
                long positionDifference = position - foundSegment.StartPosition;
                return FileCacheProxy.FILE_HEADER_SIZE + 
                    _fileCacheProxy.GetSegmentSizeWithHeader() * foundSegment.Index + 
                    FileCacheProxy.SEGMENT_HEADER_SIZE + 
                    positionDifference;
            }
            else
            {
                throw new IndexOutOfRangeException("Wrong position");
            }
        }
    }
}
