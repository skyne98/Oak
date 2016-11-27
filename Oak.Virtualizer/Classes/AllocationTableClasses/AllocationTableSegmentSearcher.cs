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
    public class AllocationTableSegmentSearcher : IAllocationTableSegmentSearcher
    {
        public ISegment Search(long position, IBlockSpace blockSpace, FileStream fileStream)
        {
            long fileSpacePosition = FileSpace.FILE_HEADER_LENGTH;
            int segmentSize = FileStreamHelper.GetIntAtPosition(1, fileStream) + FileSpace.SEGMENT_HEADER_LENGTH;
            long blockSpaceBytesPassed = 0;

            while (fileSpacePosition < fileStream.Length)
            {
                bool occupied = FileStreamHelper.GetBoolAtPosition(fileSpacePosition, fileStream);

                if (occupied)
                {
                    long id = FileStreamHelper.GetLongAtPosition(fileSpacePosition + 1, fileStream);

                    if (id == blockSpace.GetID())
                    {
                        blockSpaceBytesPassed += segmentSize - FileSpace.SEGMENT_HEADER_LENGTH;

                        if (blockSpaceBytesPassed > position)
                        {
                            ISegment segment = new Segment((fileSpacePosition - FileSpace.FILE_HEADER_LENGTH) / segmentSize, blockSpaceBytesPassed - segmentSize + FileSpace.SEGMENT_HEADER_LENGTH);

                            return segment;
                        }
                    }
                }

                fileSpacePosition += segmentSize;
            }

            return null;
        }
    }
}
