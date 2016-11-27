using NUnit.Framework;
using Oak.Virtualizer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Tests.AllocationTable
{
    [TestFixture]
    public class AllocationTableFileAllocatorTests
    {
        [Test]
        public void FileAllocator_Block0Allocate0_SegmentIndexIs0()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_allocate_zero.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);

            //Act
            var blockSeg = fileSpace.AllocateSegment(block);

            //Assert
            Assert.AreEqual(0, blockSeg.GetIndex());
        }

        [Test]
        public void FileAllocator_Block0Allocate1_SegmentIndexIs1()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_allocate_one.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            fileSpace.AllocateSegment(block);

            //Act
            var blockSeg = fileSpace.AllocateSegment(block);

            //Assert
            Assert.AreEqual(1, blockSeg.GetIndex());
        }

        [Test]
        public void FileAllocator_Block3Allocate3_SegmentIndexIs0()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_allocate_two.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            var block1 = fileSpace.AllocateBlockSpace(1);
            var block2 = fileSpace.AllocateBlockSpace(2);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block1);

            //Act
            fileSpace.UnallocateBlockSpace(block);
            var blockSeg = fileSpace.AllocateSegment(block2);

            //Assert
            Assert.AreEqual(0, blockSeg.GetIndex());
        }

        [Test]
        public void FileAllocator_Block3Allocate3_SegmentIndexIs2()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_allocate_three.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            var block1 = fileSpace.AllocateBlockSpace(1);
            var block2 = fileSpace.AllocateBlockSpace(2);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block1);

            //Act
            fileSpace.UnallocateBlockSpace(block);
            var blockSeg = fileSpace.AllocateSegment(block1);

            //Assert
            Assert.AreEqual(2, blockSeg.GetIndex());
        }
    }
}
