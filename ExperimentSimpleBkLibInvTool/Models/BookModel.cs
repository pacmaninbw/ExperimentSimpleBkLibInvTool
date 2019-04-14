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
        private List<DataTableItemBaseModel> _itemsToUpDate;

        private uint _bookKey;
        private bool _editMode;

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

        public BookModel(bool EditNotInsert)
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
            _itemsToUpDate = new List<DataTableItemBaseModel>();
            _editMode = EditNotInsert;

            if (!EditNotInsert)
            {
                _itemsToValidate.Add(_bookInfo);
            }
        }

        public PublishInfoModel PublishInfo
        {
            get => _publishInfo;
            set => _publishInfo = IsInsertedIntoLists(_publishInfo, value) ? value : _publishInfo;
        }

        public PuchaseInfoModel PuchaseInfo
        {
            get  => _puchaseInfo;
            set => _puchaseInfo = (IsInsertedIntoLists(_puchaseInfo, value)) ? value : _puchaseInfo;
        }

        public OwnerShipModel Owned
        {
            get => _owned;
            set => _owned =  IsInsertedIntoLists(_owned, value) ? value : _owned;
        }

        public ForSaleModel ForSale
        {
            get => _forSale;
            set => _forSale = IsInsertedIntoLists(_forSale, value) ? value : _forSale;
        }

        public AuthorModel AuthorInfo
        {
            get => _authorInfo;
            set => SetAuthorValues(value);
        }

        public SeriesModel SeriesInfo
        {
            get => _seriesInfo;
            set => SetSeriesValues(value);
        }

        public RatingsModel Ratings
        {
            get => _ratings;
            set => _ratings = IsInsertedIntoLists(_ratings, value) ? value : _ratings;
        }

        public ConditionsAndOtherOptionsModel ConditionsAndOptions
        {
            get => _optionalItems;
            set => _optionalItems = IsInsertedIntoLists(_optionalItems, value) ? value : _optionalItems;
        }

        public VolumeInSeries VolumeNumber
        {
            get => _volumeInSeries;
            set => _volumeInSeries = IsInsertedIntoLists(_volumeInSeries, value) ? value : _volumeInSeries;
        }

        public Synopsis Summary
        {
            get => _synopsis;
            set => _synopsis = IsInsertedIntoLists(_synopsis, value) ? value : _synopsis;
        }

        public string Genre
        {
            get { return TheModel.CategoryTable.CategoryTitle(_bookInfo.GenreId); ; }
            set => _bookInfo.GenreId = TheModel.CategoryTable.CategoryKey(value);
        }

        public string Title
        {
            get => TheModel.BookTable.GetTitle(_bookInfo.TitleId);
            set => _bookInfo.TitleId = TheModel.BookTable.InsertTitleIfNotInTable(value);
        }

        public string Format
        {
            get => TheModel.FormatTable.FormatTitle(_bookInfo.FormatId);
            set => _bookInfo.FormatId = TheModel.FormatTable.FormatKey(value);
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
                if (success && _bookKey > 0)
                {
                    foreach (DataTableItemBaseModel item in _itemsToAddToDb)
                    {
                        success = AddToDb(success, item);
                        if (!success)
                        {
                            break;
                        }
                    }

                    if (success)
                    {
                        foreach (DataTableItemBaseModel item in _itemsToUpDate)
                        {
                            success = DbUpdate(success, item);
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
            _itemsToValidate.Add(_bookInfo);

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

        private bool DbUpdate(bool success, DataTableItemBaseModel item)
        {
            if (success && item != null)
            {
                item.BookId = _bookKey;
                success = item.DbUpdate();
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

        private bool IsInsertedIntoLists(DataTableItemBaseModel localItem, DataTableItemBaseModel incomingItem)
        {
            if (!_editMode)
            {
                localItem = incomingItem;
                _itemsToAddToDb.Add(localItem);
                _itemsToValidate.Add(localItem);
                return true;
            }
            else
            {
                if (localItem == incomingItem)
                {
                    MessageBox.Show("Local Item and Incoming Item are same item");
                }
                bool localItemWasNull = localItem == null;
                if (localItemWasNull || (!localItem.Modified && incomingItem.Modified))
                {
                    localItem = incomingItem;
                    _itemsToValidate.Add(localItem);
                    if (localItemWasNull)
                    {
                        _itemsToAddToDb.Add(localItem);
                    }
                    else
                    {
                        _itemsToUpDate.Add(localItem);
                    }
                    return true;
                }
                return false;
            }
        }

        // When the user selects a book in the grid, all parts of the aggregation
        // that exist are added to the book model.
        private void FillBookModelWithExistingData(uint bookKey)
        {
            _optionalItems = TheModel.ConditionsAndOptions.GetConditionsAndOtherOptions(bookKey);
            _forSale = TheModel.ForSaleTable.GetForSaleModel(bookKey);
            _owned = TheModel.OwnerShip.GetOwnerShipModel(bookKey);
            _publishInfo = TheModel.PublishingData.GetPublishInfo(bookKey);
            _puchaseInfo = TheModel.PurchaseData.GetPuchaseInfo(bookKey);
            _ratings = TheModel.RatingsTable.GetRatingsData(bookKey);
            _volumeInSeries = TheModel.VolumeInSeriesTable.GetVolumneInSersData(bookKey);
            _synopsis = TheModel.SynopsisTable.GetSynopsisData(bookKey);
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
