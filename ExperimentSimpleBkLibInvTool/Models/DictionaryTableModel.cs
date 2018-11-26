using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel
{
    public class DictionaryTableModel : CDataTableModel
    {
        protected string _firstParameterName;
 
        private Dictionary<uint, string> _keyToTitle;
        private Dictionary<string, uint> _titleToKey;
        private DataTable _dataTable;

        public DictionaryTableModel()
        {
            _titleToKey = new Dictionary<string, uint>();
            _keyToTitle = new Dictionary<uint, string>();
            _lastParameterName = "primaryKey";      // This is a default value, it can be changed in superclasses.
        }

        protected string KeyToName(uint Key)
        {
            return _keyToTitle[Key];
        }

        protected uint NameToKey(string CategoryTitle)
        {
            return _titleToKey[CategoryTitle];
        }

        protected void AddItemToDicionary(string ItemTitleValue)
        {
            uint NewKey = AddItemToDataTable(ItemTitleValue);

            if (NewKey > 0)
            {
                _keyToTitle.Add(NewKey, ItemTitleValue);
                _titleToKey.Add(ItemTitleValue, NewKey);
            }
        }

        protected void InitializeDictionaries()
        {
            _dataTable = getDataTable();
            _titleToKey = _dataTable.AsEnumerable().ToDictionary(row => row.Field<string>(0), row => row.Field<uint>(1));
            _keyToTitle = _dataTable.AsEnumerable().ToDictionary(row => row.Field<uint>(1), row => row.Field<string>(0));
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
                        cmd.Parameters.Add(new MySqlParameter(_lastParameterName, MySqlDbType.UInt32));
                        cmd.Parameters[_lastParameterName].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        NewKey = (uint)cmd.Parameters[_lastParameterName].Value;
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

    }
}
