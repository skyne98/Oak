using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces.BlockSpaceContainer
{
    public interface IBlockSpaceGetter
    {
        IBlockSpace Get(long id, FileStream fileStream);
    }
}
