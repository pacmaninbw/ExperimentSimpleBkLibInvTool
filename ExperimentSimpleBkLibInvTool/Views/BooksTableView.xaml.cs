using System;
using System.Collections.Generic;
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
using System.Data;
using pacsw.BookInventory.Models.BookInfo;

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
