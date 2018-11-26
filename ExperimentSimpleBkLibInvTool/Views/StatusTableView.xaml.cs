using System.Windows;
using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkStatusTable;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for StatusTableView.xaml
    /// </summary>
    public partial class StatusTableView : Window
    {
        public StatusTableView()
        {
            InitializeComponent();
            StatusTableModel statusTableModel = ((App)Application.Current).Model.StatusTable;
            DataTable statusTable = statusTableModel.StatusTable;
            StatusGrid.DataContext = statusTable.DefaultView;
            CB_Status_Selector.DataContext = statusTable;
            CB_Status_Selector.DisplayMemberPath = "BkStatusStr";

        }

        private void Btn_StatusesClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
