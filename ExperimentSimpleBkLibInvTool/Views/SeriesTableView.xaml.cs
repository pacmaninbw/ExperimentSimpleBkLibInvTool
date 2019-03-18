using System;
using System.Data;
using System.Windows;
using pacsw.BookInventory.Models.Series;
using pacsw.BookInventory.Models.Author;

namespace pacsw.BookInventory.Views
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
            AddSeriesToAuthorDlg addSeriesDlg = new AddSeriesToAuthorDlg();
            addSeriesDlg.Closed += new EventHandler(AddSeriesClosed);
            addSeriesDlg.Show();
        }

        private void AddSeriesClosed(object sender, EventArgs e)
        {
            _seriesTable = _seriesTableModel.Series;
            SeriesGrid.DataContext = _seriesTable.DefaultView;
        }

        private void Btn_SeriesTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
