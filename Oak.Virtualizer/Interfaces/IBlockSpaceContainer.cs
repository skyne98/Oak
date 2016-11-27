using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces
{
    public interface IBlockSpaceContainer
    {
        IBlockSpace GetBlockSpace(long id);
        IBlockSpace AllocateBlockSpace(long id);
        void UnallocateBlockSpace(IBlockSpace blockSpace);
    }
}
