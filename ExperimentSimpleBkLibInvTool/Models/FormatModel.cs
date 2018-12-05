using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using MySql.Data.MySqlClient;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel
{
    public class FormatModel : DataTableItemBaseModel
    {
        public FormatModel()
        {
            InitParametersList();
        }

        public FormatModel(string FormatName)
        {
            InitParametersList();
            SetParameterValue("Name", FormatName);
        }

        public string Format
        {
            get { return GetParameterValue("Name"); }
            set { SetParameterValue("Name", value); }
        }

        protected override bool _dataIsValid()
        {
            bool isValid = GetParameterIsValid("Name");

            return isValid;
        }

        private void InitParametersList()
        {
            _addSqlCommandParameter("ID", "idFormat", "N/A", MySqlDbType.UInt32, false, ParameterDirection.Input, true);
            _addSqlCommandParameter("Name", "FormatName", "bookFormatStr", MySqlDbType.String, true, ParameterDirection.Input);
            _addSqlCommandParameter("Primary Key", "primaryKey", "primaryKey", MySqlDbType.UInt32, false, ParameterDirection.Output);
        }
    }
}
