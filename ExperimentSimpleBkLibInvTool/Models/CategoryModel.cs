using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using MySql.Data.MySqlClient;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Category
{
    public class CategoryModel : DataTableItemBaseModel
    {
        public CategoryModel()
        {
            InitParametersList();
        }

        public CategoryModel(string CategoryName)
        {
            InitParametersList();
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

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }

        private void InitParametersList()
        {
            _addSqlCommandParameter("ID", "idBookCategories", "N/A", MySqlDbType.UInt32, false, ParameterDirection.Input, true);
            _addSqlCommandParameter("Name", "CategoryName", "categoryName", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("Primary Key", "primaryKey", "primaryKey", MySqlDbType.UInt32, false, ParameterDirection.Output);
        }
    }
}
