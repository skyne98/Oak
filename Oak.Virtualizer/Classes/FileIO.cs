using Oak.Virtualizer.Interfaces;
using Oak.Virtualizer.Interfaces.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes
{
    public class FileIO : IFileIO
    {
        IFileReader _fileReader;
        IFileWriter _fileWriter;

        string _filePath;
        FileStream _fileStream;

        public FileIO(string filePath, IFileReader fileReader, IFileWriter fileWriter)
        {
            this._fileReader = fileReader;
            this._fileWriter = fileWriter;

            _filePath = filePath;
            _fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.ReadWrite);
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }

        public byte ReadByte(long position)
        {
            return _fileReader.ReadByte(position, _fileStream);
        }

        public void WriteByte(long position, byte value)
        {
            _fileWriter.WriteFile(position, value, _fileStream);
        }

        public FileStream GetFileStream()
        {
            return _fileStream;
        }
    }
}
