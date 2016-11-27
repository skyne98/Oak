using Oak.Virtualizer.Interfaces.BlockSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Virtualizer.Interfaces;

namespace Oak.Virtualizer.Classes.BlockSpaceClasses
{
    public class BlockSpaceWriter : IBlockSpaceWriter
    {
        public void WriteByte(long position, byte value, IAllocationTable allocationTable, IFileIO fileIO, IBlockSpace blockSpace)
        {
            long fileSpacePosition = allocationTable.GetFileSpacePosition(position, blockSpace);
            fileIO.WriteByte(fileSpacePosition, value);
        }
    }
}
