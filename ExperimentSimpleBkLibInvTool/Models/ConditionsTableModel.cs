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
            // There is no plan to implement the addition of conditions to the condition table
            // so this method is to remain unimplmented.
            throw new System.NotImplementedException();
        }
    }
}
