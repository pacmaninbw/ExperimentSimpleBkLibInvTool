using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AuthorsTableView.xaml
    /// </summary>
    public partial class AuthorsTableView : Window
    {
        private AuthorTableModel _authorTableModel;
        private DataTable _authorTable;
        private AuthorModel _selectedAuthor;

        public AuthorsTableView()
        {
            InitializeComponent();
            _selectedAuthor = null;
            _authorTableModel = ((App)Application.Current).Model.AuthorTable;
            _authorTable = _authorTableModel.AuthorTable;
            AuthorsDataGrid.DataContext = _authorTable.DefaultView;
        }

        private void Btn_AuthorsAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AddAuthorDlg AddAuthorControl = new AddAuthorDlg();
            AddAuthorControl.Closed += new EventHandler(AuthorAddAuthor_FormClosed);
            AddAuthorControl.Show();
        }

        private void AuthorAddAuthor_FormClosed(object sender, EventArgs e)
        {
            _authorTable = _authorTableModel.AuthorTable;
            AuthorsDataGrid.DataContext = _authorTable.DefaultView;
            AuthorsDataGrid.Items.Refresh();
        }

        private void Btn_AuthorsAddSeries_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAuthor != null)
            {
                AddSeriesToAuthorDlg addSeries = new AddSeriesToAuthorDlg();
                addSeries.SelectedAuthor = _selectedAuthor;
                addSeries.Show();
            }
            else
            {
                AddSeriesToAuthorDlg addSeries = new AddSeriesToAuthorDlg();
                addSeries.Show();
            }
        }

        private void Btn_AuthorsAddBook_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedAuthor != null)
            {
                AddBookDlg addBookDlg = new AddBookDlg();
                addBookDlg.SelectedAuthor = _selectedAuthor;
                addBookDlg.Show();
            }
            else
            {
                AddBookDlg addBookDlg = new AddBookDlg();
                addBookDlg.Show();
            }

        }

        private void Btn_AuthorTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AuthorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
            string LastName = ((TextBlock)RowColumn.Content).Text;
            RowColumn = dataGrid.Columns[2].GetCellContent(row).Parent as DataGridCell;
            string FirstName = ((TextBlock)RowColumn.Content).Text;
            DataRow[] _authors = _authorTableModel.FindAuthors(LastName, FirstName);
            _selectedAuthor = _authorTableModel.ConvertDataRowToAuthor(_authors[0]);
        }
    }
}
