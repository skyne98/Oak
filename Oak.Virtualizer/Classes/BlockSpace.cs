using Oak.Virtualizer.Interfaces;
using Oak.Virtualizer.Interfaces.BlockSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes
{
    public class BlockSpace : IBlockSpace
    {
        IBlockSpaceReader _blockSpaceReader;
        IBlockSpaceWriter _blockSpaceWriter;
        IAllocationTable _allocationTable;
        IFileIO _fileIO;

        long _id;

        public BlockSpace(long id, IBlockSpaceReader blockSpaceReader, IBlockSpaceWriter blockSpaceWriter, IAllocationTable allocationTable, IFileIO fileIO)
        {
            this._blockSpaceReader = blockSpaceReader;
            this._blockSpaceWriter = blockSpaceWriter;
            this._allocationTable = allocationTable;
            this._fileIO = fileIO;
            this._id = id;
        }

        public byte ReadByte(long position)
        {
            return _blockSpaceReader.ReadByte(position, _allocationTable, _fileIO, this);
        }

        public void WriteByte(long position, byte value)
        {
            _blockSpaceWriter.WriteByte(position, value, _allocationTable, _fileIO, this);
        }

        public long GetID()
        {
            return _id;
        }
    }
}
