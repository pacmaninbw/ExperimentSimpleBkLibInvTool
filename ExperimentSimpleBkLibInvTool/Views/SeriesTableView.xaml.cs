using System.Data;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for SeriesTableView.xaml
    /// </summary>
    public partial class SeriesTableView : Window
    {
        private SeriesTableModel _seriesTableModel;
        private DataTable _seriesTable;

        public SeriesTableView()
        {
            InitializeComponent();
            _seriesTableModel = ((App)Application.Current).Model.SeriesTable;
            _seriesTable = _seriesTableModel.Series;
            SeriesGrid.DataContext = _seriesTable.DefaultView;
        }

        private void Btn_SeriesAddSeries_Click(object sender, RoutedEventArgs e)
        {
            AuthorTableModel authorTable = ((App)Application.Current).Model.AuthorTable;
            SelectAuthorDlg selectAuthor = new SelectAuthorDlg(authorTable);
            selectAuthor.Show();
        }

        private void Btn_SeriesTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
