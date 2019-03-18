using System;
using System.Data;
using System.Windows;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for CategoriesTableView.xaml
    /// </summary>
    public partial class CategoriesTableView : Window
    {
        private CategoryTableModel _catTableModel;
        private DataTable _CatTable;

        public CategoriesTableView()
        {
            InitializeComponent();
            _catTableModel = ((App)Application.Current).Model.CategoryTable;
            _CatTable = _catTableModel.CategoryTable;
            CategoriesDataGrid.DataContext = _CatTable.DefaultView;
        }

        private void Btn_CategoriesAddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryDlg addCategory = new AddCategoryDlg();
            addCategory.Closed += new EventHandler(CategoriesAddGenre_Close);
            addCategory.Show();
        }

        private void Btn_CategoriesTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CategoriesAddGenre_Close(object sender, EventArgs e)
        {
            _CatTable = _catTableModel.CategoryTable;
            CategoriesDataGrid.DataContext = _CatTable.DefaultView;
            CategoriesDataGrid.Items.Refresh();

            AddCategoryDlg addCategory = sender as AddCategoryDlg;
            string target = addCategory.NewGenre;
        }
    }
}
