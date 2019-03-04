using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel
{
    public class FormatModel : DataTableItemBaseModel
    {
        public FormatModel()
            : base(((App)Application.Current).Model.FormatTable)
        {
        }

        public FormatModel(string FormatName)
            : base(((App)Application.Current).Model.FormatTable)
        {
            SetParameterValue("Name", FormatName);
        }

        public string Name
        {
            get { return GetParameterValue("Name"); }
            set { SetParameterValue("Name", value); }
        }

        public string Format
        {
            get { return Name; }
            set { Name = value; }
        }

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }
    }
}
