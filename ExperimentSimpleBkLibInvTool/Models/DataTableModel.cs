using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel
{
    public class CDataTableModel
    {
        protected DataTable _dataTable;

        protected string _dbConnectionString;
        protected string _getTableStoredProcedureName;
        protected string _addItemStoredProcedureName;
        protected string _lastParameterName;
        protected bool _tableWasUpdated;
        protected uint _newKeyValue;
        protected List<string> _columns;        // provides the order of the parameters to be added to the stored procedure.

        public uint NewKeyValue { get { return _newKeyValue; } }

        protected DataTable DataTable
        {
            get {
                RefreshDataTableWhenNescessary();
                return _dataTable;
            }
        }

        public CDataTableModel()
        {
            _dataTable = null;
            _tableWasUpdated = false;
            _dbConnectionString = ConfigurationManager.ConnectionStrings["LibInvToolDBConnStr"].ConnectionString;
            _lastParameterName = null;
            _getTableStoredProcedureName = null;
            _addItemStoredProcedureName = null;
            _columns = new List<string>();
            _newKeyValue = 0;
        }

        protected void InitializeDataTable()
        {
            _dataTable = getDataTable();
            foreach (DataColumn column in _dataTable.Columns)
            {
                _columns.Add(column.ColumnName);
            }
        }

        // To maintain the best performance only refersh the table after it has been modified.
        protected void RefreshDataTableWhenNescessary()
        {
            if (_tableWasUpdated)
            {
                _dataTable = null;
                _dataTable = getDataTable();
                _tableWasUpdated = false;
            }
        }

        protected bool addItem(DataTableItemBaseModel NewDataItem)
        {
            bool canAddItemToTable = true;

            canAddItemToTable = NewDataItem.IsValid;
            if (canAddItemToTable)
            {
                canAddItemToTable = dbAddItem(NewDataItem);
            }

            if (canAddItemToTable)
            {
                _tableWasUpdated = true;
            }

            return canAddItemToTable;
        }

        protected bool _addParametersInOrder(MySqlCommand cmd, DataTableItemBaseModel NewDataItem)
        {
            bool ParameterWasAdded = true;
            foreach (string ColumnName in _columns)
            {
                if (!NewDataItem.AddParameterToCommand(cmd, ColumnName))
                {
                    return false;
                }
            }
            // For the stored procedures that return the primary key in the last parameter of the command.
            if (!string.IsNullOrEmpty(_lastParameterName))
            {
                ParameterWasAdded = NewDataItem.AddParameterToCommand(cmd, _lastParameterName);
            }

            return ParameterWasAdded;
        }

        protected virtual bool dbAddItem(DataTableItemBaseModel NewDataItem)
        {
            bool AddItemSuccess = true;

            if (string.IsNullOrEmpty(_addItemStoredProcedureName))
            {
                string errorMsg = "Programmer ERROR : _addItemStoredProcedureName is not set!";
                MessageBox.Show(errorMsg);
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
                            if (!string.IsNullOrEmpty(_lastParameterName))
                            {
                                _newKeyValue = (uint)cmd.Parameters[_lastParameterName].Value;
                            }
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

        protected DataTable getDataTable()
        {
            int ResultCount = 0;
            DataTable Dt = new DataTable();
            if(string.IsNullOrEmpty(_getTableStoredProcedureName))
            {
                string errorMsg = "Programmer ERROR : _getTableStoredProcedureName is not set!";
                MessageBox.Show(errorMsg);
                return Dt;
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
                        cmd.CommandText = _getTableStoredProcedureName;

                        MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                        ResultCount = sda.Fill(Dt);
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
    }
}
