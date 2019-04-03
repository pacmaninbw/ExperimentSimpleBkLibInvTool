using System.Windows;
using System.ComponentModel;
using pacsw.BookInventory.Models;


namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddBookSummary.xaml
    /// </summary>
    public partial class AddBookSummary : Window
    {
        private bool _saveClicked;

        public AddBookSummary()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LoadPreviousValues);
            Closing += AddBookSummary_Closing;
            Cancelled = false;
            _saveClicked = false;
            Summary = null;
        }

        public Synopsis Summary { get; set; }

        public bool Cancelled { get; private set; }

        private void LoadPreviousValues(object sender, RoutedEventArgs e)
        {
            if (Summary == null)
            {
                Summary = new Synopsis();
            }
            else
            {
                TXTBX_Synopsis.Text = Summary.Summary;
            }
        }

        private void BTN_SaveSummary_Click(object sender, RoutedEventArgs e)
        {
            _saveClicked = true;
            Close();
        }

        private void BTN_CancelSummary_Click(object sender, RoutedEventArgs e)
        {
            Summary = null;
            Cancelled = true;
            Close();
        }

        private void TXTBX_Synopsis_LostFocus(object sender, RoutedEventArgs e)
        {
            Summary.Summary = TXTBX_Synopsis.Text;
        }

        private void AddBookSummary_Closing(object sender, CancelEventArgs e)
        {
            if (!_saveClicked)
            {
                Summary = null;
                Cancelled = true;
            }
        }
    }
}
