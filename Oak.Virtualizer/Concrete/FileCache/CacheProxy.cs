using Oak.Virtualizer.Abstract.FileCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete.FileCache
{
    internal class CacheProxy : ICacheProxy
    {
        IFileProxy _fileProxy;
        List<Segment> _segments;
        List<Block> _blocks;
        List<KeyValuePair<long, byte>> _pendingChanges;

        public CacheProxy(IFileProxy fileProxy, IFileCacheProxy fileCacheProxy, BlockFileConverter blockFileConverter)
        {
            _fileProxy = fileProxy;
            _segments = _fileProxy.GetSegments().ToList();
            _blocks = _fileProxy.GetBlocks(fileCacheProxy, blockFileConverter).ToList();
            _pendingChanges = new List<KeyValuePair<long, byte>>();
        }

        public Segment AllocateAndAppendSegment(bool occupied, int id)
        {
            Segment lastWithId = _segments.FindLast(a => a.BlockID == id);

            if (lastWithId != null)
            {
                return AllocateAndAppendSegment(occupied, id, lastWithId.StartPosition + _fileProxy.GetSegmentSize());
            }
            else
            {
                return AllocateAndAppendSegment(occupied, id, 0);
            }
        }

        public Segment AllocateAndAppendSegment(bool occupied, int id, long startPosition)
        {
            Segment allocatedSegment = _fileProxy.AllocateAndAppendSegment(occupied, id, startPosition);
            _segments.Add(allocatedSegment);
            return allocatedSegment;
        }

        public Block[] GetBlocks()
        {
            return _blocks.ToArray();
        }

        public int GetBlocksCount()
        {
            return _blocks.Count;
        }

        public IEnumerable<Block> GetBlocksEnumerator()
        {
            foreach (var block in _blocks)
            {
                yield return block;
            }
        }

        public Segment[] GetSegments()
        {
            return _segments.ToArray();
        }

        public int GetSegmentsCount()
        {
            return _segments.Count;
        }

        public IEnumerable<Segment> GetSegmentsEnumerator()
        {
            foreach (var segment in _segments)
            {
                yield return segment;
            }
        }

        public byte ReadByte(long position)
        {
            int index = _pendingChanges.FindIndex(a => a.Key == position);

            if (index == -1)
            {
                return _fileProxy.ReadByte(position);
            }
            else
            {
                return _pendingChanges[index].Value;
            }
        }

        public byte[] ReadBytes(long position, long count)
        {
            byte[] bytes = new byte[count];

            for (int i = 0; i < count; i++)
            {
                byte b = ReadByte(i);

                bytes[i] = b;
            }

            return bytes;
        }

        public int ReadSegmentBlockID(int index)
        {
            long byteToRead = FileCacheProxy.FILE_HEADER_SIZE + index * _fileProxy.GetSegmentSize() + FileCacheProxy.SEGMENT_BLOCK_ID;

            return Convert.ToInt32(ReadBytes(byteToRead, 4));
        }

        public bool ReadSegmentOccupied(int index)
        {
            long byteToRead = FileCacheProxy.FILE_HEADER_SIZE + index * _fileProxy.GetSegmentSize() + FileCacheProxy.SEGMENT_OCCUPIED;

            return Convert.ToBoolean(ReadBytes(byteToRead, 1));
        }

        public void WriteByte(long position, byte value)
        {
            int index = _pendingChanges.FindIndex(a => a.Key == position);

            if (index == -1)
            {
                _pendingChanges.Add(new KeyValuePair<long, byte>(position, value));
            }
            else
            {
                _pendingChanges[index] = new KeyValuePair<long, byte>(position, value);
            }
        }

        public void WriteBytes(long position, byte[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                byte b = values[i];

                WriteByte(position + i, b);
            }
        }

        public void WriteSegmentBlockID(int index, int value)
        {
            long byteToWrite = FileCacheProxy.FILE_HEADER_SIZE + index * _fileProxy.GetSegmentSize() + FileCacheProxy.SEGMENT_BLOCK_ID;

            WriteBytes(byteToWrite, BitConverter.GetBytes(value));
        }

        public void WriteSegmentOccupied(int index, bool value)
        {
            long byteToWrite = FileCacheProxy.FILE_HEADER_SIZE + index * _fileProxy.GetSegmentSize() + FileCacheProxy.SEGMENT_OCCUPIED;

            WriteBytes(byteToWrite, BitConverter.GetBytes(value));
        }

        public void Flush()
        {
            foreach (var pair in _pendingChanges)
            {
                _fileProxy.WriteByte(pair.Key, pair.Value);
            }

            _pendingChanges.Clear();
        }

        public Block AllocateBlock(int id, IFileCacheProxy fileCacheProxy, BlockFileConverter blockFileConverter)
        {
            Block foundBlock = _blocks.Find(a => a.GetId() == id);

            if (foundBlock != null)
            {
                return foundBlock;
            }
            else
            {
                Block newBlock = new Block(id, fileCacheProxy, blockFileConverter);
                _blocks.Add(newBlock);
                return newBlock;
            }
        }

        public void DeallocateBlock(int id)
        {
            Block foundBlock = _blocks.Find(a => a.GetId() == id);

            if (foundBlock != null)
            {
                _blocks.Remove(foundBlock);

                foreach (var segment in GetSegmentsEnumerator())
                {
                    if (segment.BlockID == id)
                    {
                        WriteSegmentOccupied(segment.Index, false);
                    }
                }
            }
        }
    }
}
