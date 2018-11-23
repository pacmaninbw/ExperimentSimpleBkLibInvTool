using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned
{
    class OwnerShipModel : IOwnerShipModel
    {
        private int _bookId;
        private bool _isOwned;
        private bool _isWishListed;

        public bool IsOwned
        {
            get
            {
                return _isOwned;
            }
            set
            {
                _isOwned = value;
            }
        }

        public bool IsWishListed
        {
            get
            {
                return _isWishListed;
            }
            set
            {
                _isWishListed = value;
            }
        }

        public OwnerShipModel(bool isOwned=true, bool isWishListed=false)
        {
            _bookId = 0;
            _isOwned = isOwned;
            _isWishListed = isWishListed;
        }

        public void setBookId(int BookId)
        {
            _bookId = BookId;
        }

        public int getBookId()
        {
            return _bookId;
        }
    }
}
