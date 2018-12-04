using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel
{
    public class DictionaryTableModel : CDataTableModel
    {
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
    }
}
