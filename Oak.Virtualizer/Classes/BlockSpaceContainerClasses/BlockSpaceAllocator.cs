using Oak.Virtualizer.Interfaces.BlockSpaceContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Virtualizer.Interfaces;
using System.IO;
using Oak.Virtualizer.Interfaces.BlockSpace;
using Oak.Virtualizer.Classes.BlockSpaceClasses;

namespace Oak.Virtualizer.Classes.BlockSpaceContainerClasses
{
    public class BlockSpaceAllocator : IBlockSpaceAllocator
    {
        public IBlockSpace Allocate(long id, FileStream fileStream, IAllocationTable allocationTable, IFileIO fileIO)
        {
            IBlockSpaceReader blockSpaceReader = new BlockSpaceReader();
            IBlockSpaceWriter blockSpaceWriter = new BlockSpaceWriter();

            return new BlockSpace(id, blockSpaceReader, blockSpaceWriter, allocationTable, fileIO);
        }
    }
}
