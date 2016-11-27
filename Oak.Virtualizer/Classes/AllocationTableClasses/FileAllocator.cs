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
    public class FileAllocator : IFileAllocator
    {
        public ISegment AllocateSegment(IBlockSpace blockSpace, FileStream fileStream)
        {
            ISegment segment = TryFindFree(blockSpace, fileStream);

            if (segment == null)
            {
                segment = AllocateNew(blockSpace, fileStream);
            }

            return segment;
        }

        ISegment TryFindFree(IBlockSpace blockSpace, FileStream fileStream)
        {
            long position = FileSpace.FILE_HEADER_LENGTH;
            int segmentSize = FileStreamHelper.GetIntAtPosition(1, fileStream) + FileSpace.SEGMENT_HEADER_LENGTH;
            long lastFoundFreeSegmentPosition = 0;
            bool freeSegmentFound = false;

            while (position < fileStream.Length)
            {
                bool occupied = FileStreamHelper.GetBoolAtPosition(position, fileStream);
                long id = FileStreamHelper.GetLongAtPosition(position + 1, fileStream);

                if (!occupied && freeSegmentFound == false)
                {
                    freeSegmentFound = true;
                    lastFoundFreeSegmentPosition = position;
                }
                else if (occupied && id == blockSpace.GetID())
                {
                    freeSegmentFound = false;
                }

                position += segmentSize;
            }

            if (freeSegmentFound)
            {
                ISegment segment = new Segment((lastFoundFreeSegmentPosition - FileSpace.FILE_HEADER_LENGTH) / segmentSize, 0);

                FileStreamHelper.SetBoolAtPosition(lastFoundFreeSegmentPosition, true, fileStream);
                FileStreamHelper.SetLongAtPosition(lastFoundFreeSegmentPosition + 1, blockSpace.GetID(), fileStream);

                return segment;
            }

            return null;
        }
        ISegment AllocateNew(IBlockSpace blockSpace, FileStream fileStream)
        {
            int segmentSize = FileStreamHelper.GetIntAtPosition(1, fileStream) + FileSpace.SEGMENT_HEADER_LENGTH;
            long fileStreamLength = fileStream.Length;

            ISegment segment = new Segment((fileStreamLength - FileSpace.FILE_HEADER_LENGTH) / segmentSize, 0);

            FileStreamHelper.SetBoolAtPosition(fileStreamLength, true, fileStream);
            FileStreamHelper.SetLongAtPosition(fileStreamLength + 1, blockSpace.GetID(), fileStream);

            for (int i = 0; i < segmentSize - FileSpace.SEGMENT_HEADER_LENGTH; i++)
            {
                FileStreamHelper.SetBoolAtPosition(fileStreamLength + FileSpace.SEGMENT_HEADER_LENGTH + i, false, fileStream);
            }

            return segment;
        }
    }
}
