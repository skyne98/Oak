using NUnit.Framework;
using Oak.Virtualizer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Tests
{
    [TestFixture]
    public class FileIOTests
    {
        [Test]
        public void FileIO_Writes0_EquealToReads0()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_io_one.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            fileSpace.AllocateSegment(block);

            //Act
            block.WriteByte(0, 255);

            //Assert
            Assert.AreEqual(255, block.ReadByte(0));
        }

        [Test]
        public void FileIO_Writes1_EquealToReads1()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_io_two.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            fileSpace.AllocateSegment(block);

            //Act
            block.WriteByte(1, 44);

            //Assert
            Assert.AreEqual(44, block.ReadByte(1));
        }

        [Test]
        public void FileIO_Writes8_EquealToReads8()
        {
            //Arrange
            var fileSpace = FileSpace.Create("Tests/test_io_three.ov", 8);

            var block = fileSpace.AllocateBlockSpace(0);
            var block1 = fileSpace.AllocateBlockSpace(1);
            fileSpace.AllocateSegment(block);
            fileSpace.AllocateSegment(block1);
            fileSpace.AllocateSegment(block);

            //Act
            block.WriteByte(8, 33);

            //Assert
            Assert.AreEqual(33, block.ReadByte(8));
        }
    }
}
