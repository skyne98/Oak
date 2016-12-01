using Oak.Virtualizer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Abstract.FileCache
{
    internal interface IFileCacheProxy
    {
        //TODO: Complete docs
        /// <summary>
        /// If true, uses cache to speed up the queries to the file
        /// </summary>
        bool UseCache { get; set; }

        /// <summary>
        /// Reads a byte from a file
        /// </summary>
        /// <param name="position">Position of byte to read (from 0 to file length in bytes)</param>
        /// <returns></returns>
        byte ReadByte(long position);
        /// <summary>
        /// Reads some bytes from a file
        /// </summary>
        /// <param name="position">Start position of a read operation (from 0 to file length in bytes)</param>
        /// <param name="count">Number of bytes to read</param>
        /// <returns></returns>
        byte[] ReadBytes(long position, long count);
        /// <summary>
        /// Writes a byte to a file
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
        long GetSegmentSize();
        long GetSegmentSizeWithHeader();

        Block[] GetBlocks(BlockFileConverter blockFileConverter);
        IEnumerable<Block> GetBlocksEnumerator(BlockFileConverter blockFileConverter);
        int GetBlocksCount(BlockFileConverter blockFileConverter);
        Block AllocateBlock(int id, BlockFileConverter blockFileConverter);
        void DeallocateBlock(Block block);

        Segment AllocateSegmentForBlock(Block block);
        void DeallocateSegmentFromBlock(Block block);
        Segment AllocateAndAppendSegment(bool occupied, int id);
        Segment AllocateAndAppendSegment(bool occupied, int id, long startPosition);

        void Flush();
    }
}
