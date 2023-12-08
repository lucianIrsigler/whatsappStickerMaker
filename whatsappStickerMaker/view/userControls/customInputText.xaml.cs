using System.Windows.Controls;

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

        public void Clear()
        {
            txtInput.Clear();
        }

    }
}
