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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExperimentSimpleBkLibInvTool.Views;

namespace ExperimentSimpleBkLibInvTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnBtn_AuthorsTable_Click(object sender, RoutedEventArgs e)
        {
            AuthorsTableView AutorsTableWin = new AuthorsTableView();
            AutorsTableWin.Show();
        }

        private void Btn_CategoriesTable_Click(object sender, RoutedEventArgs e)
        {
            CategoriesTableView categoriesTable = new CategoriesTableView();
            categoriesTable.Show();
        }

        private void Btn_BooksTable_Click(object sender, RoutedEventArgs e)
        {
            BooksTableGrid booksTableGrid = new BooksTableGrid();
            booksTableGrid.Show();
        }

        private void Btn_SeriesTable_Click(object sender, RoutedEventArgs e)
        {
            SeriesTableView seriesTableView = new SeriesTableView();
            seriesTableView.Show();
        }

        private void Btn_StatusTable_Click(object sender, RoutedEventArgs e)
        {
            StatusTableView statusTable = new StatusTableView();
            statusTable.Show();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_FormatsTable_Click(object sender, RoutedEventArgs e)
        {
            FormatTable formatTable = new FormatTable();
            formatTable.Show();
        }

        private void Btn_ConditionsTable_Click(object sender, RoutedEventArgs e)
        {
            ConditionsTableView conditionsTable = new ConditionsTableView();
            conditionsTable.Show();
        }

        private void Btn_AddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookDlg addBook = new AddBookDlg();
            addBook.Show();
        }

        private void Btn_AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
        }

        private void Btn_AddFormat_Click(object sender, RoutedEventArgs e)
        {
            AddFormat addFormat = new AddFormat();
            addFormat.Show();
        }

        private void Btn_AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            AddAuthorDlg addAuthorDlg = new AddAuthorDlg();
            addAuthorDlg.Show();
        }

        private void Btn_AddSeries_Click(object sender, RoutedEventArgs e)
        {
            AddSeriesToAuthor addSeriesToAuthor = new AddSeriesToAuthor();
            addSeriesToAuthor.Show();
        }
    }
}
