using Oak.Virtualizer.Interfaces;
using Oak.Virtualizer.Interfaces.BlockSpaceContainer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes
{
    public class BlockSpaceContainer : IBlockSpaceContainer
    {
        IBlockSpaceAllocator _blockSpaceAllocator;
        IBlockSpaceGetter _blockSpaceGetter;
        IBlockSpaceUnallocator _blockSpaceUnallocator;
        IAllocationTable _allocationTable;
        IFileIO _fileIO;

        public BlockSpaceContainer(IBlockSpaceAllocator blockSpaceAllocator, IBlockSpaceGetter blockSpaceGetter, IBlockSpaceUnallocator blockSpaceUnallocator, IAllocationTable allocationTable, IFileIO fileIO)
        {
            this._blockSpaceAllocator = blockSpaceAllocator;
            this._blockSpaceGetter = blockSpaceGetter;
            this._blockSpaceUnallocator = blockSpaceUnallocator;
            this._allocationTable = allocationTable;
            this._fileIO = fileIO;
        }

        public IBlockSpace AllocateBlockSpace(long id)
        {
            return _blockSpaceAllocator.Allocate(id, _fileIO.GetFileStream(), _allocationTable, _fileIO);
        }

        public IBlockSpace GetBlockSpace(long id)
        {
            return _blockSpaceGetter.Get(id, _fileIO.GetFileStream());
        }

        public void UnallocateBlockSpace(IBlockSpace blockSpace)
        {
            _blockSpaceUnallocator.Unallocate(blockSpace, _fileIO.GetFileStream());
        }
    }
}
