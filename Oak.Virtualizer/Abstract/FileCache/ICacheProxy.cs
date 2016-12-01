using Oak.Virtualizer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Abstract.FileCache
{
    internal interface ICacheProxy
    {
        //TODO: Complete docs
        /// <summary>
        /// Reads a byte from cache (if present)
        /// </summary>
        /// <param name="position">Position of byte to read (from 0 to file length in bytes)</param>
        /// <returns></returns>
        byte ReadByte(long position);
        /// <summary>
        /// Reads some bytes from cache (if present)
        /// </summary>
        /// <param name="position">Start position of a read operation (from 0 to file length in bytes)</param>
        /// <param name="count">Number of bytes to read</param>
        /// <returns></returns>
        byte[] ReadBytes(long position, long count);
        /// <summary>
        /// Writes a byte to cache (if present)
        /// </summary>
        /// <param name="position">The index of byte to write (from 0 to file length in bytes)</param>
        /// <param name="value">Value of the byte to be written</param>
        void WriteByte(long position, byte value);
        void WriteBytes(long position, byte[] values);

        void WriteSegmentOccupied(int index, bool value);
        bool ReadSegmentOccupied(int index);
        void WriteSegmentBlockID(int index, int value);
        int ReadSegmentBlockID(int index);

        Segment[] GetSegments();
        IEnumerable<Segment> GetSegmentsEnumerator();
        int GetSegmentsCount();

        Block[] GetBlocks();
        IEnumerable<Block> GetBlocksEnumerator();
        int GetBlocksCount();
        Block AllocateBlock(int id, IFileCacheProxy fileCacheProxy, BlockFileConverter blockFileConverter);
        void DeallocateBlock(int id);

        Segment AllocateAndAppendSegment(bool occupied, int id);
        Segment AllocateAndAppendSegment(bool occupied, int id, long startPosition);

        void Flush();
    }
}
