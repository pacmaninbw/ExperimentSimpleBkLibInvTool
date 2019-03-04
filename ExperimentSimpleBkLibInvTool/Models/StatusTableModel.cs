using System.Data;
using ExperimentSimpleBkLibInvTool.ModelInMVC.DictionaryTabelBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BkStatusTable
{
    public class StatusTableModel : DictionaryTableModel
    {
        public DataTable StatusTable
        {
            get { return DataTable; }
        }

        public StatusTableModel() : base("bkstatuses", "getAllStatusesWithKeys", null)
        {
        }

        public string StatusTitle(uint Key)
        {
            return KeyToName(Key);
        }

        public uint StatusKey(string Title)
        {
            return NameToKey(Title);
        }

        protected override void InitializeSqlCommandParameters()
        {
            throw new System.NotImplementedException();
        }
    }
}
