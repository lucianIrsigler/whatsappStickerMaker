using System;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using whatsappStickerMaker.view.userControls;

namespace whatsappStickerMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool gridCreate = false;

        public MainWindow()
        {
            InitializeComponent();
            createButton.ClickButton += CreateButtonClick;
            clearButton.ClickButton += ClearButtonClick;
            clearDataButton.ClickButton += ClearDataButtonClick;
            exportButton.ClickButton += ExportButtonClick;
        }

        //onclick events
        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            if (!gridCreate)
            {
                if (!imageHolderGrid.Visibility.Equals(Visibility.Hidden)) {
                    InitImageGrid();
                    InitPackInfoGrid();
                }
                else
                {
                    imageHolderGrid.Visibility = Visibility.Visible;
                    infoAboutPackGrid.Visibility = Visibility.Visible;
                }
                gridCreate = true;
            }
        }


        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            if (gridCreate)
            {
                imageHolderGrid.Visibility= Visibility.Hidden;
                infoAboutPackGrid.Visibility = Visibility.Hidden;
                gridCreate = false;
            }
        }

        private void ClearDataButtonClick(object sender, RoutedEventArgs e)
        {
            if (gridCreate)
            {
                foreach (imageHolder child in imageHolderGrid.Children)
                {
                    child.ClearImage();

                }

                //todo clear data of info grid inputs
            }
        }

        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {
            if (gridCreate)
            {
                FileMethods fileMethods = new();

                /*Validation validation = new();

                if (!validation.ValidateInput(infoAboutPackGrid)) {
                    return;
                }*/

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


                string zipFilePath = string.Format("output/{0}.zip", title);
                string wastickersFilePath = string.Format("output/{0}.wastickers", title);

                fileMethods.DeleteFile(zipFilePath);

                ZipFile.CreateFromDirectory(title, zipFilePath);
                byte[] data = fileMethods.StreamFile(zipFilePath);

                fileMethods.WriteBytesToFile(data, wastickersFilePath);
            }
        }


        private void InitImageGrid()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    String name = string.Format("image{0}", i * 10 + j);

                    imageHolder imageHolder = new() {
                        //range will be [0,29]
                        Name = name
                    };
                    Grid.SetRow(imageHolder, i);
                    Grid.SetColumn(imageHolder, j);
                    imageHolderGrid.Children.Add(imageHolder);
                    imageHolderGrid.RegisterName(name, imageHolder);
                }
            }
        }

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
                isPackIcon= true
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
