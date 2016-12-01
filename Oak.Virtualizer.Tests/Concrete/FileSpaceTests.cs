using NUnit.Framework;
using Oak.Virtualizer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Tests.Concrete
{
    [TestFixture]
    public class FileSpaceTests
    {
        [Test]
        public void FileSpace_FileCreatedWithRightData()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            var fileSpace = FileSpace.Create(tempFile, 8);
            (fileSpace as IDisposable).Dispose();
            //Assert
            using (var fileStream = new FileStream(tempFile, FileMode.Open, FileAccess.Read))
            {
                Assert.AreEqual(0, fileStream.ReadByte());
                byte[] bytes = new byte[8];
                fileStream.Read(bytes, 0, 8);
                Assert.AreEqual(8, BitConverter.ToInt64(bytes, 0));
            }
        }

        [Test]
        public void FileSpace_BlockAllocatedAndAddedToList()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                var block1 = fileSpace.AllocateBlock(1);
                //Assert
                Assert.That(fileSpace.GetBlocks().Contains(block0));
                Assert.That(fileSpace.GetBlocks().Contains(block1));
            }
        }

        [Test]
        public void FileSpace_BlocksArentAllocatedTwice()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                var block1 = fileSpace.AllocateBlock(0);
                //Assert
                Assert.That(fileSpace.GetBlocks().Contains(block0));
                Assert.That(fileSpace.GetBlocks().Count() == 1);
                Assert.That(block1 == block0);
            }
        }

        [Test]
        public void FileSpace_BlocksAreDeallocated()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                var block1 = fileSpace.AllocateBlock(1);

                fileSpace.DeallocateBlock(block0);

                //Assert
                Assert.That(fileSpace.GetBlocks().Contains(block0) == false);
                Assert.That(fileSpace.GetBlocks().Contains(block1));
            }
        }

        [Test]
        public void FileSpace_BlocksAreStillDeallocatedWhenReloaded()
        {
            //Arrange
            string tempFile = Path.GetTempFileName();
            //Act
            using (var fileSpace = FileSpace.Create(tempFile, 8))
            {
                var block0 = fileSpace.AllocateBlock(0);
                var block1 = fileSpace.AllocateBlock(1);

                block0.AllocateSegment();
                block1.AllocateSegment();

                fileSpace.DeallocateBlock(block0);
            }
            using (var fileSpace = FileSpace.Load(tempFile))
            {
                var block0 = fileSpace.GetBlock(0);
                var block1 = fileSpace.GetBlock(1);

                //Assert
                Assert.That(block0 == null);
                Assert.That(block1 != null);
                Assert.That(fileSpace.GetBlocks().Count() == 1);
            }
        }
    }
}
