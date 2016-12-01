using NUnit.Framework;
using Oak.Virtualizer.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Tests.Concrete
{
    [TestFixture]
    public class BlockTests
    {
        [Test]
        public void Block_HasRightIdWhenAllocated()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block1 = fileSpace.AllocateBlock(1);

                //Assert
                Assert.That(block1.GetId() == 1);
            }
        }

        [Test]
        public void Block_IsCachedInARigthWay()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                var block1 = fileSpace.AllocateBlock(1);
                var blockCache0 = fileSpace.GetBlock(0);
                var blockCache1 = fileSpace.GetBlock(1);

                //Assert
                Assert.That(block0 == blockCache0);
                Assert.That(block1 == blockCache1);
                Assert.That(block0 != blockCache1);
            }
        }

        [Test]
        public void Block_AllocatesTwoSegments()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                block0.AllocateSegment();
                block0.AllocateSegment();

                //Assert
                Assert.That(block0.GetSegmentsCount() == 2);
            }
        }

        [Test]
        public void Block_AllocatesTwoSegmentsAndStaysAfterReload()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                block0.AllocateSegment();
                block0.AllocateSegment();
            }
            using (var fileSpace = FileSpace.Load(tempFile))
            {
                var block0 = fileSpace.GetBlock(0);

                //Assert
                Assert.That(block0.GetSegmentsCount() == 2);
            }
        }
        [Test]
        public void Block_BlockIsAllocatedInsteadOfAnotherAndStaysAfterReload()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                block0.AllocateSegment();
                block0.AllocateSegment();
                fileSpace.DeallocateBlock(block0);
                var block1 = fileSpace.AllocateBlock(1);
                block1.AllocateSegment();
                block1.AllocateSegment();
            }

            using (var fileSpace = FileSpace.Load(tempFile))
            {
                var block0 = fileSpace.GetBlock(0);
                var block1 = fileSpace.GetBlock(1);

                //Assert
                Assert.That(block0 == null);
                Assert.That(block1 != null);
                Assert.That(block1.GetSegmentsCount() == 2);
            }
        }
        [Test]
        public void Block_WritesAndReadsValuesFromOneSegment()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                block0.AllocateSegment();
                block0.WriteByte(0, 8);
                block0.WriteByte(2, 9);

                //Assert
                Assert.AreEqual(8, block0.ReadByte(0));
                Assert.AreEqual(9, block0.ReadByte(2));
            }
        }
        [Test]
        public void Block_WritesAndReadsValuesFromTwoSegments()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                block0.AllocateSegment();
                block0.AllocateSegment();
                block0.WriteByte(0, 8);
                block0.WriteByte(8, 9);

                //Assert
                Assert.AreEqual(8, block0.ReadByte(0));
                Assert.AreEqual(9, block0.ReadByte(8));
            }
        }
        [Test]
        public void Block_CannotWriteOutOfBounds()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                try
                {
                    var block0 = fileSpace.AllocateBlock(0);
                    block0.AllocateSegment();
                    block0.WriteByte(8, 9);
                    Assert.That(false);
                }
                catch
                {
                    //Assert
                    Assert.That(true);
                }
            }
        }
        [Test]
        public void Block_CannotReadOutOfBounds()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                try
                {
                    var block0 = fileSpace.AllocateBlock(0);
                    block0.AllocateSegment();
                    block0.ReadByte(8);
                    Assert.That(false);
                }
                catch
                {
                    //Assert
                    Assert.That(true);
                }
            }
        }
        [Test]
        public void Block_TenSegmentsAllocatedWithZeros()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);

                for (int i = 0; i < 10; i++)
                {
                    block0.AllocateSegment();
                }

                //Assert
                for (int i = 0; i < 80; i++)
                {
                    Assert.AreEqual(block0.ReadByte(i), 0);
                }
            }
        }
        [Test]
        public void Block_TenSegmentsAllocatedWithZeroToEighty()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);

                for (int i = 0; i < 10; i++)
                {
                    block0.AllocateSegment();
                }

                for (int i = 0; i < 80; i++)
                {
                    block0.WriteByte(i, (byte)i);
                }

                //Assert
                for (int i = 0; i < 80; i++)
                {
                    Assert.AreEqual(block0.ReadByte(i), i);
                }
            }
        }
        [Test]
        public void Block_TenSegmentsAllocatedWithZeroToEightyAndRewritten()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);

                for (int i = 0; i < 10; i++)
                {
                    block0.AllocateSegment();
                }

                for (int i = 0; i < 80; i++)
                {
                    block0.WriteByte(i, (byte)i);
                }

                for (int i = 0; i < 10; i++)
                {
                    block0.DeallocateSegment();
                }

                for (int i = 0; i < 10; i++)
                {
                    block0.AllocateSegment();
                }

                for (int i = 80 - 1; i >= 0; i--)
                {
                    block0.WriteByte(i, (byte)i);
                }

                //Assert
                for (int i = 80 - 1; i >= 0; i--)
                {
                    Assert.AreEqual(block0.ReadByte(i), i);
                }
            }
        }
    }
}
