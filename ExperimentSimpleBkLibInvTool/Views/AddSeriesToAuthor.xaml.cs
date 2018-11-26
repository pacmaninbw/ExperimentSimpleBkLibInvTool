using System;
using System.Collections.Generic;
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
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddSeriesToAuthor.xaml
    /// </summary>
    public partial class AddSeriesToAuthor : Window
    {
        public AddSeriesToAuthor()
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
