using Oak.Virtualizer.Interfaces.AllocationTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Virtualizer.Interfaces;
using System.IO;
using Oak.Virtualizer.Classes.Helpers;

namespace Oak.Virtualizer.Classes.AllocationTableClasses
{
    public class AllocationTablePositionConverter : IAllocationTablePositionConverter
    {
        public long ToFileSpace(long position, ISegment segment, FileStream fileStream)
        {
            int segmentSize = FileStreamHelper.GetIntAtPosition(1, fileStream) + FileSpace.SEGMENT_HEADER_LENGTH;

            long segmentFileSpaceStartPosition = FileSpace.FILE_HEADER_LENGTH + segmentSize * segment.GetIndex() + FileSpace.SEGMENT_HEADER_LENGTH;

            long startToPositionDifference = position - segment.GetBlockSpaceStartPosition();

            return segmentFileSpaceStartPosition + startToPositionDifference;
        }
    }
}
