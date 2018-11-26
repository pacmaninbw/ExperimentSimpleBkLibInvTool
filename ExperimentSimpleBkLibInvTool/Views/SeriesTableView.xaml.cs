using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
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
            AddSeriesToAuthor addSeriesToAuthor = new AddSeriesToAuthor();
            addSeriesToAuthor.Show();
        }

        private void Btn_SeriesTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
