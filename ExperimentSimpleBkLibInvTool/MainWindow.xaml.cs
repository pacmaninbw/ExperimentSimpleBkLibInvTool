using System.Windows;
using ExperimentSimpleBkLibInvTool.Views;
using ExperimentSimpleBkLibInvTool.ModelInMVC;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;

namespace ExperimentSimpleBkLibInvTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly App TheApp = Application.Current as App;

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

        private void Btn_FormatsTable_Click(object sender, RoutedEventArgs e)
        {
            FormatTable formatTable = new FormatTable();
            formatTable.Show();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
