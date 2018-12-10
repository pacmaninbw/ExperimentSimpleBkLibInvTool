using System.Data;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AuthorsTableView.xaml
    /// </summary>
    public partial class AuthorsTableView : Window
    {
        private AuthorTableModel _authorTableModel;
        private DataTable _authorTable;

        public AuthorsTableView()
        {
            InitializeComponent();
            _authorTableModel = ((App)Application.Current).Model.AuthorTable;
            _authorTable = _authorTableModel.AuthorTable;
            AuthorsDataGrid.DataContext = _authorTable.DefaultView;
        }

        public void ForceRefreshAfterAddition()
        {
            _authorTable = _authorTableModel.AuthorTable;
            AuthorsDataGrid.DataContext = _authorTable.DefaultView;
        }

        private void Btn_AuthorsAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AddAuthorDlg AddAuthorControl = new AddAuthorDlg();
            AddAuthorControl.Show();
        }

        private void Btn_AuthorsAddSeries_Click(object sender, RoutedEventArgs e)
        {
            SelectAuthorDlg selectAuthor = new SelectAuthorDlg(_authorTableModel);
            selectAuthor.Show();
        }

        private void Btn_AuthorsAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookDlg addBookDlg = new AddBookDlg();
            addBookDlg.Show();
        }

        private void Btn_AuthorTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
