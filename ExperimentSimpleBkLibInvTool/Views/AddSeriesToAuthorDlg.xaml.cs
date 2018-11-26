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
        public AddSeriesToAuthorDlg()
        {
            InitializeComponent();
        }

        private void Btn_AddSeriesSave_Click(object sender, RoutedEventArgs e)
        {
            AuthorModel author = new AuthorModel();
            author.SetFirstName(TxtBx_SeriesAuthorFirstName.Text);
            author.SetLastName(TxtBx_SeriesAuthorMiddleName.Text);
            author.SetMiddleName(TxtBx_SeriesAuthorLastName.Text);

            if (author.IsValid)
            {
                SeriesModel series = new SeriesModel(author);
                series.SetTitle(TxtBx_SeriesAuthorTitle.Text);
                if (series.IsValid)
                {
                    SeriesTableModel seriesTableModle = ((App)Application.Current).Model.SeriesModel;
                    if (seriesTableModle.AddSeries(series))
                    {
                        Close();
                    }
                }
            }
        }
    }
}
