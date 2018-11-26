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
        protected string _lastParameterName;

        protected DataTable DataTable
        {
            get {
                RefreshDataTable();
                return _dataTable;
            }
        }

        public CDataTableModel()
        {
            _dataTable = null;
            _dbConnectionString = ConfigurationManager.ConnectionStrings["LibInvToolDBConnStr"].ConnectionString;
        }

        protected void InitializeDataTable()
        {
            _dataTable = getDataTable();
        }

        protected void RefreshDataTable()
        {
            _dataTable = null;
            _dataTable = getDataTable();
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
