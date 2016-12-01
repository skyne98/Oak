using Oak.Virtualizer.Abstract.FileCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete
{
    /// <summary>
    /// A class for accessing some of FileSpace virtual memory
    /// </summary>
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

        /// <summary>
        /// Check whether given position is in bounds of Block's virtual memory
        /// </summary>
        /// <param name="position">Position to check (in bytes)</param>
        /// <returns></returns>
        public bool InBounds(long position)
        {
            long bytesAvailable = GetSegmentsCount() * _fileCacheProxy.GetSegmentSize();

            return position >= 0 && position < bytesAvailable;
        }
        /// <summary>
        /// Read a byte from a given position in virtual memory
        /// </summary>
        /// <param name="position">Position to read from (in bytes)</param>
        /// <returns></returns>
        public byte ReadByte(long position)
        {
            long filePosition = _blockFileConverter.BlockToFile(position, this);
            return _fileCacheProxy.ReadByte(filePosition);
        }
        /// <summary>
        /// Write a byte to a given position in virtual memory
        /// </summary>
        /// <param name="position">Position to write to (in bytes)</param>
        /// <param name="value">Byte value to be written</param>
        public void WriteByte(long position, byte value)
        {
            long filePosition = _blockFileConverter.BlockToFile(position, this);
            _fileCacheProxy.WriteByte(filePosition, value);
        }
        /// <summary>
        /// Provide one segment more virtual memory to the Block
        /// </summary>
        public void AllocateSegment()
        {
            _fileCacheProxy.AllocateSegmentForBlock(this);
        }
        /// <summary>
        /// Remove one (last) segment from the Block's virtual memory
        /// </summary>
        public void DeallocateSegment()
        {
            _fileCacheProxy.DeallocateSegmentFromBlock(this);
        }
        /// <summary>
        /// Get the amount of memory segments dedicated to the Block
        /// </summary>
        /// <returns></returns>
        public int GetSegmentsCount()
        {
            return _fileCacheProxy.GetSegments().Where(a => a.BlockID == _id).Count();
        }
        /// <summary>
        /// Get the id of the block
        /// </summary>
        /// <returns></returns>
        public int GetId()
        {
            return _id;
        }
    }
}
