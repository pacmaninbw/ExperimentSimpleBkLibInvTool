using System;
using System.Windows;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddSeriesToAuthorDlg.xaml
    /// </summary>
    public partial class AddSeriesToAuthorDlg : Window
    {
        private AuthorModel _author;

        public AddSeriesToAuthorDlg()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(bypassAuthorSelectionIfAuthorSelected);
        }

        private void bypassAuthorSelectionIfAuthorSelected(object sender, RoutedEventArgs e)
        {
            if (_author == null)
            {
                SelectAuthorDlg selectAuthor = new SelectAuthorDlg();
                selectAuthor.Closed += new EventHandler(SelectAuthorClosed);
                selectAuthor.Show();
            }
            else
            {
                TxtBx_SeriesAuthorFirstName.Text = _author.FirstName;
                TxtBx_SeriesAuthorMiddleName.Text = _author.LastName;
            }
        }

        public AuthorModel SelectedAuthor { get { return _author; } set { _author = value; } }

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

        private void SelectAuthorClosed(object sender, EventArgs e)
        {
            SelectAuthorDlg authorSelector = (SelectAuthorDlg)sender;
            _author = authorSelector.SelectedAuthor;
            if (_author != null)
            {
                TxtBx_SeriesAuthorFirstName.Text = _author.FirstName;
                TxtBx_SeriesAuthorMiddleName.Text = _author.LastName;
            }
        }
    }
}
