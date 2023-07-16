using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace whatsappStickerMaker
{
    internal class FileMethods
    {

        public FileMethods() { }


        public void CreateDir(string path) {
            if (!Directory.Exists(path)){
                Directory.CreateDirectory(path); 
            }
        }  

        public void DeleteFile(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public void DeleteFilesInDir(string path)
        {
            string[] filePaths = Directory.GetFiles(path);

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }


        public void SaveImageWebp(string path, System.Windows.Controls.Image image)
        {
            BitmapSource bitmapSource = (BitmapSource)image.Source;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                using (Image imageOutput = Image.Load(memoryStream))
                {
                    imageOutput.Save(path, new WebpEncoder());
                }

            }
        }

        public void SaveImagePNG(string path, System.Windows.Controls.Image image)
        {
            TransformedBitmap bitmap= (TransformedBitmap)image.Source;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder(); 
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(fileStream);
            }
        }

        public void SaveTxtFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public byte[] StreamFile(string filename)
        {
            return File.ReadAllBytes(filename);
        }

        public void WriteBytesToFile(byte[] data, string filePath)
        {
            File.WriteAllBytes(filePath, data);
        }
    }
}
