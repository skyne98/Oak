using Oak.Virtualizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes
{
    public class Segment : ISegment
    {
        long _index, _blockSpaceStartPosition;

        public Segment(long index, long blockSpaceStartPosition)
        {
            this._index = index;
            this._blockSpaceStartPosition = blockSpaceStartPosition;
        }

        public long GetBlockSpaceStartPosition()
        {
            return _blockSpaceStartPosition;
        }

        public long GetIndex()
        {
            return _index;
        }
    }
}
