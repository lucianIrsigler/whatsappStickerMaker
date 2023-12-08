using Microsoft.Win32;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace whatsappStickerMaker.view.userControls
{

    public delegate void Notify();
    /// <summary>
    /// Interaction logic for imageHolder.xaml
    /// </summary>
    public partial class imageHolder : UserControl
    {
        public System.Windows.Controls.Image? imageSelected;
        public string fileName;
        public event EventHandler<ImageChangedEventArgs> ImageChanged;

        public bool IsPackIcon { get; set; }
        public bool ImageSet { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public imageHolder()
        {
            InitializeComponent();
            ImageSet = false;
            IsPackIcon = false;
        }

        //sends row/col data back to the main page
        protected virtual void OnImageChanged()
        {
            ImageChanged?.Invoke(this, new ImageChangedEventArgs(Row,Col));
        }

        public void TriggerEvent()
        {
            OnImageChanged();
        }

        /// <summary>
        /// Left click allows the user to select a file from their filesystem
        /// </summary>
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
                Image selectedImage = new Image();

                //whatsapp stickers are 512x512
                double length;

                _ = IsPackIcon ? length = 96 : length = 512;

                //selectedImage.SetValue(HeightProperty, length);
                //selectedImage.SetValue(WidthProperty, length);

                Uri fileURI = new Uri(openFileDialog.FileName);
                BitmapImage imageOutput = new BitmapImage(fileURI);

                selectedImage.Source = new TransformedBitmap(
                    imageOutput, new ScaleTransform(length / imageOutput.PixelWidth, length / imageOutput.PixelHeight));

                //selectedImage.Source = imageOutput;
                UpdateImagePlaceholder(selectedImage, openFileDialog.SafeFileName);

                //triggers the event for the listener to do something
                TriggerEvent();
            }
        }

        public void UpdateImagePlaceholder(Image NewImage, string FileName)
        {
            imageSelected = NewImage;
            imagePlaceholder.Source = imageSelected.Source;

            if (IsPackIcon)
            {
                //whatsapp sticker packs must have the pack icon be named "icon.png"
                fileName = "icon.png";
            }
            else
            {
                fileName = FileName;
            }

            ImageSet = true;
        }

        public void ClearImage()
        {
            imagePlaceholder.Source = null;
            imageSelected = null;
            ImageSet = false;
            fileName = "";
        }
    }


}
