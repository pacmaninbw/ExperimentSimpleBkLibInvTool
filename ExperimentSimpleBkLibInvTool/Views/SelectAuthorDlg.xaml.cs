using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using pacsw.BookInventory.Models.Author;

namespace pacsw.BookInventory.Views
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
            TB_SelectAuthorLastName.Text = "";
            TB_SelectAuthorFirstName.Text = "";
            Loaded += new RoutedEventHandler(PreShowSetSelectAuthorsListBox);
        }

        public AuthorModel SelectedAuthor { get; private set; }

        private void PreShowSetSelectAuthorsListBox(object sender, RoutedEventArgs e)
        {
            _authors = _authorTable.FindAuthors(TB_SelectAuthorLastName.Text, TB_SelectAuthorFirstName.Text);
            AddRowsToListBox();
        }

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
            Close();
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
