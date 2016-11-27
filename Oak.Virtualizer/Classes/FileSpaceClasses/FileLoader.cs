using Oak.Virtualizer.Interfaces.FileSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Virtualizer.Interfaces;
using Oak.Virtualizer.Interfaces.FileIO;
using Oak.Virtualizer.Classes.FileIOClasses;
using Oak.Virtualizer.Interfaces.AllocationTable;
using Oak.Virtualizer.Classes.AllocationTableClasses;
using Oak.Virtualizer.Classes.BlockSpaceContainerClasses;
using Oak.Virtualizer.Interfaces.BlockSpaceContainer;

namespace Oak.Virtualizer.Classes.FileSpaceClasses
{
    public class FileLoader : IFileLoader
    {
        public IFileSpace Load(string filePath)
        {
            IFileReader fileReader = new FileReader();
            IFileWriter fileWriter = new FileWriter();
            IFileIO fileIO = new FileIO(filePath, fileReader, fileWriter);

            IAllocationTablePositionConverter allocationTablePositionConverter = new AllocationTablePositionConverter();
            IAllocationTableSegmentSearcher allocationTablePositionSearcher = new AllocationTableSegmentSearcher();
            IFileAllocator fileAllocator = new FileAllocator();
            IFileUnallocator fileUnallocator = new FileUnallocator();
            IAllocationTable allocationTable = new AllocationTable(fileIO, allocationTablePositionConverter, allocationTablePositionSearcher, fileAllocator, fileUnallocator);

            IBlockSpaceAllocator blockSpaceAllocator = new BlockSpaceAllocator();
            IBlockSpaceGetter blockSpaceGetter = new BlockSpaceGetter();
            IBlockSpaceUnallocator blockSpaceUnallocator = new BlockSpaceUnallocator();
            IBlockSpaceContainer blockSpaceContainer = new BlockSpaceContainer(blockSpaceAllocator, blockSpaceGetter, blockSpaceUnallocator, allocationTable, fileIO);

            return new FileSpace(fileIO, allocationTable, blockSpaceContainer);
        }
    }
}
