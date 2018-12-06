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
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for SelectAuthorDlg.xaml
    /// </summary>
    public partial class SelectAuthorDlg : Window
    {
        private AuthorTableModel _authorTableModel;
        DataRow[] _authors;

        public AuthorModel SelectedAuthor { get; private set; }

        public SelectAuthorDlg()
        {
            InitializeComponent();
            _authorTableModel = ((App)Application.Current).Model.AuthorTable;
            SelectedAuthor = null;
            _authors = _authorTableModel.FindAuthors("");
            AddRowsToListBox();     // Start with all authors
        }

        public void ForceRefreshAfterAuthorAddition()
        {
            DataTable _authorTable = _authorTableModel.AuthorTable;
            _authors = _authorTableModel.FindAuthors(AuthorLastNameSelect.Text, AuthorFirststNameSelect.Text);
            AddRowsToListBox();
        }

        private void AuthorSelectorLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAuthor = _authorTableModel.ConvertDataRowToAuthor(_authors[AuthorSelectorLB.SelectedIndex]);
            AddSeriesToAuthorDlg addSeriesToAuthor = new AddSeriesToAuthorDlg(SelectedAuthor);
            addSeriesToAuthor.Show();
            Close();
        }

        private void AddRowsToListBox()
        {
            AuthorSelectorLB.Items.Clear();

            int authorCount = 0;
            List<string> authorNames = _authorTableModel.AuthorNamesForSelector(_authors);
            foreach (string author in authorNames)
            {
                AuthorSelectorLB.Items.Add(author);
                authorCount++;
            }

            if (authorCount < 1)
            {
                string mbMsg = "There are no authors with that name, would you like to add an author?";
                MessageBoxResult messageBoxResult = MessageBox.Show(mbMsg, "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AddAuthorDlg AddAuthorControl = new AddAuthorDlg();
                    AddAuthorControl.Show();
                }
            }
        }

        private void AuthorLastNameSelect_KeyUp(object sender, KeyEventArgs e)
        {
            _authors = _authorTableModel.FindAuthors(AuthorLastNameSelect.Text, AuthorFirststNameSelect.Text);

            AddRowsToListBox();
        }
    }
}
