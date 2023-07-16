using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using whatsappStickerMaker.view.userControls;

namespace whatsappStickerMaker
{
    internal class Validation
    {
        public Validation() { }

        public bool ValidateInput(Grid infoPack) {
            return (ValidateTitle(infoPack) && ValidateAuthor(infoPack));
        }

        private bool ValidateTitle(Grid grid)
        {
            var tmp = (customInputText)grid.FindName("txtTitle");
            return (tmp.GetInputText() == "");
        }

        private bool ValidateAuthor(Grid grid)
        {
            var tmp = (customInputText)grid.FindName("txtAuthor");
            return (tmp.GetInputText() == "");

        }
    }
}
