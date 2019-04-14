using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class Synopsis : DataTableItemBaseModel
    {
        public Synopsis()
            : base(((App)Application.Current).Model.SynopsisTable)
        {
        }

        public Synopsis(uint bookId, string summary)
            : base(((App)Application.Current).Model.SynopsisTable)
        {
            BookId = bookId;
            Summary = summary;
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

        public override bool DbUpdate()
        {
            throw new System.NotImplementedException();
        }

        protected override bool _dataIsValid()
        {
            return _defaultIsValid();
        }
    }
}
