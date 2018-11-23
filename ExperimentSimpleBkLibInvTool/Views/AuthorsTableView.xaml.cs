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

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AuthorsTableView.xaml
    /// </summary>
    public partial class AuthorsTableView : Window
    {
        public AuthorsTableView()
        {
            InitializeComponent();
        }

        private void Btn_AuthorsAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AddAuthorDlg AddAuthorControl = new AddAuthorDlg();
            AddAuthorControl.Show();
        }

        private void Btn_AuthorsAddSeries_Click(object sender, RoutedEventArgs e)
        {
            AddSeriesToAuthor AddSeriesControl = new AddSeriesToAuthor();
            AddSeriesControl.Show();
        }

        private void Btn_AuthorsAddBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_AuthorTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
