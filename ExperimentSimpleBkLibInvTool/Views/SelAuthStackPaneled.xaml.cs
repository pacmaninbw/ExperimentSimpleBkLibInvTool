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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;


namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for SelAuthStackPaneled.xaml
    /// </summary>
    public partial class SelAuthStackPaneled : UserControl
    {
        private DataRow[] _authors;

        public SelAuthStackPaneled()
        {
            InitializeComponent();
            SelectedAuthor = null;
        }

        public AuthorTableModel AuthorTableModel { get; set; }

        public AuthorModel SelectedAuthor { get; private set; }

        public void InitAuthorList()
        {
            _authors = AuthorTableModel.FindAuthors("", "");
            AddRowsToListBox();
        }

        private void TB_SelectAuthorLastName_KeyUp(object sender, KeyEventArgs e)
        {
            _authors = AuthorTableModel.FindAuthors(TB_SelectAuthorLastName.Text, TB_SelectAuthorFirstName.Text);
            AddRowsToListBox();
        }

        private void TB_SelectAuthorFirstName_KeyUp(object sender, KeyEventArgs e)
        {
            _authors = AuthorTableModel.FindAuthors(TB_SelectAuthorLastName.Text, TB_SelectAuthorFirstName.Text);
            AddRowsToListBox();
        }

        private void AuthorSelectorLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAuthor = AuthorTableModel.ConvertDataRowToAuthor(_authors[AuthorSelectorLB.SelectedIndex]);
        }

        private void AddRowsToListBox()
        {
            List<string> authorNames = AuthorTableModel.AuthorNamesForSelector(_authors);
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
                AuthorSelectorLB.DataContext = authorNames;
            }
        }
    }
}
