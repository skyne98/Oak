using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.BlockSpace
{
    public interface IBlockSpaceReader
    {
        byte ReadByte(long position, IAllocationTable allocationTable, IFileIO fileIO, IBlockSpace blockSpace);
    }
}
