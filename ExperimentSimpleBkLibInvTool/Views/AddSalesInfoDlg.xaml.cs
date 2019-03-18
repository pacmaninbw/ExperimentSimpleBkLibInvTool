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
using pacsw.BookInventory.Models.BookInfo.ForSale;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddSalesInfoDlg.xaml
    /// </summary>
    public partial class AddSalesInfoDlg : Window
    {
        public ForSaleModel SalesInfo { get; private set; }

        public AddSalesInfoDlg()
        {
            InitializeComponent();
            SalesInfo = new ForSaleModel();
        }


        private void ChkBx_IsForSale_Click(object sender, RoutedEventArgs e)
        {
            SalesInfo.IsForSale = ChkBx_IsForSale.IsChecked.Value;
        }

        private void TB_AskingPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            SalesInfo.AskingPrice = TB_AskingPrice.Text;
        }

        private void TB_EstimatedValue_LostFocus(object sender, RoutedEventArgs e)
        {
            SalesInfo.EstimatedValue = TB_EstimatedValue.Text;
        }

        private void BTN_CancelSalesInfoDlg_Click(object sender, RoutedEventArgs e)
        {
            SalesInfo = null;
            Close();
        }

        private void BTN_SaveSalesInfo_Click(object sender, RoutedEventArgs e)
        {
            if (SalesInfo.IsValid)
            {
                Close();
            }
        }
    }
}
