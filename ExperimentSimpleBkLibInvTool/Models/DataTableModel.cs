using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

/*
 * 
 * This file provides the database interface layer. All data retrieval and inserts
 * are performed in this file. Information about each table is stored in the 
 * super classes that inherit from this class, but the data structures are located
 * in this base class.
 * 
 */
namespace pacsw.BookInventory.Models
{
    public abstract class CDataTableModel : ObservableModelObject
    {
        protected string _dbConnectionString;
        protected string _getTableStoredProcedureName;
        protected string _addItemStoredProcedureName;
        protected string _tableName;
        protected uint _newKeyValue;
        protected MySqlParameterCollection _addItemStoredProcedureParameters;
        protected List<DbColumnParameterData> _parameterProperties;
        protected Dictionary<string, int> ParametersIndexedByPublicName;
        protected Dictionary<string, int> ParametersIndexedByDatabaseTableName;
        protected Dictionary<string, int> ParametersIndexedByParameterName;
        private List<SqlCmdParameter> _sqlCmdParameters;

        public int AddCount { get; private set; }
        public uint NewKeyValue { get { return _newKeyValue; } }

        public MySqlParameterCollection AddItemParameters { get { return _addItemStoredProcedureParameters; } }

        public List<DbColumnParameterData> ColumnParameterData { get; private set; }

        protected DataTable DataTable { get { return getDataTable(); } }

        // The impementation of this function requires detailed knowlege of the columns in the table
        // and the parameters of the stored procedure.
        protected abstract void InitializeSqlCommandParameters();

        public DbColumnParameterData GetDBColumnData(string columnName)
        {
            return ColumnParameterData.Find(x => x.ColumnName == columnName);
        }

        public List<SqlCmdParameter> SQLCommandParameters { get { return _sqlCmdParameters; } }

        public Dictionary<string, int> PublicNameParameterIndex { get { return ParametersIndexedByPublicName; } }

        public Dictionary<string, int> ParametersIndexByDbColumnName { get { return ParametersIndexedByDatabaseTableName; } }

        public Dictionary<string, int> ParametersIndexByStoredProcedureName { get { return ParametersIndexedByParameterName; } }

        protected CDataTableModel(string TableName, string GetTableStoredProcedureName, string AddItemToTableStoredProcedureName=null)
        {
            _newKeyValue = 0;
            _tableName = TableName;
            _getTableStoredProcedureName = GetTableStoredProcedureName;
            _addItemStoredProcedureName = AddItemToTableStoredProcedureName;
            _dbConnectionString = ConfigurationManager.ConnectionStrings["LibInvToolDBConnStr"].ConnectionString;
            _sqlCmdParameters = new List<SqlCmdParameter>();
            ParametersIndexedByPublicName = new Dictionary<string, int>();
            ParametersIndexedByDatabaseTableName = new Dictionary<string, int>();
            ParametersIndexedByParameterName = new Dictionary<string, int>();

            // Not all datatable classes can add items, 2 examples are the status table and the condition table.
            if (!string.IsNullOrEmpty(AddItemToTableStoredProcedureName))
            {
                GetParametersNamesFromAddCommand();
                ColumnParameterData = GetColumnParameterProperties();
                InitializeSqlCommandParameters();
                ValidateParameterCount();
            }

            AddCount = 0;
        }

        protected bool addItem(DataTableItemBaseModel NewDataItem)
        {
            bool canAddItemToTable = true;

            canAddItemToTable = NewDataItem.IsValid;
            if (canAddItemToTable)
            {
                canAddItemToTable = dbAddItem(NewDataItem);
            }

            return canAddItemToTable;
        }

        protected bool _addParametersInOrder(MySqlCommand cmd, DataTableItemBaseModel NewDataItem)
        {
            foreach (MySqlParameter parameter in _addItemStoredProcedureParameters)
            {
                if (!NewDataItem.AddParameterToCommand(cmd, parameter.ParameterName))
                {
                    return false;
                }
            }

            return true;
        }

        protected void _addSqlCommandParameter(string PublicName, DbColumnParameterData ColumnData, MySqlParameter parameter)
        {
            bool isRequired = false;
            string DBColumnName = (ColumnData != null) ? ColumnData.ColumnName : "primaryKey";

            if (!ParameterIsValid(PublicName, DBColumnName, parameter.ParameterName))
            {
                return;
            }

            if (ColumnData == null || ColumnData.IsNullable)
            {
                isRequired = false;
            }
            else
            {
                isRequired = true;
            }

            SqlCmdParameter NewParameter = new SqlCmdParameter(PublicName, DBColumnName, parameter.ParameterName, parameter.MySqlDbType, isRequired, parameter.Direction);
            ParametersIndexedByPublicName.Add(PublicName, _sqlCmdParameters.Count);
            ParametersIndexedByDatabaseTableName.Add(DBColumnName, _sqlCmdParameters.Count);
            ParametersIndexedByParameterName.Add(parameter.ParameterName, _sqlCmdParameters.Count);
            _sqlCmdParameters.Add(NewParameter);
        }

