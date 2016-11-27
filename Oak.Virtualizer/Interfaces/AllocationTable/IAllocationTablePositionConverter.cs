using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.AllocationTable
{
    public interface IAllocationTablePositionConverter
    {
        long ToFileSpace(long position, ISegment segment, FileStream fileStream);
    }
}
