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
        public ConditionsAndOtherOptionsModel() :
            base(((App)Application.Current).Model.ConditionsAndOptions)
        {
        }

        public string Condition { get; set; }

        public string Status { get; set; }

        public string PhysicalCondition { get; set; }

        public bool SignedByAuthor { get; set; }

        public bool Read { get; set; }

        protected override bool _dataIsValid()
        {
            throw new NotImplementedException();
        }
    }
}
