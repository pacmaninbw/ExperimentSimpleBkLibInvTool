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
        private bool _deleteBtnClicked;
        private bool _editBtnClicked;

        private const int AuthorLastNameColumnIndex = 0;
        private const int AuthorFirstNameColumnIndex = 1;
        private const int TitleColumnIndex = 2;
        private const int FormatColumnIndex = 3;
        private const int GenreColumnIndex = 4;

        public BooksTableGrid()
        {
            InitializeComponent();
            _bookTableModel = ((App)Application.Current).Model.BookTable;
            ShowOrRefreshBookGrid();

            _selectedBook = null;
            Btn_DeleteBook.IsEnabled = false;
            Btn_EditBook.IsEnabled = false;
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
            ShowOrRefreshBookGrid();
        }

        private void BooksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_deleteBtnClicked || _editBtnClicked)
            {
                return;
            }

            DataGrid dataGrid = sender as DataGrid;

            // These 4 items are enough to distinctly identify a book in the database.
            // The book model will gather all the data.
            string lastName = GetColumnContents(dataGrid, AuthorLastNameColumnIndex);
            string firstName = GetColumnContents(dataGrid, AuthorFirstNameColumnIndex);
            string format = GetColumnContents(dataGrid, FormatColumnIndex);
            string title = GetColumnContents(dataGrid, TitleColumnIndex);

            _selectedBook = new BookModel(true);
            _selectedBook.SelectBookForEditOrDelete(lastName, firstName, title, format);
            Btn_DeleteBook.IsEnabled = true;
            Btn_EditBook.IsEnabled = true;
            _deleteBtnClicked = false;
            _editBtnClicked = false;
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
            _deleteBtnClicked = true;
            BooksGrid.UnselectAll();
            ShowOrRefreshBookGrid();
            Btn_DeleteBook.IsEnabled = false;
            Btn_EditBook.IsEnabled = false;
        }

        private void ShowOrRefreshBookGrid()
        {
            _bookTable = _bookTableModel.BookTable;
            BooksGrid.DataContext = _bookTable.DefaultView;
            BooksGrid.Items.Refresh();
            _deleteBtnClicked = false;
            _editBtnClicked = false;
        }

        private void Btn_EditBook_Click(object sender, RoutedEventArgs e)
        {
            EditBookDlg bookEditor = new EditBookDlg();
            bookEditor.ThisBook = _selectedBook;
            Btn_DeleteBook.IsEnabled = false;
            Btn_EditBook.IsEnabled = false;
            _editBtnClicked = true;
            bookEditor.Closed += new EventHandler(AddBook_FormClosed);
            bookEditor.Show();
        }

        private void EditBook_FormClosed(object sender, EventArgs e)
        {
            ShowOrRefreshBookGrid();
        }
    }
}
