using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Concrete
{
    internal class Segment
    {
        int _index;
        bool _occupied;
        int _blockId;
        long _startPosition;

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        public bool Occupied
        {
            get { return _occupied; }
            set { _occupied = value; }
        }
        public int BlockID
        {
            get { return _blockId; }
            set { _blockId = value; }
        }
        public long StartPosition
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }
    }
}
