using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BkConditionTable
{
    public class ConditionsTableModel : DictionaryTableModel
    {
        public DataTable ConditionTable
        {
            get { return DataTable; }
        }

        public ConditionsTableModel()
        {
            _getTableStoredProcedureName = "getAllConditionsWithKeys";
            InitializeDictionaries();
        }

        public string ConditionTitle(uint Key)
        {
            return KeyToName(Key);
        }

        public uint ConditionKey(string Title)
        {
            return NameToKey(Title);
        }
    }
}
