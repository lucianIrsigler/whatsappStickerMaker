using System;
using System.IO;
using System.IO.Compression;

namespace whatsappStickerMaker
{
    internal class ZipToBinConverter
    {
        ZipToBinConverter() { }

        public static void Convert(string zipFilePath, string binFilePath)
        {
            // Rename the ZIP file to have a .bin extension
            File.Move(zipFilePath, binFilePath);
        }

        private byte[] StreamFile(string filename)
        {
            return File.ReadAllBytes(filename);
        }
    }
}
