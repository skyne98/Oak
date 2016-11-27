using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.BlockSpaceContainer
{
    public interface IBlockSpaceUnallocator
    {
        void Unallocate(IBlockSpace blockSpace, FileStream fileStream);
    }
}
