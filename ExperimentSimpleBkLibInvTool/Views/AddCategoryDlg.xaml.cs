using System.Windows;

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

        private void Btn_AddCategorySave_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Model.CategoryTable.AddCategory(TxtBx_CategorTitle.Text);
            Close();
        }
    }
}
