using Oak.Virtualizer.Interfaces.AllocationTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces
{
    public interface IAllocationTable
    {
        IAllocationTablePositionConverter GetPositionConverter();
        void SetPositionConverter(IAllocationTablePositionConverter allocationTablePositionConverter);
        IAllocationTableSegmentSearcher GetSegmentSearcher();
        void SetSegmentSearcher(IAllocationTableSegmentSearcher allocationTableSegmentSearcher);
        IFileAllocator GetFileAllocator();
        void SetFileAllocator(IFileAllocator fileAllocator);
        IFileUnallocator GetFileUnallocator();
        void SetFileUnallocator(IFileUnallocator fileUnallocator);
        ISegment AllocateSegment(IBlockSpace blockSpace);
        void UnallocateSegment(ISegment segment);
        long GetFileSpacePosition(long position, IBlockSpace blockSpace);
    }
}
