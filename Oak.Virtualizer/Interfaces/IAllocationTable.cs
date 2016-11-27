using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces
{
    public interface IAllocationTable
    {
        ISegment AllocateSegment(IBlockSpace blockSpace);
        void UnallocateSegment(ISegment segment);
        long GetFileSpacePosition(long position, IBlockSpace blockSpace);
    }
}
