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
        private SeriesModel _seriesInfo;

        private int _bookInfoIndex = -1;
        private int _authorIndex = -1;
        private int _seriesIndex = -1;
        private int _forSaleIndex = -1;
        private int _optionsIndex = -1;
        private int _ownedIndex = -1;
        private int _puchaseInfoIndex = -1;
        private int _publishInfoIndex = -1;
        private int _ratingsIndex = -1;
        private int _volumeInSeriesIndex = -1;
        private int _synopsisIndex = -1;

        public BookModel(bool EditNotInsert)
        {
            _bookKey = 0;
            _bookInfo = new BookInfoModel();
            _authorInfo = null;
            _itemsToValidate = new List<DataTableItemBaseModel>();
            _itemsToAddToDb = new List<DataTableItemBaseModel>();
            _editMode = EditNotInsert;

            if (!EditNotInsert)
            {
                _bookInfoIndex = AddUnique(_itemsToValidate, _bookInfo);
            }
            else
            {
                _itemsToUpDate = new List<DataTableItemBaseModel>();
            }
        }

        public PublishInfoModel PublishInfo
        {
            get => GetItemFromValidateList(_publishInfoIndex) as PublishInfoModel;
            set => _publishInfoIndex = InsertIntoLists(value, _publishInfoIndex);
        }

        public PuchaseInfoModel PuchaseInfo
        {
            get  => GetItemFromValidateList(_puchaseInfoIndex) as PuchaseInfoModel;
            set => _puchaseInfoIndex = InsertIntoLists(value, _puchaseInfoIndex);
        }

        public OwnerShipModel Owned
        {
            get => GetItemFromValidateList(_ownedIndex) as OwnerShipModel;
            set => _ownedIndex = InsertIntoLists(value, _ownedIndex);
        }

        public ForSaleModel ForSale
        {
            get => GetItemFromValidateList(_forSaleIndex) as ForSaleModel;
            set => _forSaleIndex = InsertIntoLists(value, _forSaleIndex);
        }

        public RatingsModel Ratings
        {
            get => GetItemFromValidateList(_ratingsIndex) as RatingsModel;
            set => _ratingsIndex = InsertIntoLists(value, _ratingsIndex);
        }

        public ConditionsAndOtherOptionsModel ConditionsAndOptions
        {
            get => GetItemFromValidateList(_optionsIndex) as ConditionsAndOtherOptionsModel;
            set => _optionsIndex = InsertIntoLists(value, _optionsIndex);
        }

        public VolumeInSeries VolumeNumber
        {
            get => GetItemFromValidateList(_volumeInSeriesIndex) as VolumeInSeries;
            set => _volumeInSeriesIndex = InsertIntoLists(value, _volumeInSeriesIndex);
        }

        public Synopsis Summary
        {
            get => GetItemFromValidateList(_synopsisIndex) as Synopsis;
            set => _synopsisIndex = InsertIntoLists(value, _synopsisIndex);
        }

        public AuthorModel AuthorInfo
        {
            get => GetItemFromValidateList(_authorIndex) as AuthorModel;
            set => SetAuthorValues(value);
        }

        public SeriesModel SeriesInfo
        {
            get => GetItemFromValidateList(_seriesIndex) as SeriesModel;
            set => SetSeriesValues(value);
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
                        if (_seriesIndex > -1 && _volumeInSeriesIndex > -1)
                        {
                           ((VolumeInSeries)_itemsToValidate[_volumeInSeriesIndex]).SeriesId = _bookInfo.SeriesId;
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
                success = false;
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
                        FinalizeItemsToUpdate();
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
                success = false;
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

            AuthorInfo = TheModel.AuthorTable.GetAuthor(lastName, firstName);
            Title = title;
            Format = format;

            _bookInfo = TheModel.BookInfoTable.GetBookInfo(_authorInfo.AuthorId, _bookInfo.TitleId, _bookInfo.FormatId);
            _bookKey = _bookInfo.BookID;
            SeriesInfo = TheModel.SeriesTable.GetSeriesModel(_bookInfo.SeriesId);
            _bookInfoIndex = AddUnique(_itemsToValidate, _bookInfo);

            if (_bookKey > 0)
            {
                FillBookModelWithExistingData(_bookKey);
                // Make sure nothings modified at the start.
                foreach(DataTableItemBaseModel item in _itemsToValidate)
                {
                    item.Reset();
                }
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
            _authorIndex = AddUnique(_itemsToValidate, _authorInfo);
        }

        private void SetSeriesValues(SeriesModel seriesModel)
        {
            if (!seriesModel.IsValid || !_authorInfo.IsValid)
            {
                return;
            }

            _seriesInfo = seriesModel;
            _bookInfo.SeriesId = TheModel.SeriesTable.GetSeriesKey(_authorInfo, _seriesInfo.Title); ;
            _seriesIndex = AddUnique(_itemsToValidate, seriesModel);
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

        private int InsertIntoLists(DataTableItemBaseModel localItem, int itemIndex)
        {
            int validationIndex = itemIndex;
            if (_editMode)
            {
                // User added new data.
                if (itemIndex < 0)
                {
                    validationIndex = AddUnique(_itemsToValidate, localItem);
                    AddUnique(_itemsToAddToDb, localItem);
                }
                else
                {
                    // User updated exisitng value.
                    AddUnique(_itemsToUpDate, localItem);
                }
            }
            else
            {
                validationIndex = AddUnique(_itemsToValidate, localItem);
                AddUnique(_itemsToAddToDb, localItem);
            }

            return validationIndex;
        }

        // When the user selects a book in the grid, all parts of the aggregation
        // that exist are added to the book model.
        private void FillBookModelWithExistingData(uint bookKey)
        {
            _optionsIndex =  AddUnique(_itemsToValidate, TheModel.ConditionsAndOptions.GetConditionsAndOtherOptions(bookKey));
            _forSaleIndex = AddUnique(_itemsToValidate, TheModel.ForSaleTable.GetForSaleModel(bookKey));
            _ownedIndex = AddUnique(_itemsToValidate, TheModel.OwnerShip.GetOwnerShipModel(bookKey));
            _publishInfoIndex = AddUnique(_itemsToValidate, TheModel.PublishingData.GetPublishInfo(bookKey));
            _puchaseInfoIndex = AddUnique(_itemsToValidate, TheModel.PurchaseData.GetPuchaseInfo(bookKey));
            _ratingsIndex = AddUnique(_itemsToValidate, TheModel.RatingsTable.GetRatingsData(bookKey));
            _volumeInSeriesIndex = AddUnique(_itemsToValidate, TheModel.VolumeInSeriesTable.GetVolumneInSersData(bookKey));
            _synopsisIndex = AddUnique(_itemsToValidate, TheModel.SynopsisTable.GetSynopsisData(bookKey));
        }

        private int AddUnique(List<DataTableItemBaseModel> list, DataTableItemBaseModel item)
        {
            int indexOf = -1;

            if (item != null)
            {
                Type itemType = item.GetType();
                DataTableItemBaseModel Found = null;
                Found = list.Find(x => x.GetType() == itemType);
                if (Found == null)
                {
                    list.Add(item);
                    indexOf = list.Count - 1;
                }
                else
                {
                    indexOf = list.IndexOf(Found);
                }
            }

            return indexOf;
        }

        /*
         * This function is only called when editing a book, it is not necessary when adding a book
         * because all items will be in the _itemsToAddToDb list.
         */
        private void FinalizeItemsToUpdate()
        {
            // Shouldn't need these.
            //_itemsToValidate[_bookInfoIndex].Reset();
            //_itemsToValidate[_authorIndex].Reset();

            foreach (DataTableItemBaseModel item in _itemsToValidate)
            {
                if (item.IsModified)
                {
                    Type itemType = item.GetType();
                    object Found = null;
                    Found = _itemsToAddToDb.Find(x => x.GetType() == itemType);
                    if (Found == null)
                    {
                        AddUnique(_itemsToUpDate, item);
                    }
                }
            }
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

        private DataTableItemBaseModel GetItemFromValidateList(int itemIndex)
        {
            return (itemIndex > -1) ? _itemsToValidate[itemIndex] : null;
        }
    }
}
