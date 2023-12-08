using System;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using whatsappStickerMaker.view.userControls;

namespace whatsappStickerMaker
{
    /// <summary>
    /// Window that holds the grids and manages the grid's visibility
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool gridCreate = false;

        //used to track when an image has been set
        private bool[,] imagesSet = new bool[3,10];

        public MainWindow()
        {
            InitializeComponent();
            createButton.ClickButton += CreateButtonClick;
            clearButton.ClickButton += ClearButtonClick;
            clearDataButton.ClickButton += ClearDataButtonClick;
            exportButton.ClickButton += ExportButtonClick;
        }

        /// <summary>
        /// Creates the grids required for sticker creation
        /// </summary>
        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            if (!gridCreate)
            {
                //if the visibility is set to hidden then initialize the grids
                if (!imageHolderGrid.Visibility.Equals(Visibility.Hidden)) {
                    InitImageGrid();
                    InitPackInfoGrid();
                }
                else
                {
                    //this is needed as the "clear" button just sets the grid's visibility to
                    //false
                    imageHolderGrid.Visibility = Visibility.Visible;
                    infoAboutPackGrid.Visibility = Visibility.Visible;
                }
                gridCreate = true;
            }
        }

        /// <summary>
        /// Clears the grids and make them invisible
        /// </summary>
        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            if (gridCreate)
            {
                imageHolderGrid.Visibility= Visibility.Hidden;
                infoAboutPackGrid.Visibility = Visibility.Hidden;
                ClearDataButtonClick(sender, e);
                gridCreate = false;
            }
        }

        /// <summary>
        /// Clears the text boxes and image holders
        /// </summary>
        private void ClearDataButtonClick(object sender, RoutedEventArgs e)
        {
            if (gridCreate)
            {
                foreach (imageHolder child in imageHolderGrid.Children)
                {
                    child.ClearImage();

                }
                //clear textfields
                var titleElement = (customInputText)infoAboutPackGrid.FindName("txtTitle");
                var authorElement = (customInputText)infoAboutPackGrid.FindName("txtAuthor");
                titleElement.Clear();
                authorElement.Clear();

                //clear pack icon
                var iconImageElement = (imageHolder)infoAboutPackGrid.FindName("imagePackIcon");
                iconImageElement.ClearImage();

                //clear images
                foreach (imageHolder child in imageHolderGrid.Children)
                {
                    child.ClearImage();
                }
            }
        }

        /// <summary>
        /// Exports the whatsapp sticker pack to .wastickers format
        /// </summary>
        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {
            if (gridCreate)
            {
                FileMethods fileMethods = new();

                Validation validation = new();

                bool isInvalidText = validation.ValidateTextFields(infoAboutPackGrid);

                if (isInvalidText) {
                    MessageBox.Show("Invalid text fields", "Text field error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool isValidImages = validation.ValidateImages(imagesSet);

                if (!isValidImages)
                {
                    MessageBox.Show("minimum of 3 images required","Image error", MessageBoxButton.OK, MessageBoxImage.Error);
                    // TODO error messages
                    return;
                }

                //get title
                var titleElement = (customInputText)infoAboutPackGrid.FindName("txtTitle");
                string title = titleElement.GetInputText();

                //get author
                var authorElement = (customInputText)infoAboutPackGrid.FindName("txtAuthor");
                string author = authorElement.GetInputText();

                //create dir, and remove any existing files
                fileMethods.CreateDir(title);
                fileMethods.DeleteFilesInDir(title);

                //create Text files
                string filePathTitle = string.Format("{0}/title.txt", title);
                string filePathAuthor = string.Format("{0}/author.txt", title);

                fileMethods.SaveTxtFile(filePathTitle, title);
                fileMethods.SaveTxtFile(filePathAuthor, author);

                int i = 1;
                //save images
                foreach (imageHolder child in imageHolderGrid.Children)
                {
                    if (child.imageSelected != null)
                    {

                        string filePath = string.Format("{0}/{1}.webp", title, i);
                        fileMethods.SaveImagePNG(filePath,child.imageSelected);
                        i++;
                    }
                }

                //save Pack icon
                string filePathIcon = string.Format("{0}/0.png", title);
                var iconImageElement = (imageHolder)infoAboutPackGrid.FindName("imagePackIcon");
                fileMethods.SaveImageWebp(filePathIcon, iconImageElement.imageSelected);

                fileMethods.CreateDir("output");
                fileMethods.DeleteFilesInDir("output");

                //we get the data from the zip then write to the .wasticker file format
                string zipFilePath = string.Format("output/{0}.zip", title);
                string wastickersFilePath = string.Format("output/{0}.wastickers", title);

                fileMethods.DeleteFile(zipFilePath);
                ZipFile.CreateFromDirectory(title, zipFilePath);
                byte[] data = fileMethods.StreamFile(zipFilePath);

                fileMethods.WriteBytesToFile(data, wastickersFilePath);
            }
        }

        /// <summary>
        /// initiate all the image holders.
        /// </summary>
        private void InitImageGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    String name = string.Format("image{0}", i * 10 + j);

                    imageHolder ImageHolder  = new() {
                        //range will be [0,29]
                        Name = name,
                        Row=i,
                        Col=j
                    };

                    //add event handling
                    ImageHolder.ImageChanged += HandleImageChanged;

                    Grid.SetRow(ImageHolder, i);
                    Grid.SetColumn(ImageHolder, j);
                    imageHolderGrid.Children.Add(ImageHolder);
                    imageHolderGrid.RegisterName(name, ImageHolder);
                }
            }
        }

        /// <summary>
        /// Event listener to the ImageChangedEvent event
        /// </summary>
        private void HandleImageChanged(object sender, ImageChangedEventArgs e)
        {
            //Sets the image row,col as "set"/true
            imagesSet[e.Row,e.Col] = true;
        }

        /// <summary>
        /// Creates the text fields and pack icon field
        /// </summary>
        private void InitPackInfoGrid()
        {
            const int height = 100;
            const int width = 200;

            //col 0
            customInputText customInputTextAuthor = new()
            {
                CustomLabel = "Author",
                Height = height,
                Width = width,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            Grid.SetColumn(customInputTextAuthor,0);

            //col 1

            imageHolder packIcon = new() { 
                Height= 100,
                Width = 100,
                IsPackIcon= true
            };

            Label lblPackIcon = new() { 
                Content="Pack Icon",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize=20
            };

            Grid.SetColumn(packIcon,1);
            Grid.SetColumn(lblPackIcon, 1);

            //col 2
            customInputText customInputTextTitle = new()
            {
                CustomLabel = "Title",
                Height = height,
                Width = width,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            Grid.SetColumn(customInputTextTitle, 2);
            
            //add to grid
            infoAboutPackGrid.Children.Add(customInputTextAuthor);
            infoAboutPackGrid.Children.Add(customInputTextTitle);
            infoAboutPackGrid.Children.Add(lblPackIcon);
            infoAboutPackGrid.Children.Add(packIcon);

            //register names
            infoAboutPackGrid.RegisterName("txtAuthor", customInputTextAuthor);
            infoAboutPackGrid.RegisterName("txtTitle", customInputTextTitle);
            infoAboutPackGrid.RegisterName("imagePackIcon", packIcon);
        }

    }
}
