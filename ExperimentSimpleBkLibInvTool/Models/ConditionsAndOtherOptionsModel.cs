using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Options
{
    public class ConditionsAndOtherOptionsModel : DataTableItemBaseModel, IConditionsAndOtherOptionsModel
    {
        string _conditionStr;
        string _statusStr;
        private readonly Model TheModel = ((App)Application.Current).Model;


        public ConditionsAndOtherOptionsModel() :
            base(((App)Application.Current).Model.ConditionsAndOptions)
        {
            _conditionStr = string.Empty;
            _statusStr = string.Empty;
        }

        public string Condition {
            get { return _conditionStr; }
            set { SaveConditionAndConvertToKey(value); }
        }

        public uint ConditionKey { get { return GetParameterKValue("Condition Id"); } }

        public string Status {
            get { return _statusStr; }
            set { SaveStatusAndConvertToKey(value); }
        }

        public string PhysicalCondition { get; set; }

        public bool SignedByAuthor { get; set; }

        public bool Read { get; set; }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.ConditionsAndOptions.AddConditionsAndOptions(this);
        }

        protected override bool _dataIsValid()
        {
            throw new NotImplementedException();
        }

        private void SaveConditionAndConvertToKey(string condition)
        {
            _conditionStr = condition;
            SetParameterValue("Condition Id", TheModel.ConditionsTable.ConditionKey(condition));
        }

        private void SaveStatusAndConvertToKey(string status)
        {
            _statusStr = status;
            SetParameterValue("Status Id", TheModel.StatusTable.StatusKey(status));
        }
    }
}
