using Oak.Virtualizer.Interfaces.BlockSpaceContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oak.Virtualizer.Interfaces;
using System.IO;
using Oak.Virtualizer.Classes.Helpers;

namespace Oak.Virtualizer.Classes.BlockSpaceContainerClasses
{
    public class BlockSpaceUnallocator : IBlockSpaceUnallocator
    {
        public void Unallocate(IBlockSpace blockSpace, FileStream fileStream)
        {
            long position = FileSpace.FILE_HEADER_LENGTH;
            int segmentSize = FileStreamHelper.GetIntAtPosition(1, fileStream) + FileSpace.SEGMENT_HEADER_LENGTH;

            while (position < fileStream.Length)
            {
                bool occupied = FileStreamHelper.GetBoolAtPosition(position, fileStream);

                if (occupied)
                {
                    long id = FileStreamHelper.GetLongAtPosition(position + 1, fileStream);

                    if (id == blockSpace.GetID())
                    {
                        FileStreamHelper.SetBoolAtPosition(position, false, fileStream);
                    }
                }

                position += segmentSize;
            }
        }
    }
}
