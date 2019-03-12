using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Options;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    /*
     * 
     * The book model is different from the other models, it is an aggregation of the other models.
     * The book model is therefore not based on the DataTableItemBaseModel. The other data models
     * are collected, any necessary data conversion is performed, any additional error checking
     * is performed and the data for each model is entered into the database.
     * 
     */

    public class BookModel
    {
        private readonly Model TheModel = ((App)Application.Current).Model;

        private uint _bookKey;

        private BookInfoModel _bookInfo;
        private AuthorModel _authorInfo;
        private ForSaleModel _forSale;
        private OwnerShipModel _owned;
        private ConditionsAndOtherOptionsModel _optionalItems;
        private PuchaseInfoModel _puchaseInfo;
        private PublishInfoModel _publishInfo;
        private RatingsModel _ratings;
        private SeriesModel _seriesInfo;

        public BookModel()
        {
            _bookKey = 0;
            _bookInfo = new BookInfoModel();
            _authorInfo = null;
            _forSale = null;
            _owned = null;
            _publishInfo = null;
            _puchaseInfo = null;
            _ratings = null;
            _seriesInfo = null;
        }

        public IPublishInfoModel PublishInfo
        {
            get { return _publishInfo; }
            set { _publishInfo = (PublishInfoModel)value; }
        }

        public IPuchaseInfoModel PuchaseInfo
        {
            get { return _puchaseInfo; }
            set { _puchaseInfo = (PuchaseInfoModel)value; }
        }

        public IOwnerShipModel Owned
        {
            get { return _owned; }
            set { _owned = (OwnerShipModel)value; }
        }

        public IForSaleModel ForSale
        {
            get { return (IForSaleModel) _forSale; }
            set { _forSale = (ForSaleModel) value; }
        }

        public IAuthorModel AuthorInfo
        {
            get { return _authorInfo; }
            set { _authorInfo = (AuthorModel)value; }
        }

        public ISeriesModel SeriesInfo
        {
            get { return _seriesInfo; }
            set { _seriesInfo = (SeriesModel)value; }
        }

        public IRatingsModel Ratings
        {
            get { return _ratings; }
            set { _ratings = (RatingsModel)value; }
        }

        public IConditionsAndOtherOptionsModel ConditionsAndOptions
        {
            get { return _optionalItems; }
            set { _optionalItems = (ConditionsAndOtherOptionsModel) value; }
        }

        public string Genre
        {
            get { return _bookInfo.Genre; }
            set { _bookInfo.Genre = value; }
        }

        public string Title
        {
            get { return _bookInfo.Title; }
            set { _bookInfo.Title = value; }
        }

        public string Format
        {
            get { return _bookInfo.Format; }
            set { _bookInfo.Format = value; }
        }

        public string Condition
        {
            get { return _optionalItems.Condition; }
            set { _optionalItems.Condition = value; }
        }

        public string Status
        {
            get { return _optionalItems.Status; }
            set { _optionalItems.Status = value; }
        }

        public bool AddBookToLibrary()
        {
            bool success = IsValid;

            if (success)
            {
                if (success = _bookInfo.AddToDb())
                {
                    _bookKey = _bookInfo.getBookID();
                }

                if (_bookKey > 0)
                {
                    success = AddToDb(success, _publishInfo);
                    success = AddToDb(success, _puchaseInfo);
                    success = AddToDb(success, _optionalItems);
                    success = AddToDb(success, _forSale);
                    success = AddToDb(success, _owned);
                    success = AddToDb(success, _ratings);
                }
            }

            return success;
        }

        public bool IsValid { get { return _dataIsValid(); } }

        protected bool _dataIsValid()
        {
            bool isValid = true;
            if (_authorInfo == null || !_authorInfo.IsValid)
            {
                isValid = false;
            }

            if (_bookInfo == null || !_bookInfo.IsValid)
            {
                isValid = false;
            }

            if (_forSale != null && !_forSale.IsValid)
            {
                isValid = false;
            }

            if (_optionalItems != null && !_optionalItems.IsValid)
            {
                isValid = false;
            }

            if (_owned != null && !_owned.IsValid)
            {
                isValid = false;
            }

            if (_publishInfo != null && !_publishInfo.IsValid)
            {
                isValid = false;
            }

            if (_puchaseInfo != null && !_puchaseInfo.IsValid)
            {
                isValid = false;
            }

            if (_ratings != null && !_ratings.IsValid)
            {
                isValid = false;
            }

            if (_seriesInfo != null && !_seriesInfo.IsValid)
            {
                isValid = false;
            }

            return isValid;
        }

        protected void SetAuthorValues(AuthorModel authorInfo)
        {
            if (!authorInfo.IsValid)
            {
                string errorMsg = "Author first and last names are required fields.";
                MessageBox.Show(errorMsg);
                return;
            }

            _bookInfo.Author = authorInfo;
        }

        private bool AddToDb(bool success, DataTableItemBaseModel item)
        {
            if (success && item != null)
            {
                item.setBookId(_bookKey);
                success = item.AddToDb();
            }

            return success;
        }
    }
}
