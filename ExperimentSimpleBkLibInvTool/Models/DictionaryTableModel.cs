using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel
{
    public class DictionaryTableModel : CDataTableModel
    {
        private Dictionary<uint, string> _keyToTitle;
        private Dictionary<string, uint> _titleToKey;
        private DataTable _dataTable;

        public DictionaryTableModel()
        {
            _secondParameterName = "primaryKey";
            _titleToKey = new Dictionary<string, uint>();
            _keyToTitle = new Dictionary<uint, string>();
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
    }
}
