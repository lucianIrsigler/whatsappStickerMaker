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
    /// Interaction logic for customButton.xaml
    /// </summary>
    public partial class customButton : UserControl
    {

        public event RoutedEventHandler ClickButton;

        public customButton()
        {
            InitializeComponent();
        }

        public string CustomText
        {
            get { return (string)button.Content; }
            set { button.Content= value;}
        }

        public int CustomFontSize
        {
            get { return (int)lblButton.FontSize; }
            set { lblButton.FontSize= value;}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ClickButton != null)
            {
                ClickButton(this, e);
            }
        }
    }
}
