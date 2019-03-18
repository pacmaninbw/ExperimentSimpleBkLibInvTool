using System.Windows;
using pacsw.BookInventory.Models.FormatsTableModel;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddFormatDlg.xaml
    /// </summary>
    public partial class AddFormatDlg : Window
    {
        public AddFormatDlg()
        {
            InitializeComponent();
        }

        public string NewFormat { get { return TxtBx_FormatName.Text; } }

        private void Btn_AddFormatSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBx_FormatName.Text))
            {
                MessageBox.Show("Please enter a Format name before clicking the Save button.");
            }
            else
            {
                FormatModel format = new FormatModel(TxtBx_FormatName.Text);
                format.AddToDb();
                Close();
            }
        }
    }
}
