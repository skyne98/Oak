using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.BlockSpaceContainer
{
    public interface IBlockSpaceAllocator
    {
        IBlockSpace Allocate(long id, FileStream fileStream, IAllocationTable allocationTable, IFileIO fileIO);
    }
}
