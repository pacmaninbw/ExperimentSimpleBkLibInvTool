using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned
{
    public class OwnerShipModel : DataTableItemBaseModel, IOwnerShipModel
    {
        private bool _isOwned;
        private bool _isWishListed;

        public bool IsOwned
        {
            get { return _isOwned; }
            set { _isOwned = value; }
        }

        public bool IsWishListed
        {
            get  { return _isWishListed; }
            set { _isWishListed = value; }
        }

        public OwnerShipModel(bool isOwned = true, bool isWishListed = false)
            : base(((App)Application.Current).Model.OwnerShip)
        {
            _isOwned = isOwned;
            _isWishListed = isWishListed;
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
