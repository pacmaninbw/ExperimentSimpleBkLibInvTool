using System.Windows;
using pacsw.BookInventory.Models;

namespace pacsw.BookInventory.Views
{
    /// <summary>
    /// Interaction logic for AddPublishingInformation.xaml
    /// </summary>
    public partial class AddPublishingInformation : Window
    {
        public AddPublishingInformation()
        {
            InitializeComponent();
            PublishInfo = null;
            Cancelled = false;
            Loaded += new RoutedEventHandler(LoadPreviousValues);
        }

        public PublishInfoModel PublishInfo { get; set; }

        public bool Cancelled { get; private set; }

        private void LoadPreviousValues(object sender, RoutedEventArgs e)
        {
            if (PublishInfo == null)
            {
                PublishInfo = new PublishInfoModel();
            }
            else
            {
                TB_Copyright.Text = PublishInfo.CopyRight;
                TB_ISBNumber.Text = PublishInfo.ISBNumber;
                TB_Publisher.Text = PublishInfo.Publisher;
                TB_Edition.Text = PublishInfo.Edition;
                TB_Printing.Text = PublishInfo.Printing;
                CB_OutofPrint.IsChecked = PublishInfo.OutOfPrint;
            }
        }

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
            Cancelled = true;
            PublishInfo = null;
            Close();
        }
    }
}
