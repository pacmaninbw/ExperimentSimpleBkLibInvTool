using System.Windows;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;
using ExperimentSimpleBkLibInvTool.ModelInMVC.ItemBaseModel;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Category;
using ExperimentSimpleBkLibInvTool.ModelInMVC.FormatsTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public class BookInfoModel : DataTableItemBaseModel
    {
        private uint _idBookInfo;
        private uint _category;
        private uint _titleKey;
        private string _title;
        private uint _authorKey;
        private uint _seriesKey;
        private uint _formatKey;
        private string _status;
        private string _condition;
        private PuchaseInfoModel _puchaseInfo;
        private PublishInfoModel _publishInfo;
        private OwnerShipModel _owned;
        private ForSaleModel _forSale;
        private AuthorModel _authorInfo;
        private SeriesModel _seriesInfo;
        private CategoryModel _categoryM;
        private FormatModel _formatM;
        private RatingsModel _ratings;

        public BookInfoModel()
        {
            InitializeSqlCommands();

            _authorInfo = null;
            _publishInfo = null;
            _puchaseInfo = null;
            _owned = null;
            _forSale = null;
            _seriesInfo = null;
            _idBookInfo = 0;
            _formatKey = 0;
            _category = 0;
            _titleKey = 0;
            _authorKey = 0;
            _seriesKey = 0;
            _status = null;
            _condition = null;
            _title = null;
        }

        public BookInfoModel(uint category, string title, IPublishInfoModel publishInfo, IPuchaseInfoModel puchaseInfo, IOwnerShipModel owned,
            IForSaleModel forSale, IAuthorModel authorInfo, ISeriesModel seriesInfo, RatingsModel ratings, string Status = null, string Condition = null)
        {
            InitializeSqlCommands();

            _category = category;
            _title = title;
            PublishInfo = publishInfo;
            PuchaseInfo = puchaseInfo;
            Owned = owned;
            ForSale = forSale;
            AuthorInfo = authorInfo;
            SeriesInfo = seriesInfo;
            _ratings = ratings;
            _condition = Condition;
            _status = Status;
        }

        public BookInfoModel(AuthorModel authorInfo, string title, CategoryModel category, PuchaseInfoModel puchaseInfo=null, PublishInfoModel publishInfo=null,
            OwnerShipModel owned=null, ForSaleModel forSale=null, SeriesModel seriesInfo=null, FormatModel formatm=null,  RatingsModel ratings=null, string Status=null, string Condition=null)
        {
            InitializeSqlCommands();

            _categoryM = category;
            _title = title;
            _puchaseInfo = puchaseInfo;
            _publishInfo = publishInfo;
            _owned = owned;
            _forSale = forSale;
            _authorInfo = authorInfo;
            _seriesInfo = seriesInfo;
            _formatM = formatm;
            _condition = Condition;
            _status = Status;
            _ratings = ratings;

            if (authorInfo.IsValid)
            {
                _authorKey = authorInfo.ID;
            }
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

        public RatingsModel Ratings
        {
            get { return _ratings; }
            set { _ratings = value; }
        }

        public string Title { get { return _title; } }

        public uint SeriesKey { get { return _seriesKey; } }

        public uint BookID { get { return _idBookInfo; } }

        public uint CategoryId { get { return _category; } }

        public uint FormatId { get { return _formatKey; } }

        public uint AuthorId { get { return _authorKey; } }

        public uint TitleId { get { return _titleKey; } }

        private void InitializeSqlCommands()
        {

        }

        protected override bool _dataIsValid()
        {
            bool dataIsValid = true;
            bool seriesIsValid = true;
            bool purchaseIsValid = true;
            bool publishIsValid = true;
            bool authorIsValid = true;
            bool forSaleIsValid = true;

            if (_seriesInfo != null)
            {
                seriesIsValid = _seriesInfo.IsValid;
            }

            if (_puchaseInfo != null)
            {
                purchaseIsValid = _puchaseInfo.IsValid;
            }

            if (_publishInfo != null)
            {
                publishIsValid = _publishInfo.IsValid;
            }

            if (_authorInfo != null)
            {
                authorIsValid = _authorInfo.IsValid;
            }
            else
            {
                string errorMsg = "Author first and last names are required fields.";
                MessageBox.Show(errorMsg);
                authorIsValid = false;
            }

            if (_forSale != null)
            {
                forSaleIsValid = _forSale.IsValid;
            }

            if (_categoryM == null)
            {
                string errorMsg = "The book category is a required field.";
                MessageBox.Show(errorMsg);
                dataIsValid = false;
            }

            if (!seriesIsValid || !purchaseIsValid || !publishIsValid || !authorIsValid || !forSaleIsValid)
            {
                dataIsValid = false;
            }

            if (string.IsNullOrEmpty(_title))
            {
                string errorMsg = "The book title is a required field.";
                MessageBox.Show(errorMsg);
                dataIsValid = false;
            }

            return dataIsValid;
        }

    }
}
