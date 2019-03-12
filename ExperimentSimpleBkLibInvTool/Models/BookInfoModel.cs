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
        private readonly Model TheModel = ((App)Application.Current).Model;

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
            get { return TheModel.CategoryTable.CategoryTitle(_genreKey); }
            set { SetGenreParameter(value); }
        }

        public string Series
        {
            get { return TheModel.SeriesTable.GetSeriesTitle(_seriesKey); }
            set
            {
                _seriesKey = ConvertSeriesToKey(value);
                SetParameterValue("Series Key", _seriesKey);
            }
        }
        public string Title
        {
            get { return GetTitleByKey(); }
            set { SetTitleKeyParameter(value); }
        }

        public string Format
        {
            get { return TheModel.FormatTable.FormatTitle(_formatKey); }
            set { SetFormatParameter(value); }
        }

        public uint FormatId { get { return _formatKey; } }

        public uint AuthorId { get { return _authorKey; } }

        public uint TitleId { get { return _titleKey; } }

        public override bool AddToDb()
        {
           return TheModel.BookInfoTable.AddBookInfo(this);
        }

        protected override bool _dataIsValid()
        {
            bool isValid = _defaultIsValid();

            if (!isValid)
            {
                return isValid;
            }

            if (_authorKey < 1)
            {
                MessageBox.Show("Author has not been selected", "Missing Author selection", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (_titleKey < 1)
            {
                MessageBox.Show("The title has not been entered", "Missing title", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            return isValid;
        }

        private void SetGenreParameter(string genre)
        {
            _genreKey = ConvertGenreToKey(genre);
            SetParameterValue("Genre Id", _genreKey);
        }

        private uint ConvertGenreToKey(string genre)
        {
            return TheModel.CategoryTable.CategoryKey(genre);
        }

        private void SetFormatParameter(string format)
        {
            _formatKey = ConvertFormatToKey(format);
            SetParameterValue("Format Key", _formatKey);
        }

        private uint ConvertFormatToKey(string Format)
        {
            return TheModel.FormatTable.FormatKey(Format);
        }

        private uint ConvertAuthorToKey(IAuthorModel author)
        {
            return TheModel.AuthorTable.AuthorKey((AuthorModel)author);
        }

        private void SetAuthorKeyParameter(AuthorModel author)
        {
            _authorKey = ConvertAuthorToKey(author);
            SetParameterValue("Author Id", _authorKey);
        }

        private string GetTitleByKey()
        {
            return TheModel.BookInfoTable.GetTitle(_titleKey);
        }

        private void SetTitleKeyParameter(string title)
        {
            _titleKey = ConvertTitleToKey(title);
            SetParameterValue("Title Key", _titleKey);
        }

        private uint ConvertTitleToKey(string title)
        {
            uint titleid = 0;

            titleid = TheModel.BookInfoTable.InsertTitleIfNotInTable(title);

            return titleid;
        }

        private uint ConvertSeriesToKey(IAuthorModel author, string series)
        {
            return ConvertSeriesToKey(author as AuthorModel, series);
        }

        private uint ConvertSeriesToKey(AuthorModel author, string series)
        {
            return TheModel.SeriesTable.GetSeriesKey(author, series);
        }

        private uint ConvertSeriesToKey(string title)
        {
            uint key = 0; ;

            if (_authorKey > 0)
            {
                AuthorModel author = TheModel.AuthorTable.GetAuthorFromId(_authorKey);
                key = TheModel.SeriesTable.GetSeriesKey(author, title);
            }

            return key;
        }
    }
}
