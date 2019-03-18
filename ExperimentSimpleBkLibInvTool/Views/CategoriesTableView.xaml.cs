using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using pacsw.BookInventory.Models.Category;

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

#if false
            // Needs Debugging row is NULL
            for (int i = 0; i < CategoriesDataGrid.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)CategoriesDataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                TextBlock cellContent = CategoriesDataGrid.Columns[0].GetCellContent(row) as TextBlock;
                if (cellContent != null && cellContent.Text.Equals(target))
                {
                    object item = CategoriesDataGrid.Items[i];
                    CategoriesDataGrid.SelectedItem = item;
                    CategoriesDataGrid.ScrollIntoView(item);
                    row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    break;
                }
            }
#endif
        }
    }
}
