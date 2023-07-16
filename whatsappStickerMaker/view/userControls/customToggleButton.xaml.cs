using System.Windows.Controls;


namespace whatsappStickerMaker.view.userControls
{
    /// <summary>
    /// Interaction logic for customToggleButton.xaml
    /// </summary>
    public partial class customToggleButton : UserControl
    {
        public customToggleButton()
        {
            InitializeComponent();
        }


        public string CustomText
        {
            get { return CustomText; }
            set { CustomText = value; }
        }
    }
}
