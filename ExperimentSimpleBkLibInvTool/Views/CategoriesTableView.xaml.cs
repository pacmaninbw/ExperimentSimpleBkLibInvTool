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
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
        }

        private void Btn_CategoriesTableClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
