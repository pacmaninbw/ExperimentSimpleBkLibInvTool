using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using pacsw.BookInventory.Models.DataTableModel;
using pacsw.BookInventory.Models.ItemBaseModel;

namespace pacsw.BookInventory.Models.DictionaryTabelBaseModel
{
    public abstract class DictionaryTableModel : CDataTableModel
    {
        private Dictionary<uint, string> _keyToTitle;
        private Dictionary<string, uint> _titleToKey;

        public DictionaryTableModel(string TableName, string GetTableStoredProcedureName, string AddItemToTableStoredProcedureName = null) :
            base(TableName, GetTableStoredProcedureName, AddItemToTableStoredProcedureName)
        {
            _titleToKey = new Dictionary<string, uint>();
            _keyToTitle = new Dictionary<uint, string>();
            _titleToKey = DataTable.AsEnumerable().ToDictionary(row => row.Field<string>(0), row => row.Field<uint>(1));
            _keyToTitle = DataTable.AsEnumerable().ToDictionary(row => row.Field<uint>(1), row => row.Field<string>(0));
        }

        public List<string> ListBoxSelectionList()
        {
            List<string> listBoxSelectionValues = _keyToTitle.Values.ToList<string>();

            return listBoxSelectionValues;
        }

        protected string KeyToName(uint Key)
        {
            return _keyToTitle[Key];
        }

        protected uint NameToKey(string CategoryTitle)
        {
            return _titleToKey[CategoryTitle];
        }

        protected void AddItemToDictionary(DataTableItemBaseModel NewItem)
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
    }
}
