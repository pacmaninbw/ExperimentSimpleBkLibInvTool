using System;
using System.Windows;
using System.Data;
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

        public BooksTableGrid()
        {
            InitializeComponent();
            _bookTableModel = ((App)Application.Current).Model.BookTable;
            _bookTable = _bookTableModel.BookTable;
            BooksGrid.DataContext = _bookTable.DefaultView;
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


    }
}
