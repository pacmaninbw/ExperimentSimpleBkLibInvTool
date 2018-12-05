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
        private DataRow[] _authors;
        private AuthorModel _author;

        private const int IDColumnIndex = 0;
        private const int LastNameColumnIndex = 1;
        private const int FirstNameColumnIndex = 2;
        private const int MiddleNameColumnIndex = 3;
        private const int DobColumnIndex = 4;
        private const int DodColumnIntex = 5;

        public AuthorModel SelectedAuthor { get { return _author; } }

        public SelectAuthorDlg()
        {
            InitializeComponent();
            _authorTableModel = ((App)Application.Current).Model.AuthorTable;
            _author = null;
        }

        private void AuthorSelectorLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRow Author = _authors[AuthorSelectorLB.SelectedIndex];
            _author = new AuthorModel(Author[FirstNameColumnIndex].ToString(), Author[LastNameColumnIndex].ToString(), Author[MiddleNameColumnIndex].ToString(),
                Author[DobColumnIndex].ToString(), Author[DodColumnIntex].ToString(), Convert.ToUInt32(Author[IDColumnIndex].ToString()));
            AddSeriesToAuthorDlg addSeriesToAuthor = new AddSeriesToAuthorDlg(_author);
            addSeriesToAuthor.Show();
            Close();
        }

        private void AuthorLastNameSelect_KeyUp(object sender, KeyEventArgs e)
        {
            int authorCount = 0;
            AuthorSelectorLB.Items.Clear();
            _authors = _authorTableModel.FindAuthors(AuthorLastNameSelect.Text, AuthorFirststNameSelect.Text);
            foreach (DataRow author in _authors)
            {
                string authorFirstMiddleLast = author[LastNameColumnIndex].ToString() + ", " + author[FirstNameColumnIndex].ToString() + " " + author[MiddleNameColumnIndex].ToString();
                AuthorSelectorLB.Items.Add(authorFirstMiddleLast);
                authorCount++;
            }
            if (authorCount < 1)
            {
                string mbMsg = "There are no authors with that name, would you like to add an author?";
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(mbMsg, "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    AddAuthorDlg AddAuthorControl = new AddAuthorDlg();
                    AddAuthorControl.Show();
                }
            }
        }
    }
}
