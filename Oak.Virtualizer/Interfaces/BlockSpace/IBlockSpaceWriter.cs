using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.BlockSpace
{
    public interface IBlockSpaceWriter
    {
        void WriteByte(long position, byte value, IAllocationTable allocationTable, IFileIO fileIO, IBlockSpace blockSpace);
    }
}
