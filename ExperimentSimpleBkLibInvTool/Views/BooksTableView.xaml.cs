using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for BooksTableGrid.xaml
    /// </summary>
    public partial class BooksTableGrid : Window
    {
        private BookTableModel _bookTableModel;
        private DataTable _bookTable;
        private BookModel _selectedBook;

        private const int AuthorLastNameColumnIndex = 0;
        private const int AuthorFirstNameColumnIndex = 1;
        private const int TitleColumnIndex = 2;
        private const int FormatColumnIndex = 3;
        private const int GenreColumnIndex = 4;

        public BooksTableGrid()
        {
            InitializeComponent();
            _bookTableModel = ((App)Application.Current).Model.BookTable;
            _bookTable = _bookTableModel.BookTable;
            BooksGrid.DataContext = _bookTable.DefaultView;
            _selectedBook = null;
            Btn_DeleteBook.IsEnabled = false;
        }

        private void Btn_BooksAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookDlg addBook = new AddBookDlg();
            addBook.Closed += new EventHandler(AddBook_FormClosed);
            addBook.Show();
        }

        private void Btn_BooksTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddBook_FormClosed(object sender, EventArgs e)
        {
            _bookTable = _bookTableModel.BookTable;
            BooksGrid.DataContext = _bookTable.DefaultView;
            BooksGrid.Items.Refresh();
        }

        private void BooksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;

            // These 4 items are enough to distinctly identify a book in the database.
            // The book model will gather all the data.
            string lastName = GetColumnContents(dataGrid, AuthorLastNameColumnIndex);
            string firstName = GetColumnContents(dataGrid, AuthorFirstNameColumnIndex);
            string format = GetColumnContents(dataGrid, FormatColumnIndex);
            string title = GetColumnContents(dataGrid, TitleColumnIndex);

            _selectedBook = new BookModel();
            _selectedBook.SelectBookForEditOrDelete(lastName, firstName, title, format);
            Btn_DeleteBook.IsEnabled = true;
        }

        private string GetColumnContents(DataGrid dataGrid, int columnIndex)
        {
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[columnIndex].GetCellContent(row).Parent as DataGridCell;
            return ((TextBlock)RowColumn.Content).Text;
        }

        private void Btn_DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBook != null)
            {
                _selectedBook.DeleteBook();
            }
            Btn_DeleteBook.IsEnabled = false;
        }
    }
}
