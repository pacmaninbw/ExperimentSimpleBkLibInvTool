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
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo;

namespace ExperimentSimpleBkLibInvTool.Views
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
            addBook.Show();
        }

        private void Btn_BooksTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
