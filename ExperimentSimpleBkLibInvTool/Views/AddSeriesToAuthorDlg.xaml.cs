using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddSeriesToAuthorDlg.xaml
    /// </summary>
    public partial class AddSeriesToAuthorDlg : Window
    {
        private AuthorModel _author;

        public AddSeriesToAuthorDlg(AuthorModel author)
        {
            InitializeComponent();
            _author = author;
            TxtBx_SeriesAuthorFirstName.Text = author.FirstName;
            TxtBx_SeriesAuthorMiddleName.Text = author.LastName;
        }

        private void Btn_AddSeriesSave_Click(object sender, RoutedEventArgs e)
        {
            if (_author.IsValid)
            {
                SeriesModel series = new SeriesModel(_author);
                series.Title = TxtBx_SeriesAuthorTitle.Text;
                if (series.IsValid)
                {
                    SeriesTableModel seriesTableModle = ((App)Application.Current).Model.SeriesTable;
                    if (seriesTableModle.AddSeries(series))
                    {
                        Close();
                    }
                }
            }
        }
    }
}
