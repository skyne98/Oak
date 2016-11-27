using Oak.Virtualizer.Classes;
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
            var block = fileSpace.AllocateBlockSpace(0);
            var seg0 = fileSpace.AllocateSegment(block);
            block.WriteByte(0, 1);
            block.WriteByte(1, 2);
        }
    }
}
