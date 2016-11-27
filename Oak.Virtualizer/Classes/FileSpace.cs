using Oak.Virtualizer.Classes.FileSpaceClasses;
using Oak.Virtualizer.Interfaces;
using Oak.Virtualizer.Interfaces.FileSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes
{
    public class FileSpace : IFileSpace
    {
        public static int FILE_HEADER_LENGTH = 5;
        public static int SEGMENT_HEADER_LENGTH = 9;

        IFileIO _fileIO;
        IAllocationTable _allocationTable;
        IBlockSpaceContainer _blockSpaceContainer;

        public FileSpace(IFileIO fileIO, IAllocationTable allocationTable, IBlockSpaceContainer blockSpaceContainer)
        {
            this._fileIO = fileIO;
            this._allocationTable = allocationTable;
            this._blockSpaceContainer = blockSpaceContainer;
        }

        public static FileSpace Create(string filePath, int segmentSize)
        {
            IFileCreator fileCreator = new FileCreator();

            fileCreator.Create(filePath, segmentSize);
            return Load(filePath);
        }
        public static FileSpace Load(string filePath)
        {
            IFileLoader fileCreator = new FileLoader();

            return (FileSpace)fileCreator.Load(filePath);
        }

        public void WriteByte(long position, byte value)
        {
            _fileIO.WriteByte(position, value);
        }

        public byte ReadByte(long position)
        {
            return _fileIO.ReadByte(position);
        }

        public ISegment AllocateSegment(IBlockSpace blockSpace)
        {
            return _allocationTable.AllocateSegment(blockSpace);
        }

        public void UnallocateSegment(ISegment segment)
        {
            _allocationTable.UnallocateSegment(segment);
        }

        public IBlockSpace GetBlockSpace(long id)
        {
            return _blockSpaceContainer.GetBlockSpace(id);
        }

        public IBlockSpace AllocateBlockSpace(long id)
        {
            return _blockSpaceContainer.AllocateBlockSpace(id);
        }

        public void UnallocateBlockSpace(IBlockSpace blockSpace)
        {
            _blockSpaceContainer.UnallocateBlockSpace(blockSpace);
        }

        #region Abstract Getters/Setters
        public IFileIO GetFileIO()
        {
            return _fileIO;
        }

        public void SetFileIO(IFileIO fileIO)
        {
            _fileIO = fileIO;
        }

        public IAllocationTable GetAllocationTable()
        {
            return _allocationTable;
        }

        public void SetAllocationTable(IAllocationTable allocationTable)
        {
            _allocationTable = allocationTable;
        }

        public IBlockSpaceContainer GetBlockSpaceContainer()
        {
            return _blockSpaceContainer;
        }

        public void SetBlockSpaceContainer(IBlockSpaceContainer blockSpaceContainer)
        {
            _blockSpaceContainer = blockSpaceContainer;
        }
        #endregion
    }
}
