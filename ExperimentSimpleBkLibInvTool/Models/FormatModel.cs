using System.Windows;

namespace pacsw.BookInventory.Models
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
            Modified = false;       // Initialization is not modification.
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

        public override bool AddToDb()
        {
            ((App)Application.Current).Model.FormatTable.AddFormat(this);
            return true;
        }

        public override bool DbUpdate()
        {
            throw new System.NotImplementedException();
        }

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }
    }
}
