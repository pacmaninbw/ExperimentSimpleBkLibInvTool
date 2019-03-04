using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddCategoryDlg.xaml
    /// </summary>
    public partial class AddCategoryDlg : Window
    {
        public AddCategoryDlg()
        {
            InitializeComponent();
        }

        public string NewGenre { get { return TxtBx_CategorTitle.Text; } }

        private void Btn_AddCategorySave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBx_CategorTitle.Text))
            {
                MessageBox.Show("Please enter a category name before clicking the Save button.");
            }
            else
            {
                CategoryModel category = new CategoryModel(TxtBx_CategorTitle.Text);
                ((App)Application.Current).Model.CategoryTable.AddCategory(category);
                Close();
            }
        }
    }
}
