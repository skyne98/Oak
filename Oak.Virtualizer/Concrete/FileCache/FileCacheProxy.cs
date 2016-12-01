using Oak.Virtualizer.Abstract.FileCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete.FileCache
{
    internal class FileCacheProxy : IFileCacheProxy, IDisposable
    {
        public const int FILE_HEADER_SIZE = 9;
        public const int SEGMENT_HEADER_SIZE = 17;
        public const int SEGMENT_INDEX = 0;
        public const int SEGMENT_OCCUPIED = 4;
        public const int SEGMENT_BLOCK_ID = 5;
        public const int SEGMENT_START_POSITION = 9;

        bool _useCache = true;
        IFileProxy _fileProxy;
        ICacheProxy _cacheProxy;

        //Statics
        //Factory method
        public static FileCacheProxy Create(IFileProxy fileProxy, ICacheProxy cacheProxy)
        {
            FileCacheProxy fileCacheProxy = new FileCacheProxy();

            fileCacheProxy.Load(fileProxy, cacheProxy);

            return fileCacheProxy;
        }
        public static FileCacheProxy Create(string filePath, BlockFileConverter blockFileConverter)
        {
            FileCacheProxy fileCacheProxy = new FileCacheProxy();

            IFileProxy fileProxy = new FileProxy(filePath);
            ICacheProxy cacheProxy = new CacheProxy(fileProxy, fileCacheProxy, blockFileConverter);

            fileCacheProxy.Load(fileProxy, cacheProxy);

            return fileCacheProxy;
        }

        //Non-statics
        //TODO: Somehow get rid of "Load"
        public void Load(IFileProxy fileProxy, ICacheProxy cacheProxy)
        {
            _fileProxy = fileProxy;
            _cacheProxy = cacheProxy;
        }

        public bool UseCache
        {
            get
            {
                return _useCache;
            }

            set
            {
                _useCache = value;
            }
        }

        public Segment AllocateAndAppendSegment(bool occupied, int id)
        {
            return _cacheProxy.AllocateAndAppendSegment(occupied, id);
        }

        public Segment AllocateAndAppendSegment(bool occupied, int id, long startPosition)
        {
            return _cacheProxy.AllocateAndAppendSegment(occupied, id, startPosition);
        }

        public Block[] GetBlocks(BlockFileConverter blockFileConverter)
        {
            return _cacheProxy.GetBlocks();
        }

        public int GetBlocksCount(BlockFileConverter blockFileConverter)
        {
            return _cacheProxy.GetBlocksCount();
        }

        public IEnumerable<Block> GetBlocksEnumerator(BlockFileConverter blockFileConverter)
        {
            return _cacheProxy.GetBlocksEnumerator();
        }

        public Segment[] GetSegments()
        {
            return _cacheProxy.GetSegments();
        }

        public int GetSegmentsCount()
        {
            return _cacheProxy.GetSegmentsCount();
        }

        public IEnumerable<Segment> GetSegmentsEnumerator()
        {
            return _cacheProxy.GetSegmentsEnumerator();
        }

        public byte ReadByte(long position)
        {
            if (_useCache)
            {
                return _cacheProxy.ReadByte(position);
            }

            return _fileProxy.ReadByte(position);
        }

        public byte[] ReadBytes(long position, long count)
        {
            if (_useCache)
            {
                return _cacheProxy.ReadBytes(position, count);
            }

            return _fileProxy.ReadBytes(position, count);
        }

        public int ReadSegmentBlockID(int index)
        {
            return _cacheProxy.ReadSegmentBlockID(index);
        }

        public bool ReadSegmentOccupied(int index)
        {
            return _cacheProxy.ReadSegmentOccupied(index);
        }

        public void WriteByte(long position, byte value)
        {
            if (_useCache)
            {
                _cacheProxy.WriteByte(position, value);
                return;
            }

            _fileProxy.WriteByte(position, value);
        }

        public void WriteBytes(long position, byte[] values)
        {
            if (_useCache)
            {
                _cacheProxy.WriteBytes(position, values);
                return;
            }

            _fileProxy.WriteBytes(position, values);
        }

        public void WriteSegmentBlockID(int index, int value)
        {
            _cacheProxy.WriteSegmentBlockID(index, value);
        }

        public void WriteSegmentOccupied(int index, bool value)
        {
            _cacheProxy.WriteSegmentOccupied(index, value);
        }

        public void Flush()
        {
            _cacheProxy.Flush();
        }

        public Block AllocateBlock(int id, BlockFileConverter blockFileConverter)
        {
            return _cacheProxy.AllocateBlock(id, this, blockFileConverter);
        }

        public void DeallocateBlock(Block block)
        {
            _cacheProxy.DeallocateBlock(block.GetId());
        }

        public long GetSegmentSize()
        {
            return _fileProxy.GetSegmentSize();
        }

        public long GetSegmentSizeWithHeader()
        {
            return _fileProxy.GetSegmentSizeWithHeader();
        }

        public Segment AllocateSegmentForBlock(Block block)
        {
            Segment blockLastSegment = GetSegments().LastOrDefault(a => a.Occupied && a.BlockID == block.GetId());

            int lastIndex = -1;
            if (blockLastSegment != null)
            {
                lastIndex = blockLastSegment.Index;
            }

            Segment firstAvailableSegment = GetSegments().Where(a => a.Index > lastIndex).FirstOrDefault(a => a.Occupied == false);

            if (firstAvailableSegment == null)
            {
                return AllocateAndAppendSegment(true, block.GetId());
            }

            _cacheProxy.WriteSegmentOccupied(firstAvailableSegment.Index, true);
            _cacheProxy.WriteSegmentBlockID(firstAvailableSegment.Index, block.GetId());

            return firstAvailableSegment;
        }

        public void Dispose()
        {
            (_fileProxy as IDisposable).Dispose();
        }

        public void DeallocateSegmentFromBlock(Block block)
        {
            Segment lastSegment = GetSegments().LastOrDefault(a => a.Occupied && a.BlockID == block.GetId());

            if (lastSegment == null)
            {
                return;
            }

            WriteSegmentOccupied(lastSegment.Index, false);
        }
    }
}
