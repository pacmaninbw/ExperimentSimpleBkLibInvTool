using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for SelectAuthorDlg.xaml
    /// </summary>
    public partial class SelectAuthorDlg : Window
    {
        private DataRow[] _authors;
        private AuthorTableModel _authorTable;

        public SelectAuthorDlg()
        {
            InitializeComponent();
            SelectedAuthor = null;
            _authorTable = ((App)Application.Current).Model.AuthorTable;
            _authors = _authorTable.FindAuthors("", "");    // Show all authors to beging with.
            AddRowsToListBox();
        }

        public AuthorModel SelectedAuthor { get; private set; }

        private void TB_SelectAuthorLastName_KeyUp(object sender, KeyEventArgs e)
        {
            _authors = _authorTable.FindAuthors(TB_SelectAuthorLastName.Text, TB_SelectAuthorFirstName.Text);
            AddRowsToListBox();
        }

        private void TB_SelectAuthorFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            _authors = _authorTable.FindAuthors(TB_SelectAuthorLastName.Text, TB_SelectAuthorFirstName.Text);
            AddRowsToListBox();
        }

        private void AuthorSelectorLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAuthor = _authorTable.ConvertDataRowToAuthor(_authors[AuthorSelectorLB.SelectedIndex]);
            TB_SelectAuthorFirstName.Text = SelectedAuthor.FirstName;
            TB_SelectAuthorLastName.Text = SelectedAuthor.LastName;
            TB_SelectAuthorMiddleName.Text = SelectedAuthor.MiddleName;
            TB_SelectAuthorLastName.Background = Brushes.White;
            TB_SelectAuthorFirstName.Background = Brushes.White;
            AuthorSelectorLB.Background = Brushes.White;
        }

        private void HighLightAuthorAndReportError()
        {
            MessageBox.Show("The author is required for adding a new book!");
            TB_SelectAuthorLastName.Background = Brushes.Red;
            TB_SelectAuthorFirstName.Background = Brushes.Red;
            AuthorSelectorLB.Background = Brushes.Red;
            FocusManager.SetFocusedElement(this, TB_SelectAuthorLastName);
        }

        private void AddRowsToListBox()
        {
            List<string> authorNames = _authorTable.AuthorNamesForSelector(_authors);
            if (authorNames.Count < 1)
            {
                string mbMsg = "There are no authors with that name, would you like to add an author?";
                MessageBoxResult messageBoxResult = MessageBox.Show(mbMsg, "Add Author Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AddAuthorDlg AddAuthorControl = new AddAuthorDlg();
                    AddAuthorControl.Show();
                }
            }
            else
            {
                AuthorSelectorLB.Items.Clear();
                AuthorSelectorLB.DataContext = authorNames;
                foreach (string author in authorNames)
                {
                    AuthorSelectorLB.Items.Add(author);
                }
            }
        }
    }
}
