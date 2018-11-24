﻿using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel;


namespace ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel
{
    public class FormatTableModel : DictionaryTableModel
    {
        public FormatTableModel()
        {
            _getTableStoredProcedureName = "getAllBookFormatsWithKeys";
            _addItemStoredProcedureName = "addFormat";
            _firstParameterName = "bookFormatStr";

            InitializeDictionaries();
        }

        public DataTable FormatTable
        {
            get { return DataTable; }
        }
        public string FormatTitle(uint Key)
        {
            return KeyToName(Key);
        }

        public uint FormatKey(string FormatTitle)
        {
            return NameToKey(FormatTitle);
        }

        public void AddFormat(string FormatTitle)
        {
            AddItemToDicionary(FormatTitle);
        }
    }
}