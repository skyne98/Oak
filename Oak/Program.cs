using Oak.Virtualizer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSpace = FileSpace.Create("test.ov", 8);
            var block = fileSpace.AllocateBlock(0);
            block.AllocateSegment();
            block.WriteByte(0, 1);
            block.WriteByte(1, 2);
            var block2 = fileSpace.AllocateBlock(1);
            block2.AllocateSegment();
            block2.WriteByte(0, 2);
            block2.WriteByte(1, 3);
            block.AllocateSegment();
            block.WriteByte(8, 8);
            block.WriteByte(9, 9);
            fileSpace.Flush();
        }
    }
}
