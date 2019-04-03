using System.Windows;
using System.ComponentModel;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddSalesInfoDlg.xaml
    /// </summary>
    public partial class AddSalesInfoDlg : Window
    {
        private bool _saveClicked;

        public ForSaleModel SalesInfo { get; set; }

        public AddSalesInfoDlg()
        {
            InitializeComponent();
            Cancelled = false;
            _saveClicked = false;
            Loaded += new RoutedEventHandler(LoadPreviousValues);
            Closing += AddSalesInfoDlg_Closing;
            SalesInfo = null;
        }

        public bool Cancelled { get; private set; }

        private void LoadPreviousValues(object sender, RoutedEventArgs e)
        {
            if (SalesInfo == null)
            {
                SalesInfo = new ForSaleModel();
            }
            else
            {
                ChkBx_IsForSale.IsChecked = SalesInfo.IsForSale;
                TB_AskingPrice.Text = SalesInfo.AskingPrice;
                TB_EstimatedValue.Text = SalesInfo.EstimatedValue;
            }
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
            Cancelled = true;
            Close();
        }

        private void BTN_SaveSalesInfo_Click(object sender, RoutedEventArgs e)
        {
            if (SalesInfo.IsValid)
            {
                _saveClicked = true;
                Close();
            }
        }

        private void AddSalesInfoDlg_Closing(object sender, CancelEventArgs e)
        {
            if (!_saveClicked)
            {
                SalesInfo = null;
                Cancelled = true;
            }
        }
    }
}
