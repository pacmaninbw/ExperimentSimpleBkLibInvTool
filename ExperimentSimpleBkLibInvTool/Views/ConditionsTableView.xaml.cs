using System.Data;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BkConditionTable;

namespace ExperimentSimpleBkLibInvTool.Views
{
    /// <summary>
    /// Interaction logic for ConditionsTableView.xaml
    /// </summary>
    public partial class ConditionsTableView : Window
    {
        private ConditionsTableModel _conditionsTableModel;
        private DataTable _conditions;

        public ConditionsTableView()
        {
            InitializeComponent();
            _conditionsTableModel = ((App)Application.Current).Model.ConditionsTable;
            _conditions = _conditionsTableModel.ConditionTable;
            ConditionsDataGrid.DataContext = _conditions.DefaultView;
            LB_Conditions_Selector.DataContext = _conditions;
            LB_Conditions_Selector.DisplayMemberPath = "ConditionOfBookStr";
            CB_Conditions_Selector.DataContext = _conditions;
            CB_Conditions_Selector.DisplayMemberPath = "ConditionOfBookStr";
        }

        private void Btn_Conditions_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
