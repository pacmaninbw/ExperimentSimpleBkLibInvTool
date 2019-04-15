using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class CategoryModel : DataTableItemBaseModel
    {
        public CategoryModel()
            : base(((App)Application.Current).Model.CategoryTable)
        {
        }

        public CategoryModel(string CategoryName)
            : base(((App)Application.Current).Model.CategoryTable)
        {
            SetParameterValue("Name", CategoryName);
            IsModified = false;       // Initialization is not modification.
        }

        public string Name
        {
            get { return GetParameterValue("Name"); }
            set { SetParameterValue("Name", value); }
        }

        public string Category
        {
            get { return Name; }
            set { Name = value; }
        }

        public uint Key
        {
            get { return GetKeyValue(); }
            set { SetKeyValue(value); }
        }

        public override bool AddToDb()
        {
            ((App)Application.Current).Model.CategoryTable.AddCategory(this);
            return true;
        }

        public override bool DbUpdate() => true;

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }
    }
}
