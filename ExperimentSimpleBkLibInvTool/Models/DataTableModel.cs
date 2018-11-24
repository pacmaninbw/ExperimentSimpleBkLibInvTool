using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel
{
    public class CDataTableModel
    {
        private DataTable _dataTable;

        protected string _dbConnectionString;
        protected string _getTableStoredProcedureName;
        protected string _addItemStoredProcedureName;
        protected string _firstParameterName;
        protected string _secondParameterName;

        protected DataTable DataTable
        {
            get {
                if (_dataTable == null)
                {
                    _dataTable = getDataTable();
                }
                return _dataTable;
            }
        }

        public CDataTableModel()
        {
            _secondParameterName = "primaryKey";
            _dbConnectionString = ConfigurationManager.ConnectionStrings["LibInvToolDBConnStr"].ConnectionString;
        }

        protected void InitializeDataTable()
        {
            _dataTable = getDataTable();
        }

        protected uint AddItemToDataTable(string ItemTitleValue)
        {
            uint NewKey = _dbAddItem(ItemTitleValue);

            if (NewKey < 1)
            {
                string errorMsg = "Database Error: NewKey returned value lest than 1: NewKey = " + NewKey.ToString();
                MessageBox.Show(errorMsg);
            }

            return NewKey;
        }

        // Some 
        protected virtual uint _dbAddItem(string CategoryTitle)
        {
            uint NewKey = 0;
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
                        cmd.Parameters.AddWithValue(_firstParameterName, CategoryTitle);
                        cmd.Parameters.Add(new MySqlParameter(_secondParameterName, MySqlDbType.UInt32));
                        cmd.Parameters[_secondParameterName].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        NewKey = (uint)cmd.Parameters[_secondParameterName].Value;
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = "Database Error: " + ex.Message;
                    MessageBox.Show(errorMsg);
                }
            }

            return NewKey;
        }

        protected DataTable getDataTable()
        {
            int resultCount = 0;
            DataTable Dt = new DataTable();
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
                        resultCount = sda.Fill(Dt);
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
