using System.Windows;

namespace pacsw.BookInventory.Models
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
