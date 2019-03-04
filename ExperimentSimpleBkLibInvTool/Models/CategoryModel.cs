using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Category
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

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }
    }
}