        private bool dbAddItem(DataTableItemBaseModel NewDataItem)
        {
            bool AddItemSuccess = true;

            AddCount++;
            if (AddCount > 1)
            {
                MessageBox.Show("AddCount > 1");
                return true;
            }

            if (ReportProgrammerError(_addItemStoredProcedureName, "_addItemStoredProcedureName is not set!"))
            {
                return false;
            }

            using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = _addItemStoredProcedureName;
                        if (_addParametersInOrder(cmd, NewDataItem))
                        {
                            cmd.ExecuteNonQuery();
                            // Some of the stored procedures return the new key in the last parameter
                            // in those cases get the returned key so that the new row can be accessed.
                            int paramtercount = cmd.Parameters.Count - 1;   // indexing starts at 0 ends at count - 1
                            if (cmd.Parameters[paramtercount].Direction != ParameterDirection.Input)
                            {
                               uint.TryParse(cmd.Parameters[paramtercount].Value.ToString(), out _newKeyValue);
                            }
                            OnPropertyChanged();
                        }
                        else
                        {
                            AddItemSuccess = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                    AddItemSuccess = false;
                }
            }
            return AddItemSuccess;
        }

        private DataTable getDataTable()
        {
            int ResultCount = 0;
            DataTable Dt = new DataTable();
            if (!ReportProgrammerError(_getTableStoredProcedureName, "_getTableStoredProcedureName is not set!"))
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = _getTableStoredProcedureName;

                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            ResultCount = sda.Fill(Dt);
                            OnPropertyChanged();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                }
            }

            return Dt;
        }

        private void GetParametersNamesFromAddCommand()
        {
            if (!string.IsNullOrEmpty(_addItemStoredProcedureName))
            {
                // Neither the status table or the condition table have stored procedures to
                // add data to the tables, these tables are included in add book.
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = _addItemStoredProcedureName;
                            cmd.Connection = conn;

                            MySqlCommandBuilder.DeriveParameters(cmd);
                            _addItemStoredProcedureParameters = cmd.Parameters;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Table: " + _tableName + " Stored Procedure: " + _addItemStoredProcedureName + "\nDatabase Error Initializing Command Parameter Properties: ";
                    errorMsg += ex.Message;
                    MessageBox.Show(errorMsg, "Database Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Due to bugs/unimplemented features in MySQL MySqlCommandBuilder.DeriveParameters(Command)
        // such as IsNullable will always be false this provides a workaround for getting additional
        // information about each parameter
        private List<DbColumnParameterData> GetColumnParameterProperties()
        {
            List<DbColumnParameterData> columnSchemaDetails = new List<DbColumnParameterData>();
            DataTable Dt = new DataTable();
            int ResultCount = 0;

            if (!ReportProgrammerError(_tableName, "_tableName is not set!"))
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(_dbConnectionString))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "getTableColumnData";
                            cmd.Parameters.AddWithValue("tableName", _tableName);

                            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                            ResultCount = sda.Fill(Dt);
                        }
                    }

                    foreach (DataRow dataRow in Dt.Rows)
                    {
                        columnSchemaDetails.Add(new DbColumnParameterData(dataRow));
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error Initializing Parameter Properties: " + ex.Message;
                    MessageBox.Show(errorMsg, "Database Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return columnSchemaDetails;
        }

        private bool ReportProgrammerError(string nameToCheck, string errorMessage)
        {
            if (string.IsNullOrEmpty(nameToCheck))
            {
#if DEBUG
                string errorMsg = "Programmer ERROR : " + errorMessage;
                MessageBox.Show(errorMsg, "Programmer ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
#endif
                return true;
            }
            return false;
        }

        private bool ParameterIsValid(string PublicName, string DataBaseColumnName, string StoredProcedureParamName)
        {
            bool isValid = true;

            if (ReportProgrammerError(PublicName, "PublicName is null or empty in _addSqlCommandParameter"))
            {
                isValid = false;
            }

            if (ReportProgrammerError(DataBaseColumnName, "DataBaseColumnName is null or empty in _addSqlCommandParameter"))
            {
                isValid = false;
            }

            if (ReportProgrammerError(StoredProcedureParamName, "SBParamName is null or empty in _addSqlCommandParameter"))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateParameterCount()
        {
            bool validCount = _sqlCmdParameters.Count == _addItemStoredProcedureParameters.Count;

#if DEBUG
            if (!validCount)
            {
                string eMsg = "Stored Procedure: " + _addItemStoredProcedureName + " Expected parameter count is " + _addItemStoredProcedureParameters.Count.ToString() +
                    " Actual parameter count is " + _sqlCmdParameters.Count.ToString();
                MessageBox.Show(eMsg, "Invalid Parameter Count", MessageBoxButton.OK, MessageBoxImage.Error);
            }
#endif

            return (validCount);
        }
    }
}
