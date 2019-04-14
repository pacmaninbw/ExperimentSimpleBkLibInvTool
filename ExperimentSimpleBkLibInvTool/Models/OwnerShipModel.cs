using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class OwnerShipModel : DataTableItemBaseModel
    {
        public bool IsOwned
        {
            get { return GetParameterBValue("In Library"); }
            set { SetParameterValue("In Library", value); }
        }

        public bool IsWishListed
        {
            get { return GetParameterBValue("Wish Listed"); }
            set { SetParameterValue("Wish Listed", value); }
        }

        public OwnerShipModel(bool isOwned = true, bool isWishListed = false)
            : base(((App)Application.Current).Model.OwnerShip)
        {
            IsOwned = isOwned;
            IsWishListed = isWishListed;

            Modified = false;       // Initialization is not modification.
        }

        public OwnerShipModel(uint bookId, bool isOwned = true, bool isWishListed = false)
            : base(((App)Application.Current).Model.OwnerShip)
        {
            BookId = bookId;
            IsOwned = isOwned;
            IsWishListed = isWishListed;

            Modified = false;       // Initialization is not modification.
        }

        public override bool AddToDb() => ((App)Application.Current).Model.OwnerShip.AddOwnerShipData(this);
        public override bool DbUpdate() => ((App)Application.Current).Model.OwnerShip.UpdateOwnerShipData(this);

        protected override bool _dataIsValid() => true;     // No Required fields
    }
}
