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

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    /*
     * 
     * The book model is different from the other models, it is an aggregation of the other models.
     * The book model is therefore not based on the DataTableItemBaseModel. The other data models
     * are collected, any additional error checking is performed and the data for each model is
     * entered into the database.
     * 
     */

    public class BookModel : IBookModel
    {
        private readonly Model TheModel = ((App)Application.Current).Model;

        private BookInfoModel _bookInfo;
        private PuchaseInfoModel _puchaseInfo;
        private PublishInfoModel _publishInfo;
        private OwnerShipModel _owned;
        private ForSaleModel _forSale;
        private AuthorModel _authorInfo;
        private SeriesModel _seriesInfo;
        private RatingsModel _ratings;
        private ConditionsAndOtherOptionsModel _optionalItems;
        private string _condition;
        private string _status;

        public BookModel()
        {
            _bookInfo = new BookInfoModel();
            _authorInfo = null;
            _publishInfo = null;
            _puchaseInfo = null;
            _owned = null;
            _forSale = null;
            _seriesInfo = null;
        }

        public BookModel(AuthorModel authorInfo, string Title, CategoryModel category, PuchaseInfoModel puchaseInfo=null, PublishInfoModel publishInfo=null,
            OwnerShipModel owned=null, ForSaleModel forSale=null, SeriesModel seriesInfo=null, FormatModel formatm=null,  RatingsModel ratings=null, string bkstatus=null, string bkcondition=null)
        {
            Genre = category.Category;
            _bookInfo = new BookInfoModel();
            _bookInfo.Title = Title;
            _puchaseInfo = puchaseInfo;
            _publishInfo = publishInfo;
            _owned = owned;
            _forSale = forSale;
            SetAuthorValues(authorInfo);
            _seriesInfo = seriesInfo;
            Format = formatm.Format;
            Condition = bkcondition;
            Status = bkstatus;
            _ratings = ratings;
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
            bool success = false;

            return success;
        }

        public bool IsValid { get { return _dataIsValid(); } }

        protected bool _dataIsValid()
        {
#if false
            if (_defaultIsValid())
            {
                return true;
            }

            if (_authorInfo == null || !_authorInfo.IsValid)
            {
                string errorMsg = "Author first and last names are required fields.";
                MessageBox.Show(errorMsg, "Add Book Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (string.IsNullOrEmpty(_title))
            {
                string errorMsg = "The book title is a required field.";
                MessageBox.Show(errorMsg, "Add Book Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!GetParameterIsValid("Genre"))
            {
                string errorMsg = "The book category is a required field.";
                MessageBox.Show(errorMsg, "Add Book Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
#endif
            return false;
        }

        protected void SetAuthorValues(AuthorModel authorInfo)
        {
            if (!authorInfo.IsValid)
            {
                string errorMsg = "Author first and last names are required fields.";
                MessageBox.Show(errorMsg);
                return;
            }
        }
    }

}
