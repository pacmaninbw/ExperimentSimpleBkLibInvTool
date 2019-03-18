using System.Windows;
using pacsw.BookInventory.Models.DataTableModel;
using pacsw.BookInventory.Models.ItemBaseModel;

namespace pacsw.BookInventory.Models.SynopsisNs
{
    public class Synopsis : DataTableItemBaseModel
    {
        public Synopsis()
            : base(((App)Application.Current).Model.SynopsisTable)
        {
        }

        public string Summary
        {
            get { return GetParameterValue("Synopsis"); }
            set { SetParameterValue("Synopsis", value); }
        }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.SynopsisTable.AddSynopsis(this);
        }

        protected override bool _dataIsValid()
        {
            return _defaultIsValid();
        }
    }
}
