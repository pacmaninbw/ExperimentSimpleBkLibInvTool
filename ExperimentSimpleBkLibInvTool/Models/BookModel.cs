using System;
using System.Collections.Generic;
using System.Windows;

namespace pacsw.BookInventory.Models
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
        private List<DataTableItemBaseModel> _itemsToValidate;
        private List<DataTableItemBaseModel> _itemsToAddToDb;

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
        private VolumeInSeries _volumeInSeries;
        private Synopsis _synopsis;

        public BookModel()
        {
            _bookKey = 0;
            _bookInfo = new BookInfoModel();
            _authorInfo = null;
            _forSale = null;
            _optionalItems = null;
            _owned = null;
            _publishInfo = null;
            _puchaseInfo = null;
            _ratings = null;
            _seriesInfo = null;
            _volumeInSeries = null;
            _synopsis = null;
            _itemsToValidate = new List<DataTableItemBaseModel>();
            _itemsToAddToDb = new List<DataTableItemBaseModel>();

            _itemsToValidate.Add(_bookInfo);
        }

        public IPublishInfoModel PublishInfo
        {
            get { return _publishInfo; }
            set { InsertIntoLists(_publishInfo = (PublishInfoModel)value); }
        }

        public IPuchaseInfoModel PuchaseInfo
        {
            get { return _puchaseInfo; }
            set { InsertIntoLists(_puchaseInfo = (PuchaseInfoModel)value); }
        }

        public IOwnerShipModel Owned
        {
            get { return _owned; }
            set { InsertIntoLists(_owned = (OwnerShipModel)value); }
        }

        public IForSaleModel ForSale
        {
            get { return (IForSaleModel) _forSale; }
            set { InsertIntoLists(_forSale = (ForSaleModel)value); }
        }

        public IAuthorModel AuthorInfo
        {
            get { return _authorInfo; }
            set { SetAuthorValues((AuthorModel)value); }
        }

        public ISeriesModel SeriesInfo
        {
            get { return _seriesInfo; }
            set { SetSeriesValues((SeriesModel)value); }
        }

        public IRatingsModel Ratings
        {
            get { return _ratings; }
            set { InsertIntoLists(_ratings = (RatingsModel)value); }
        }

        public IConditionsAndOtherOptionsModel ConditionsAndOptions
        {
            get { return _optionalItems; }
            set { InsertIntoLists(_optionalItems = (ConditionsAndOtherOptionsModel)value); }
        }

        public VolumeInSeries VolumeNumber
        {
            get { return _volumeInSeries; }
            set { InsertIntoLists(_volumeInSeries = value); }
        }

        public Synopsis Summary
        {
            get { return _synopsis; }
            set { InsertIntoLists(_synopsis = value); }
        }

        public string Genre
        {
            get { return TheModel.CategoryTable.CategoryTitle(_bookInfo.GenreId); ; }
            set { _bookInfo.GenreId = TheModel.CategoryTable.CategoryKey(value); }
        }

        public string Title
        {
            get { return TheModel.BookTable.GetTitle(_bookInfo.TitleId); }
            set { _bookInfo.TitleId = TheModel.BookTable.InsertTitleIfNotInTable(value); }
        }

        public string Format
        {
            get { return TheModel.FormatTable.FormatTitle(_bookInfo.FormatId); }
            set { _bookInfo.FormatId = TheModel.FormatTable.FormatKey(value); }
        }

        public bool AddBookToLibrary()
        {
            bool success = IsValid;

            try
            {
                if (success)
                {
                    if (success = _bookInfo.AddToDb())
                    {
                        _bookKey = _bookInfo.BookID;
                        if (_seriesInfo != null && _volumeInSeries != null)
                        {
                            _volumeInSeries.SeriesId = _bookInfo.SeriesId;
                        }
                    }

                    if (_bookKey > 0)
                    {
                        foreach (DataTableItemBaseModel item in _itemsToAddToDb)
                        {
                            success = AddToDb(success, item);
                            if (!success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "Database Error: " + ex.Message;
                MessageBox.Show(errorMsg);
            }

            return success;
        }

        public bool UpdateBookWithEdits()
        {
            bool success = IsValid;

            try
            {
                if (success)
                {
                    if (success = _bookInfo.AddToDb())
                    {
                        _bookKey = _bookInfo.BookID;
                        if (_seriesInfo != null && _volumeInSeries != null)
                        {
                            _volumeInSeries.SeriesId = _bookInfo.SeriesId;
                        }
                    }

                    if (_bookKey > 0)
                    {
                        foreach (DataTableItemBaseModel item in _itemsToAddToDb)
                        {
                            success = AddToDb(success, item);
                            if (!success)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "Database Error: " + ex.Message;
                MessageBox.Show(errorMsg);
            }

            return success;
        }

        public void DeleteBook()
        {
            if (_bookKey > 0)
            {
                if (ConfirmDeleteBook())
                {
                    TheModel.BookTable.DeleteBook(_bookKey);
                }
            }
        }

        // Identify the book in the database and fill all the aggregate class members with data
        public void SelectBookForEditOrDelete(string lastName, string firstName, string title, string format)
        {
            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(format))
            {
                // Should never get here. The fields are required when adding a book.
                MessageBox.Show("One or more required fields are missing from the selected book.", "Select Book Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _authorInfo = TheModel.AuthorTable.GetAuthor(lastName, firstName);
            Title = title;
            Format = format;

            _bookInfo = TheModel.BookInfoTable.GetBookInfo(_authorInfo.AuthorId, _bookInfo.TitleId, _bookInfo.FormatId);
            _bookKey = _bookInfo.BookID;
            _seriesInfo = TheModel.SeriesTable.GetSeriesModel(_bookInfo.SeriesId);

            if (_bookKey > 0)
            {
                FillBookModelWithExistingData(_bookKey);
            }
        }

        public bool IsValid { get { return _dataIsValid(); } }

        protected bool _dataIsValid()
        {
            bool isValid = true;

            foreach (DataTableItemBaseModel item in _itemsToValidate)
            {
                isValid = ValidityTest(item, isValid);
            }

            return isValid;
        }

        private void SetAuthorValues(AuthorModel authorInfo)
        {
            if (!authorInfo.IsValid)
            {
                string errorMsg = "Author first and last names are required fields.";
                MessageBox.Show(errorMsg);
                return;
            }

            _authorInfo = authorInfo;
            _bookInfo.AuthorId = authorInfo.AuthorId;
            _itemsToValidate.Add(authorInfo);
        }

        private void SetSeriesValues(SeriesModel seriesModel)
        {
            if (!seriesModel.IsValid || !_authorInfo.IsValid)
            {
                return;
            }

            _seriesInfo = seriesModel;
            _bookInfo.SeriesId = TheModel.SeriesTable.GetSeriesKey(_authorInfo, _seriesInfo.Title); ;
            _itemsToValidate.Add(seriesModel);
        }

        private bool AddToDb(bool success, DataTableItemBaseModel item)
        {
            if (success && item != null)
            {
                item.BookId =_bookKey;
                success = item.AddToDb();
            }

            return success;
        }

        private bool ValidityTest(DataTableItemBaseModel item, bool isValid)
        {
            if (isValid)
            {
                if (item != null && !item.IsValid)
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private void InsertIntoLists(DataTableItemBaseModel item)
        {
            _itemsToValidate.Add(item);
            _itemsToAddToDb.Add(item);
        }

        // When the user selects a book in the grid, all parts of the aggregation
        // that exist are added to the book model.
        private void FillBookModelWithExistingData(uint bookKey)
        {
            ConditionsAndOptions = TheModel.ConditionsAndOptions.GetConditionsAndOtherOptions(bookKey);
            ForSale = TheModel.ForSaleTable.GetForSaleModel(bookKey);
            Owned = TheModel.OwnerShip.GetOwnerShipModel(bookKey);
            PublishInfo = TheModel.PublishingData.GetPublishInfo(bookKey);
            PuchaseInfo = TheModel.PurchaseData.GetPuchaseInfo(bookKey);
            Ratings = TheModel.RatingsTable.GetRatingsData(bookKey);
            VolumeNumber = TheModel.VolumeInSeriesTable.GetVolumneInSersData(bookKey);
            Summary = TheModel.SynopsisTable.GetSynopsisData(bookKey);
        }

        private bool ConfirmDeleteBook()
        {
            bool executeDelete = false;

            string bookData = "Do you really want to delete the book:" + Environment.NewLine + "\t" + Title + Environment.NewLine;
            bookData += "\tBy " + _authorInfo.FirstName + " " + _authorInfo.LastName + Environment.NewLine;
            bookData += "from the inventory?" + Environment.NewLine + "Once this is done it can not be undone.";

            MessageBoxResult confirmation = MessageBox.Show(bookData, "Delete Book Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (confirmation == MessageBoxResult.Yes)
            {
                executeDelete = true;
            }

            return executeDelete;
        }
    }
}
