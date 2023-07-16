using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace whatsappStickerMaker.view.userControls
{
    /// <summary>
    /// Interaction logic for customInputText.xaml
    /// </summary>
    public partial class customInputText : UserControl
    {
        public customInputText()
        {
            InitializeComponent();
        }

        private string _CustomLabel;

        public string CustomLabel
        {
            get { return _CustomLabel; }
            set { 
                _CustomLabel = value;
                lblInput.Content= value;
            }
        }

        public string GetInputText()
        {
            return txtInput.Text;
        }

    }
}
