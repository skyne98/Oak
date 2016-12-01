using Oak.Virtualizer.Abstract.FileCache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete.FileCache
{
    /// <summary>
    /// Controls direct operations with file
    /// </summary>
    internal class FileProxy : IFileProxy, IDisposable
    {
        FileStream _fileStream;
        byte _compressionIndex;
        long _segmentSize;

        public FileProxy(string filePath)
        {
            _fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        }

        public Segment AllocateAndAppendSegment(bool occupied, int id)
        {
            Segment lastWithId = GetSegmentsEnumerator().Last(a => a.BlockID == id);
            long startPosition = 0;

            if (lastWithId != null)
            {
                startPosition = lastWithId.StartPosition + GetSegmentSize();
            }

            return AllocateAndAppendSegment(occupied, id, startPosition);
        }

        public Segment AllocateAndAppendSegment(bool occupied, int id, long startPosition)
        {
            int index = GetSegmentsCount();
            long segmentByte = _fileStream.Length;

            //Write index
            _fileStream.Seek(segmentByte + FileCacheProxy.SEGMENT_INDEX, SeekOrigin.Begin);
            _fileStream.Write(BitConverter.GetBytes(index), 0, 4);

            //Write occupied
            _fileStream.Seek(segmentByte + FileCacheProxy.SEGMENT_OCCUPIED, SeekOrigin.Begin);
            _fileStream.Write(BitConverter.GetBytes(occupied), 0, 1);

            //Write blockId
            _fileStream.Seek(segmentByte + FileCacheProxy.SEGMENT_BLOCK_ID, SeekOrigin.Begin);
            _fileStream.Write(BitConverter.GetBytes(id), 0, 4);

            //Write startPosition
            _fileStream.Seek(segmentByte + FileCacheProxy.SEGMENT_START_POSITION, SeekOrigin.Begin);
            _fileStream.Write(BitConverter.GetBytes(startPosition), 0, 8);

            //Fill with zeros
            byte[] bytes = new byte[GetSegmentSize()];
            for (int i = 0; i < GetSegmentSize(); i++)
            {
                bytes[i] = 0;
            }

            _fileStream.Seek(segmentByte + FileCacheProxy.SEGMENT_HEADER_SIZE, SeekOrigin.Begin);
            _fileStream.Write(bytes, 0, bytes.Length);

            //Instantiate a segment
            Segment segment = new Segment();
            segment.Index = index;
            segment.Occupied = occupied;
            segment.BlockID = id;
            segment.StartPosition = startPosition;

            return segment;
        }

        public Block[] GetBlocks(IFileCacheProxy fileCacheProxy, BlockFileConverter blockFileConverter)
        {
            Block[] blocks = new Block[GetBlocksCount(fileCacheProxy, blockFileConverter)];

            int index = 0;
            foreach (var block in GetBlocksEnumerator(fileCacheProxy, blockFileConverter))
            {
                blocks[index] = block;
                index++;
            }

            return blocks;
        }

        public int GetBlocksCount(IFileCacheProxy fileCacheProxy, BlockFileConverter blockFileConverter)
        {
            List<int> blocks = new List<int>();

            foreach (var block in GetBlocksEnumerator(fileCacheProxy, blockFileConverter))
            {
                if (!blocks.Contains(block.GetId()))
                {
                    blocks.Add(block.GetId());
                }
            }

            return blocks.Count;
        }

        public IEnumerable<Block> GetBlocksEnumerator(IFileCacheProxy fileCacheProxy, BlockFileConverter blockFileConverter)
        {
            List<int> blocks = new List<int>();

            foreach (var segment in GetSegmentsEnumerator())
            {
                int blockId = segment.BlockID;
                if (!blocks.Contains(blockId))
                {
                    blocks.Add(blockId);

                    Block block = new Block(blockId, fileCacheProxy, blockFileConverter);
                    yield return block;
                }
            }
        }

        public Segment[] GetSegments()
        {
            Segment[] segments = new Segment[GetSegmentsCount()];

            int index = 0;
            foreach (var segment in GetSegmentsEnumerator())
            {
                segments[index] = segment;
                index++;
            }

            return segments;
        }

        public int GetSegmentsCount()
        {
            long fileStreamLength = _fileStream.Length;
            long segmentsBytes = fileStreamLength - FileCacheProxy.FILE_HEADER_SIZE;
            int segmentsCount = (int)(segmentsBytes / GetSegmentSizeWithHeader());

            return segmentsCount;
        }

        public IEnumerable<Segment> GetSegmentsEnumerator()
        {
            for (int i = 0; i < GetSegmentsCount(); i++)
            {
                long segmentByteStart = i * GetSegmentSizeWithHeader() + FileCacheProxy.FILE_HEADER_SIZE;
                Segment segment = new Segment();
                segment.Index = i;
                segment.Occupied = Convert.ToBoolean(ReadByte(segmentByteStart + FileCacheProxy.SEGMENT_OCCUPIED));
                segment.BlockID = Convert.ToInt32(ReadBytes(segmentByteStart + FileCacheProxy.SEGMENT_BLOCK_ID, 4));
                segment.StartPosition = Convert.ToInt64(ReadBytes(segmentByteStart + FileCacheProxy.SEGMENT_START_POSITION, 8));

                yield return segment;
            }
        }

        public byte ReadByte(long position)
        {
            position = Math.Min(position, _fileStream.Length);

            _fileStream.Seek(position, SeekOrigin.Begin);
            return (byte)_fileStream.ReadByte();
        }

        public byte[] ReadBytes(long position, long count)
        {
            position = Math.Min(position, _fileStream.Length);

            _fileStream.Seek(position, SeekOrigin.Begin);
            byte[] bytes = new byte[count];
            _fileStream.Read(bytes, 0, (int)count);

            return bytes;
        }

        public int ReadSegmentBlockID(int index)
        {
            long byteToRead = FileCacheProxy.FILE_HEADER_SIZE + index * GetSegmentSize() + FileCacheProxy.SEGMENT_BLOCK_ID;

            return Convert.ToInt32(ReadBytes(byteToRead, 4));
        }

        public bool ReadSegmentOccupied(int index)
        {
            long byteToRead = FileCacheProxy.FILE_HEADER_SIZE + index * GetSegmentSize() + FileCacheProxy.SEGMENT_BLOCK_ID;

            return Convert.ToBoolean(ReadBytes(byteToRead, 1));
        }

        public void WriteByte(long position, byte value)
        {
            position = Math.Min(position, _fileStream.Length);

            _fileStream.Seek(position, SeekOrigin.Begin);
            _fileStream.WriteByte(value);
        }

        public void WriteBytes(long position, byte[] values)
        {
            position = Math.Min(position, _fileStream.Length);

            _fileStream.Seek(position, SeekOrigin.Begin);
            _fileStream.Write(values, 0, values.Length);
        }

        public void WriteSegmentBlockID(int index, int value)
        {
            long byteToWrite = FileCacheProxy.FILE_HEADER_SIZE + index * GetSegmentSize() + FileCacheProxy.SEGMENT_BLOCK_ID;

            WriteBytes(byteToWrite, BitConverter.GetBytes(value));
        }

        public void WriteSegmentOccupied(int index, bool value)
        {
            long byteToWrite = FileCacheProxy.FILE_HEADER_SIZE + index * GetSegmentSize() + FileCacheProxy.SEGMENT_OCCUPIED;

            WriteBytes(byteToWrite, BitConverter.GetBytes(value));
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }

        public byte GetCompressionIndex()
        {
            return _compressionIndex;
        }

        public long GetSegmentSize()
        {
            return _segmentSize;
        }

        public long GetSegmentSizeWithHeader()
        {
            return _segmentSize + FileCacheProxy.SEGMENT_HEADER_SIZE;
        }
    }
}
