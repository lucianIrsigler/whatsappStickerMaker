using System;
using System.Windows.Controls;
using whatsappStickerMaker.view.userControls;

namespace whatsappStickerMaker
{
    internal class Validation
    {
        public Validation() { }

        public bool ValidateTextFields(Grid infoPack) {
            return (TitleIsEmpty(infoPack) || AuthorIsEmpty(infoPack));
        }

        private bool TitleIsEmpty(Grid grid)
        {
            var tmp = (customInputText)grid.FindName("txtTitle");
            return (tmp.GetInputText() == "");
        }

        private bool AuthorIsEmpty(Grid grid)
        {
            var tmp = (customInputText)grid.FindName("txtAuthor");
            return (tmp.GetInputText() == "");

        }

        public bool ValidateImages(bool[,] imagesSet)
        {
            //min of 3
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (imagesSet[i, j])
                    {
                        count++;
                    }
                }
            }

            if (count < 3)
            {
                return false;
            }

            return true;
        }
    }
}
