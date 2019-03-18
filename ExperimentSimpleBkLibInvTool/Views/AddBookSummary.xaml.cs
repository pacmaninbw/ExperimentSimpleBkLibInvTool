using System.Windows;
using pacsw.BookInventory.Models;


namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddBookSummary.xaml
    /// </summary>
    public partial class AddBookSummary : Window
    {
        public AddBookSummary()
        {
            InitializeComponent();
            Summary = new Synopsis();
        }

        public Synopsis Summary { get; private set; }

        private void BTN_SaveSummary_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTN_CancelSummary_Click(object sender, RoutedEventArgs e)
        {
            Summary = null;
            Close();
        }

        private void TXTBX_Synopsis_LostFocus(object sender, RoutedEventArgs e)
        {
            Summary.Summary = TXTBX_Synopsis.Text;
        }
    }
}
