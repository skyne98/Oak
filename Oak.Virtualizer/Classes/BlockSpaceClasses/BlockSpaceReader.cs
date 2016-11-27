using Oak.Virtualizer.Interfaces.BlockSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Virtualizer.Interfaces;

namespace Oak.Virtualizer.Classes.BlockSpaceClasses
{
    public class BlockSpaceReader : IBlockSpaceReader
    {
        public byte ReadByte(long position, IAllocationTable allocationTable, IFileIO fileIO, IBlockSpace blockSpace)
        {
            long fileSpacePosition = allocationTable.GetFileSpacePosition(position, blockSpace);
            return fileIO.ReadByte(fileSpacePosition);
        }
    }
}
