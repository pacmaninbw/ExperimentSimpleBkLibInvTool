using System.Data;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;

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
            _seriesTableModel = ((App)Application.Current).Model.SeriesModel;
            _seriesTable = _seriesTableModel.Series;
            SeriesGrid.DataContext = _seriesTable.DefaultView;
        }

        private void Btn_SeriesAddSeries_Click(object sender, RoutedEventArgs e)
        {
            AddSeriesToAuthorDlg addSeriesToAuthor = new AddSeriesToAuthorDlg();
            addSeriesToAuthor.Show();
        }

        private void Btn_SeriesTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
