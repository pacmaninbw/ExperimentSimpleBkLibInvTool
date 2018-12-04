using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel
{
    public class DictionaryTableModel : CDataTableModel
    {
        protected string _firstParameterName;
 
        private Dictionary<uint, string> _keyToTitle;
        private Dictionary<string, uint> _titleToKey;

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

        protected void AddItemToDicionary(DataTableItemBaseModel NewItem)
        {
            bool AddedSuccessfully = addItem(NewItem);

            if (AddedSuccessfully && _newKeyValue > 0)
            {
                _keyToTitle.Add(_newKeyValue, NewItem.GetParameterValue("Name"));
                _titleToKey.Add(NewItem.GetParameterValue("Name"), _newKeyValue);
            }
            else
            {
                string errorMsg = "Database Error: Failed to add item";
                MessageBox.Show(errorMsg);
            }
        }

        protected void InitializeDictionaries()
        {
            if (_dataTable == null)
            {
                InitializeDataTable();
                _dataTable = DataTable;
            }

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
            else
            {
                _tableWasUpdated = true;
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
                        _newKeyValue = (uint)cmd.Parameters[_lastParameterName].Value;
                        NewKey = _newKeyValue;
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
