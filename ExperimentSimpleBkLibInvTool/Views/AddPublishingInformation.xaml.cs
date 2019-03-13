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
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for AddPublishingInformation.xaml
    /// </summary>
    public partial class AddPublishingInformation : Window
    {
        public AddPublishingInformation()
        {
            InitializeComponent();
            PublishInfo = new PublishInfoModel();
        }

        public PublishInfoModel PublishInfo { get; private set; }

        private void TB_Copyright_LostFocus(object sender, RoutedEventArgs e)
        {
            PublishInfo.CopyRight = TB_Copyright.Text;
        }

        private void TB_ISBNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            PublishInfo.ISBNumber = TB_ISBNumber.Text;
        }

        private void TB_Publisher_LostFocus(object sender, RoutedEventArgs e)
        {
            PublishInfo.Publisher = TB_Publisher.Text;
        }

        private void TB_Edition_LostFocus(object sender, RoutedEventArgs e)
        {
            PublishInfo.Edition = TB_Edition.Text;
        }

        private void TB_Printing_LostFocus(object sender, RoutedEventArgs e)
        {
            PublishInfo.Printing = TB_Printing.Text;
        }

        private void CB_OutofPrint_Click(object sender, RoutedEventArgs e)
        {
            PublishInfo.OutOfPrint = CB_OutofPrint.IsChecked.Value;
        }

        private void BTN_SavePublishingInfo_Click(object sender, RoutedEventArgs e)
        {
            if (PublishInfo.IsValid)
            {
                Close();
            }
        }

        private void BTN_CancelPublishingInfoDlg_Click(object sender, RoutedEventArgs e)
        {
            PublishInfo = null;
            Close();
        }
    }
}
