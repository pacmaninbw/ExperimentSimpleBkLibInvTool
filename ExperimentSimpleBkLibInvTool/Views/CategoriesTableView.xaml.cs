using System.Windows;
using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;

namespace ExperimentSimpleBkLibInvTool.Views
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
            addCategory.Show();
        }

        private void Btn_CategoriesTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
