using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.ForSale;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.Ownned;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PublishInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo.PuchaseInfo;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Author;
using ExperimentSimpleBkLibInvTool.ModelInMVC.Series;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    class BookInfoModel
    {
        private int _idBookInfo;
        private int _category;
        private int _titleKey;
        private string _title;
        private int _authorKey;
        private int _seriesKey;
        private int _formatKey;
        private PuchaseInfoModel _puchaseInfo;
        private PublishInfoModel _publishInfo;
        private OwnerShipModel _owned;
        private ForSaleModel _forSale;
        private AuthorModel _authorInfo;
        private SeriesModel _seriesInfo;

        public BookInfoModel()
        {
            _authorInfo = new AuthorModel();
            _publishInfo = new PublishInfoModel();
            _puchaseInfo = new PuchaseInfoModel();
            _owned = new OwnerShipModel();
            _forSale = new ForSaleModel();
            _seriesInfo = new SeriesModel();
            _idBookInfo = 0;
            _formatKey = 0;
            _category = 0;
            _titleKey = 0;
            _authorKey = 0;
            _seriesKey = 0;
            _title = null;
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
            get { return _forSale; }
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

        public string Title { get { return _title; } }

        public int SeriesKey { get { return _seriesKey; } }

        public int BookID { get { return _idBookInfo; } }

        public int CategoryId { get { return _category; } }

        public int FormatId { get { return _formatKey; } }

        public int AuthorId { get { return _authorKey; } }

        public int TitleId { get { return _titleKey; } }

    }
}
