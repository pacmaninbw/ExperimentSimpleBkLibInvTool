using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddFormatDlg.xaml
    /// </summary>
    public partial class AddFormatDlg : Window
    {
        public AddFormatDlg()
        {
            InitializeComponent();
        }

        private void Btn_AddFormatSave_Click(object sender, RoutedEventArgs e)
        {
            FormatTableModel formats = ((App)Application.Current).Model.FormatModel;
            formats.AddFormat(TxtBx_FormatName.Text);
            Close();
        }
    }
}
