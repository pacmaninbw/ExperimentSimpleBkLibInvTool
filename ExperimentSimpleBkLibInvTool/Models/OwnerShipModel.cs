using System.Windows;

namespace pacsw.BookInventory.Models
{
    public class OwnerShipModel : DataTableItemBaseModel, IOwnerShipModel
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
        }

        protected override bool _dataIsValid()
        {
            return true;    // No required fields
        }

        public override bool AddToDb()
        {
            return ((App)Application.Current).Model.OwnerShip.AddOwnerShipData(this);
        }
    }
}
