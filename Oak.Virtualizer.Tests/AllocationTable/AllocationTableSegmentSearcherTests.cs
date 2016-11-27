using System;
using Oak.Virtualizer.Classes;
using NUnit.Framework;

namespace Oak.Virtualizer.Tests.AllocationTable
{
    [TestFixture]
    public class AllocationTableSegmentSearcherTests
    {
        [Test]
        public void Search_ZeroPosition_ZeroSegment()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_zero_zero.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            var blockSeg = fileSpace.AllocateSegment(block);
            long position = 0;

            //Act
            var seg = fileSpace.GetAllocationTable().GetSegmentSearcher().Search(position, block, fileSpace.GetFileIO().GetFileStream());

            //Assert
            Assert.AreEqual(blockSeg.GetIndex(), seg.GetIndex());            
        }

        [Test]
        public void Search_8Position_1Segment()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_eight_one.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            fileSpace.AllocateSegment(block);
            var blockSeg = fileSpace.AllocateSegment(block);
            long position = 8;

            //Act
            var seg = fileSpace.GetAllocationTable().GetSegmentSearcher().Search(position, block, fileSpace.GetFileIO().GetFileStream());

            //Assert
            Assert.AreEqual(blockSeg.GetIndex(), seg.GetIndex());
        }
        
        [Test]
        public void Search_16Position_2Segment()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_sixteen_two.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block);
            var blockSeg = fileSpace.AllocateSegment(block);
            long position = 16;

            //Act
            var seg = fileSpace.GetAllocationTable().GetSegmentSearcher().Search(position, block, fileSpace.GetFileIO().GetFileStream());

            //Assert
            Assert.AreEqual(blockSeg.GetIndex(), seg.GetIndex());
        }
    }
}
