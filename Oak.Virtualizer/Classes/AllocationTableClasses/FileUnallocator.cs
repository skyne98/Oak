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
    public class FileUnallocator : IFileUnallocator
    {
        public void UnallocateSegment(ISegment segment, FileStream fileStream)
        {
            int segmentSize = FileStreamHelper.GetIntAtPosition(1, fileStream) + FileSpace.SEGMENT_HEADER_LENGTH;
            long position = FileSpace.FILE_HEADER_LENGTH + segmentSize * segment.GetIndex();

            FileStreamHelper.SetBoolAtPosition(position, false, fileStream);
        }
    }
}
