using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace whatsappStickerMaker.view.userControls
{
    /// <summary>
    /// Interaction logic for imageHolder.xaml
    /// </summary>
    public partial class imageHolder : UserControl
    {
        public string fileName;
        public System.Windows.Controls.Image? imageSelected;
        public bool isPackIcon = false;

        public bool IsPackIcon { get; set; }

        public imageHolder()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //todo resize images properly
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.webp)|*.png;*.webp" +
                "|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();

            //file was selected
            if (result == true)
            {
                System.Windows.Controls.Image selectedImage = new System.Windows.Controls.Image();

                string selectedFilePath = openFileDialog.FileName;

                //whatsapp stickers are 512x512
                double length;

                _ = isPackIcon ? length = 96 : length = 512;

                //selectedImage.SetValue(HeightProperty, length);
                //selectedImage.SetValue(WidthProperty, length);


                Uri fileURI = new Uri(openFileDialog.FileName);
                BitmapImage imageOutput= new BitmapImage(fileURI);

                selectedImage.Source = new TransformedBitmap(
                    imageOutput, new ScaleTransform(length/imageOutput.PixelWidth,length/imageOutput.PixelHeight));
                
                //selectedImage.Source = imageOutput;
                UpdateImagePlaceholder(selectedImage, openFileDialog.SafeFileName);
            }
        }

        public void UpdateImagePlaceholder(System.Windows.Controls.Image NewImage, string FileName) {
            imageSelected = NewImage;
            imagePlaceholder.Source = imageSelected.Source;
            if (IsPackIcon){
                fileName = "icon.png";
            }
            else {
                fileName = FileName;
            }
        }
        public void ClearImage()
        {
            imagePlaceholder.Source = null;
            imageSelected = null;
            fileName = "";
        }
    }


}
