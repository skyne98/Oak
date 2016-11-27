using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces
{
    public interface ISegment
    {
        long GetIndex();
        long GetBlockSpaceStartPosition();
    }
}
