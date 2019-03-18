using System;
using System.Data;
using System.Windows;
using pacsw.BookInventory.Models.FormatsTableModel;

namespace pacsw.BookInventory.Views
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
            addFormat.Closed += new EventHandler(FormatAddFormat_Close);
            addFormat.Show();
        }

        private void FormatAddFormat_Close(object sender, EventArgs e)
        {
            _FormatsTable = _formatsModel.FormatTable;
            FormatsGrid.DataContext = _FormatsTable.DefaultView;
            FormatsGrid.Items.Refresh();

            AddFormatDlg addFormat = sender as AddFormatDlg;
            string target = addFormat.NewFormat;
            // TODO Select the new format
        }
    }
}
