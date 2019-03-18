using System.Windows;
using pacsw.BookInventory.Models.ItemBaseModel;

namespace pacsw.BookInventory.Models.Category
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

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }
    }
}
