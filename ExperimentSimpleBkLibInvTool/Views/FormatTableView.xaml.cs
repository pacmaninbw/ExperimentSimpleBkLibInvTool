using System.Windows;
using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for FormatTable.xaml
    /// </summary>
    public partial class FormatTable : Window
    {
        private FormatTableModel _formatsModel;
        private DataTable _FormatsTable;

        public FormatTable()
        {
            InitializeComponent();
            _formatsModel = ((App)Application.Current).Model.FormatTable;
            _FormatsTable = _formatsModel.FormatTable;
            FormatsGrid.DataContext = _FormatsTable.DefaultView;
        }

        private void Btn_FormatsTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_FormatsAddFormat_Click(object sender, RoutedEventArgs e)
        {
            AddFormatDlg addFormat = new AddFormatDlg();
            addFormat.Show();
        }
    }
}
