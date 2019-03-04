using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using ExperimentSimpleBkLibInvTool.ModelInMVC.DataTableModel;

namespace ExperimentSimpleBkLibInvTool.ModelInMVC.BookInfo
{
    public class BookInfoModel : DataTableItemBaseModel, IBookInfoModel
    {
        private uint _idBookInfo;
        private uint _genreKey;
        private uint _titleKey;
        private uint _authorKey;
        private uint _seriesKey;
        private uint _formatKey;

        public BookInfoModel(uint BookID = 0, uint Genre = 0, uint TitleId = 0, uint AuthorId = 0, uint SeriesId = 0, uint FormatId = 0)
            : base(((App)Application.Current).Model.BookInfoTable)
        {
            _idBookInfo = BookID;
            _genreKey = Genre;
            _titleKey = TitleId;
            _authorKey = AuthorId;
            _seriesKey = SeriesId;
            _formatKey = FormatId;
    }

        public BookInfoModel(IAuthorModel Author, string Genre, string Title, string Series, string Format)
            : base(((App)Application.Current).Model.BookInfoTable)
        {
            _idBookInfo = 0;
            _genreKey = ConvertGenreToKey(Genre);
            _titleKey = ConvertTitleToKey(Title);
            _authorKey = ConvertAuthorToKey(Author);
            _seriesKey = ConvertSeriesToKey(Author, Series);
            _formatKey = ConvertFormatToKey(Format);
        }

        public uint SeriesKey { get { return _seriesKey; } }

        public uint BookID { get { return _idBookInfo; } }

        public uint CategoryId { get { return _genreKey; } }

        public string Genre
        {
            get { return GetParameterValue("Genre"); }
            set { SetParameterValue("Genre", value); }
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

        public uint FormatId { get { return _formatKey; } }

        public uint AuthorId { get { return _authorKey; } }

        public uint TitleId { get { return _titleKey; } }

        public bool AddBook()
        {
            bool wasAdded = false;

            return wasAdded;
        }

        protected override bool _dataIsValid()
        {
            if (_defaultIsValid())
            {
                return true;
            }
            throw new NotImplementedException();
        }

        private uint ConvertGenreToKey(string Genre)
        {
            uint genreId = 0;

            return genreId;
        }

        private uint ConvertFormatToKey(string Format)
        {
            uint formatId = 0;

            return formatId;
        }

        private uint ConvertAuthorToKey(IAuthorModel author)
        {
            uint authorid = 0;

            return authorid;
        }

        private uint ConvertTitleToKey(string Title)
        {
            uint titleid = 0;

            return titleid;
        }

        private uint ConvertSeriesToKey(IAuthorModel author, string Series)
        {
            uint seriesId = 0;

            return seriesId;
        }
    }
}
