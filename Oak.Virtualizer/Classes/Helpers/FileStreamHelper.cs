using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oak.Virtualizer.Classes.Helpers
{
    public class FileStreamHelper
    {
        public static bool GetBoolAtPosition(long position, FileStream fileStream)
        {
            fileStream.Seek(position, SeekOrigin.Begin);
            byte[] occupiedBytes = { (byte)fileStream.ReadByte() };
            return BitConverter.ToBoolean(occupiedBytes, 0);
        }
        public static void SetBoolAtPosition(long position, bool value, FileStream fileStream)
        {
            fileStream.Seek(position, SeekOrigin.Begin);

            byte byteValue = BitConverter.GetBytes(value)[0];
            fileStream.WriteByte(byteValue);
        }
        public static int GetIntAtPosition(long position, FileStream fileStream)
        {
            fileStream.Seek(1, SeekOrigin.Begin);
            byte[] segmentSizeBytes = new byte[4];
            fileStream.Read(segmentSizeBytes, 0, 4);
            return BitConverter.ToInt32(segmentSizeBytes, 0);
        }
        public static long GetLongAtPosition(long position, FileStream fileStream)
        {
            fileStream.Seek(position, SeekOrigin.Begin);
            byte[] segmentSizeBytes = new byte[8];
            fileStream.Read(segmentSizeBytes, 0, 8);
            return BitConverter.ToInt64(segmentSizeBytes, 0);
        }
        public static void SetLongAtPosition(long position, long value, FileStream fileStream)
        {
            fileStream.Seek(position, SeekOrigin.Begin);

            byte[] byteValue = BitConverter.GetBytes(value);
            fileStream.Write(byteValue, 0, 8);
        }
    }
}
