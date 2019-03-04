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

        public ConditionsTableModel() : base("bookcondition", "getAllConditionsWithKeys", null)
        {
        }

        public string ConditionTitle(uint Key)
        {
            return KeyToName(Key);
        }

        public uint ConditionKey(string Title)
        {
            return NameToKey(Title);
        }

        protected override void InitializeSqlCommandParameters()
        {
            throw new System.NotImplementedException();
        }
    }
}
