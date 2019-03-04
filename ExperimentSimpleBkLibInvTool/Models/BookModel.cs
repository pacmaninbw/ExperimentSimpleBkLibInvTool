using System.Windows;
using MySql.Data.MySqlClient;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ratings;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public class BookModel : DataTableItemBaseModel, IBookModel
    {
        private BookInfoModel _bookInfo;
        private PuchaseInfoModel _puchaseInfo;
        private PublishInfoModel _publishInfo;
        private string _title;
        private OwnerShipModel _owned;
        private ForSaleModel _forSale;
        private AuthorModel _authorInfo;
        private SeriesModel _seriesInfo;
        private RatingsModel _ratings;

        public BookModel()
            : base(((App)Application.Current).Model.BookTable)
        {
            _bookInfo = null;
            _authorInfo = null;
            _publishInfo = null;
            _puchaseInfo = null;
            _owned = null;
            _forSale = null;
            _seriesInfo = null;
        }

        public BookModel(AuthorModel authorInfo, string Title, CategoryModel category, PuchaseInfoModel puchaseInfo=null, PublishInfoModel publishInfo=null,
            OwnerShipModel owned=null, ForSaleModel forSale=null, SeriesModel seriesInfo=null, FormatModel formatm=null,  RatingsModel ratings=null, string bkstatus=null, string bkcondition=null)
            : base(((App)Application.Current).Model.BookTable)
        {
            Category = category.Category;
            _title = Title;
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

        public IBookInfoModel Book
        {
            get { return _bookInfo; }
            set { _bookInfo = (BookInfoModel)value; }
        }

        public string Category
        {
            get { return GetParameterValue("Genre"); }
            set { SetParameterValue("Genre", value); }
        }

        public string LastName
        {
            get { return GetParameterValue("Author Last Name"); }
            set { SetParameterValue("Author Last Name", value); }
        }

        public string FirstName
        {
            get { return GetParameterValue("Author First Name"); }
            set { SetParameterValue("Author First Name", value); }
        }

        public string Title
        {
            get { return GetParameterValue("Title"); }
            set { SetParameterValue("Title", value); }
        }

        public string Format
        {
            get { return GetParameterValue("Format"); }
            set { SetParameterValue("Format", value); }
        }

        public string Condition
        {
            get { return GetParameterValue("Book Condition"); }
            set { SetParameterValue("Book Condition", value); }
        }

        public string Status
        {
            get { return GetParameterValue("Book Status"); }
            set { SetParameterValue("Book Status", value); }
        }

        public bool AddBookToLibrary()
        {
            bool success = false;

            return success;
        }

        protected override bool _dataIsValid()
        {
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

            LastName = authorInfo.LastName;
            FirstName = authorInfo.FirstName;
        }
    }

}
