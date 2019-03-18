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
using pacsw.BookInventory.Models.SynopsisNs;


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
