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
            var fileSpace = FileSpace.Create("test.ov", 65536);
            var block = fileSpace.AllocateBlockSpace(0);
            var seg0 = fileSpace.AllocateSegment(block);
            block.WriteByte(0, 1);
            block.WriteByte(1, 2);
            var seg1 = fileSpace.AllocateSegment(block);
            block.WriteByte(8, 3);
            block.WriteByte(9, 4);

            var block2 = fileSpace.AllocateBlockSpace(1);
            var seg2 = fileSpace.AllocateSegment(block2);

            fileSpace.UnallocateBlockSpace(block);

            var seg3 = fileSpace.AllocateSegment(block2);
            block2.WriteByte(8, 8);

            var timeStamp = DateTime.Now;

            block = fileSpace.AllocateBlockSpace(0);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            Console.WriteLine((DateTime.Now - timeStamp).TotalMilliseconds);

            for (int x = 0; x < 255; x++)
            {
                for (int i = 0; i < 255; i++)
                {
                    block.WriteByte(x * 255 + i, (byte)i);
                }
            }

            Console.WriteLine((DateTime.Now - timeStamp).TotalMilliseconds);
            Console.ReadLine();
        }
    }
}
