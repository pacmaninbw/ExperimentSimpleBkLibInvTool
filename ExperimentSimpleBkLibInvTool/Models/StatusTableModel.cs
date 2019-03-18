using System.Data;
using pacsw.BookInventory.Models.DictionaryTabelBaseModel;

namespace pacsw.BookInventory.Models.BkStatusTable
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
            // There is no plan to implement the addition of status to the status table
            // so this method is to remain unimplmented.
            throw new System.NotImplementedException();
        }
    }
}
