using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Interfaces
{
    public interface IBlockSpace
    {
        byte ReadByte(long position);
        void WriteByte(long position, byte value);
        long GetID();
    }
}
