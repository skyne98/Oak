using Oak.Virtualizer.Interfaces;
using Oak.Virtualizer.Interfaces.AllocationTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes
{
    public class AllocationTable : IAllocationTable
    {
        IAllocationTablePositionConverter _allocationTablePositionConverter;
        IAllocationTableSegmentSearcher _allocationTablePositionSearcher;
        IFileAllocator _fileAllocator;
        IFileUnallocator _fileUnallocator;
        IFileIO _fileIO;

        public AllocationTable(IFileIO fileIO, IAllocationTablePositionConverter allocationTablePositionConverter, IAllocationTableSegmentSearcher allocationTablePositionSearcher, IFileAllocator fileAllocator, IFileUnallocator fileUnallocator)
        {
            this._allocationTablePositionConverter = allocationTablePositionConverter;
            this._allocationTablePositionSearcher = allocationTablePositionSearcher;
            this._fileAllocator = fileAllocator;
            this._fileUnallocator = fileUnallocator;
            this._fileIO = fileIO;
        }

        public ISegment AllocateSegment(IBlockSpace blockSpace)
        {
            return _fileAllocator.AllocateSegment(blockSpace, _fileIO.GetFileStream());
        }

        public long GetFileSpacePosition(long position, IBlockSpace blockSpace)
        {
            ISegment segment = _allocationTablePositionSearcher.Search(position, blockSpace, _fileIO.GetFileStream());
            return _allocationTablePositionConverter.ToFileSpace(position, segment, _fileIO.GetFileStream());
        }

        public void UnallocateSegment(ISegment segment)
        {
            _fileUnallocator.UnallocateSegment(segment, _fileIO.GetFileStream());
        }
    }
}
