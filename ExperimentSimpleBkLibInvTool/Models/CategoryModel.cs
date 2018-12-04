using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using MySql.Data.MySqlClient;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.Category
{
    public class CategoryModel : DataTableItemBaseModel
    {
        public CategoryModel()
        {
            _addSqlCommandParameter("idBookCategories", "idBookCategories", "N/A", MySqlDbType.UInt32, false, ParameterDirection.Input, true);
            _addSqlCommandParameter("Name", "CategoryName", "categoryName", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("Primary Key", "primaryKey", "primaryKey", MySqlDbType.UInt32, false, ParameterDirection.Output);
        }

        public CategoryModel(string CategoryName)
        {
            _addSqlCommandParameter("idBookCategories", "idBookCategories", "N/A", MySqlDbType.UInt32, false, ParameterDirection.Input, true);
            _addSqlCommandParameter("Name", "CategoryName", "categoryName", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("Primary Key", "primaryKey", "primaryKey", MySqlDbType.UInt32, false, ParameterDirection.Output);
            SetParameterValue("Name", CategoryName);
        }

        public string Category
        {
            get { return GetParameterValue("Name"); }
            set { SetParameterValue("Name", value); }
        }

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }
    }
}
